using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamTracker.Models;

namespace StreamTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private readonly StreamTrackerDbContext _context;

        public EpisodesController(StreamTrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/episodes/video/{videoId}
        [HttpGet("video/{videoId}")]
        public async Task<ActionResult<IEnumerable<Episode>>> GetEpisodesForVideo(int videoId)
        {
            var episodes = await _context.Episodes
                .Where(e => e.VideoId == videoId)
                .ToListAsync();

            return Ok(episodes);
        }

        // GET: api/episodes/status/{status}
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Episode>>> GetEpisodesByStatus(string status)
        {
            var episodes = await _context.Episodes
                .Where(e => e.Status.ToLower() == status.ToLower())
                .ToListAsync();

            return Ok(episodes);
        }

        // POST: api/episodes
        [HttpPost]
        public async Task<ActionResult<Episode>> CreateEpisode(Episode episode)
        {
            _context.Episodes.Add(episode);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEpisodesForVideo), new { videoId = episode.VideoId }, episode);
        }

        // PUT: api/episodes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEpisode(int id, Episode updatedEpisode)
        {
            if (id != updatedEpisode.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedEpisode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Episodes.Any(e => e.Id == id))
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

        // DELETE: api/episodes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpisode(int id)
        {
            var episode = await _context.Episodes.FindAsync(id);
            if (episode == null)
            {
                return NotFound();
            }

            _context.Episodes.Remove(episode);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
