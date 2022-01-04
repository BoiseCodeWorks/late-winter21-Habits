using System;
using System.Collections.Generic;
using MyResolutions.Models;
using MyResolutions.Repositories;

namespace MyResolutions.Services
{
  public class TrackedHabitsService
  {
    private readonly TrackedHabitsRepository _repo;
    private readonly HabitsService _hs;

    public TrackedHabitsService(TrackedHabitsRepository repo, HabitsService hs)
    {
      _repo = repo;
      _hs = hs;
    }

    internal TrackedHabit Create(TrackedHabit newTracked)
    {
      // TODO check if exists already for today
      //   TrackedHabit exists

      // find the habit
      Habit habit = _hs.GetById(newTracked.HabitId);
      if (habit.CreatorId != newTracked.AccountId)
      {
        throw new Exception("You Cannot track another users habits");
      }
      TrackedHabit th = _repo.Create(newTracked);
      return th;

    }

    internal List<TrackedHabitProfile> GetTrackedProfilesByHabitId(int id)
    {
      Habit habit = _hs.GetById(id);
      if (habit.isPrivate == true)
      {
        throw new Exception("This is a privatly tracked habit");
      }
      List<TrackedHabitProfile> thps = _repo.GetTrackedProfilesByHabitId(id);
      return thps;

    }

    internal void Delete(int id, string userId)
    {
      TrackedHabit tracked = GetById(id);
      if (tracked.AccountId != userId)
      {
        throw new Exception("Invalid Access");
      }
      _repo.Delete(id);
    }

    private TrackedHabit GetById(int id)
    {
      TrackedHabit tracked = _repo.GetById(id);
      if (tracked == null)
      {
        throw new Exception("Invalid TrackedHabitId");
      }
      return tracked;
    }
  }
}