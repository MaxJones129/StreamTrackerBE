namespace StreamTracker.Models
{
  public class Episode
  {
    public int Id { get; set; }
    public int VideoId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public int Rating { get; set; }
    public float? TmdbRating { get; set; }
    public int? Season { get; set; }
    public int? EpisodeNumber { get; set; }
    public string TimeStopped { get; set; }
    }
}
