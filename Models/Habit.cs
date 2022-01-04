using System;
using System.ComponentModel.DataAnnotations;
using MyResolutions.Interfaces;

namespace MyResolutions.Models
{
  public class Habit : IRepoItem
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [MinLength(3)]
    public string Description { get; set; }
    // Adding the '?' allows the property to be null on an update
    public bool? isPrivate { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }

  public class TrackedHabitHabitViewModel : Habit
  {
    public int TrackedHabitId { get; set; }
  }
}