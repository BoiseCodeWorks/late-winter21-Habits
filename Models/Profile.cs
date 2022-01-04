using System;

namespace MyResolutions.Models
{
  public class Profile
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }

  }

  public class TrackedHabitProfile : Profile
  {
    public int TrackedHabitId { get; set; }
    public DateTime CompletedAt { get; set; }
  }
}