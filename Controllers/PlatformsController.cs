using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamTracker.Models;

namespace StreamTracker.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class PlatformsController : ControllerBase
  {
    private readonly StreamTrackerDbContext _context;

    public PlatformsController(StreamTrackerDbContext context)
    {
      _context = context;
    }

    // GET: api/Platforms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Platform>>> GetAllPlatforms()
    {
      return await _context.Platforms.ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Platform>> GetPlatformById(int id)
    {
        var platform = await _context.Platforms.FindAsync(id);
        if (platform == null) return NotFound();
        return platform;
    }

    [HttpPost]
    public async Task<ActionResult<Platform>> CreatePlatform(Platform platform)
    {
        _context.Platforms.Add(platform);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPlatformById), new { id = platform.Id }, platform);
    }
  }
}
