using StreamTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace StreamTracker.Data
{
  public static class SeedData
  {
    public static void SeedAll(ModelBuilder modelBuilder)
    {
      // Platforms
      modelBuilder.Entity<Platform>().HasData(
          new Platform { Id = 1, Name = "Netflix" },
          new Platform { Id = 2, Name = "Disney+" },
          new Platform { Id = 3, Name = "Crunchyroll" },
          new Platform { Id = 4, Name = "HiDive" },
          new Platform { Id = 5, Name = "Apple TV+" },
          new Platform { Id = 6, Name = "Hulu" },
          new Platform { Id = 7, Name = "Amazon Prime Video" },
          new Platform { Id = 8, Name = "HBO Max" },
          new Platform { Id = 9, Name = "Peacock" },
          new Platform { Id = 10, Name = "Paramount+" },
          new Platform { Id = 11, Name = "YouTube" },
          new Platform { Id = 12, Name = "Tubi" },
          new Platform { Id = 13, Name = "Pluto TV" },
          new Platform { Id = 14, Name = "Crackle" },
          new Platform { Id = 15, Name = "The Roku Channel" },
          new Platform { Id = 16, Name = "Freevee" },
          new Platform { Id = 17, Name = "Funimation" },
          new Platform { Id = 18, Name = "VRV" },
          new Platform { Id = 19, Name = "Twitch" },
          new Platform { Id = 20, Name = "TikTok" }
      );

      // Users    
      modelBuilder.Entity<User>().HasData(
          new User
          {
            Id = 1,
            Username = "sampleuser",
            Email = "sample@example.com",
            UserUid = "firebase-uid-123",
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
          }
      );

      // Videos
      modelBuilder.Entity<Video>().HasData(
          new Video
          {
            Id = 1,
            UserId = 1,
            Title = "Sample Show",
            Description = "This is a sample video.",
            Genre = "Drama",
            ReleaseYear = 2020,
            Status = "Watching",
            Rating = 4
          }
      );

      // VideoUrls
      modelBuilder.Entity<VideoUrl>().HasData(
          new VideoUrl
          {
            Id = 1,
            VideoId = 1,
            PlatformId = 1,
            Url = "https://netflix.com/sample-show"
          }
      );

      // Episodes
      modelBuilder.Entity<Episode>().HasData(
          new Episode
          {
            Id = 1,
            VideoId = 1,
            UserId = 1,
            Name = "Episode 1",
            Status = "Watching",
            Rating = 5,
            Season = 1,
            EpisodeNumber = 1,
            TimeStopped = "00:15:30"
          }
      );
    }
  }
}
