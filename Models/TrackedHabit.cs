using System;
using MyResolutions.Interfaces;

namespace MyResolutions.Models
{
  public class TrackedHabit : IRepoItem
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CompletedAt { get; set; }
    public string AccountId { get; set; }
    public int HabitId { get; set; }
  }

  public class TrackedHabitDTO : TrackedHabit
  {
    public Habit Habit { get; set; }
    public Account Account { get; set; }
  }
}