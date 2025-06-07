namespace StreamTracker.Models
{
  public class VideoUrl
  {
    public int Id { get; set; }
    public int VideoId { get; set; }
    public int PlatformId { get; set; }
    public string Url { get; set; }
    public Platform? Platform { get; set; }  // A User can have multiple Conversations
    }
}
