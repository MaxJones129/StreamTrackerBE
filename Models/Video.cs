namespace StreamTracker.Models
{
  public class Video
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Genre { get; set; }
    public int ReleaseYear { get; set; }
    public string Status { get; set; }
    public int Rating { get; set; }
    public float? TmdbRating { get; set; }
    public List<VideoUrl>? VideoUrls { get; set; }  // A User can have multiple Conversations
    }
}
