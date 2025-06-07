using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamTracker.Models;

namespace StreamTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoUrlsController : ControllerBase
    {
        private readonly StreamTrackerDbContext _context;

        public VideoUrlsController(StreamTrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/videourls/video/{videoId}
        [HttpGet("video/{videoId}")]
        public async Task<ActionResult<IEnumerable<VideoUrl>>> GetUrlsForVideo(int videoId)
        {
            var urls = await _context.VideoUrls
                .Include(vu => vu.Platform)
                .Where(vu => vu.VideoId == videoId)
                .ToListAsync();

            return Ok(urls);
        }

        // POST: api/videourls
        [HttpPost]
        public async Task<ActionResult<VideoUrl>> AddVideoUrl(VideoUrl videoUrl)
        {
            _context.VideoUrls.Add(videoUrl);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUrlsForVideo), new { videoId = videoUrl.VideoId }, videoUrl);
        }

        // PUT: api/videourls/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideoUrl(int id, VideoUrl updatedUrl)
        {
            if (id != updatedUrl.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedUrl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.VideoUrls.Any(vu => vu.Id == id))
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

        // DELETE: api/videourls/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoUrl(int id)
        {
            var videoUrl = await _context.VideoUrls.FindAsync(id);
            if (videoUrl == null)
            {
                return NotFound();
            }

            _context.VideoUrls.Remove(videoUrl);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
