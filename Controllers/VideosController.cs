using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamTracker.Models;

namespace StreamTracker.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VideosController : ControllerBase
  {
    private readonly StreamTrackerDbContext _context;

    public VideosController(StreamTrackerDbContext context)
    {
      _context = context;
    }

    // GET: api/videos
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<Video>>> GetVideosByUserId(int userId)
    {
        var videos = await _context.Videos
            .Where(v => v.UserId == userId)
            .Include(v => v.VideoUrls) // Optional: include related video URLs
                .ThenInclude(vu => vu.Platform) // Optional: include platform data
            .ToListAsync();

        if (videos == null || videos.Count == 0)
        {
            return NotFound($"No videos found for user with ID {userId}");
        }

        return Ok(videos);
    }

    // GET: api/Videos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Video>> GetVideoById(int id)
    {
        var video = await _context.Videos
            .Include(v => v.VideoUrls)
                .ThenInclude(vu => vu.Platform)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (video == null)
        {
            return NotFound();
        }

        return video;
    }

    [HttpPost]
    public async Task<ActionResult<Video>> CreateVideo(Video video)
    {
      _context.Videos.Add(video);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetVideoById), new { id = video.Id }, video);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVideo(int id, Video updatedVideo)
    {
      if (id != updatedVideo.Id)
      {
        return BadRequest();
      }

      _context.Entry(updatedVideo).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!_context.Videos.Any(v => v.Id == id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVideo(int id)
    {
      var video = await _context.Videos.FindAsync(id);
      if (video == null)
      {
        return NotFound();
      }

      _context.Videos.Remove(video);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<Video>>> FilterVideos([FromQuery] string? status, [FromQuery] string? platform)
    {
        var query = _context.Videos
            .Include(v => v.VideoUrls)
                .ThenInclude(vu => vu.Platform)
            .AsQueryable();

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(v => v.Status.ToLower() == status.ToLower());
        }

        if (!string.IsNullOrEmpty(platform))
        {
            query = query.Where(v =>
                v.VideoUrls.Any(vu => vu.Platform.Name.ToLower() == platform.ToLower()));
        }

        var result = await query.ToListAsync();

        if (result.Count == 0)
        {
            return Ok(new List<Video>()); // Return empty list instead of 404
        }

        return Ok(result);
    }
  }
}
