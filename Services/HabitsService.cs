using System;
using System.Collections.Generic;
using MyResolutions.Models;
using MyResolutions.Repositories;

namespace MyResolutions.Services
{
  public class HabitsService
  {
    private readonly HabitsRepository _repo;

    public HabitsService(HabitsRepository repo)
    {
      _repo = repo;
    }

    internal Habit GetById(int id)
    {
      Habit habit = _repo.GetById(id);
      if (habit == null)
      {
        throw new Exception("Invalid Habit Id");
      }
      return habit;
    }

    internal Habit Create(Habit newHabit)
    {
      return _repo.Create(newHabit);
    }


    internal Habit Edit(Habit update)
    {
      Habit habit = GetById(update.Id);
      if (habit.CreatorId != update.CreatorId)
      {
        throw new Exception("Invalid Access");
      }
      habit.Description = update.Description != null && update.Description.Trim().Length > 0 ? update.Description : habit.Description;
      habit.isPrivate = update.isPrivate != null ? update.isPrivate : habit.isPrivate;
      _repo.Edit(habit);
      return habit;
    }

    internal void Delete(int id, string userId)
    {
      Habit habit = GetById(id);
      if (habit.CreatorId != userId)
      {
        throw new Exception("Invalid Access");
      }
      _repo.Delete(id);
    }

    internal List<Habit> GetByAccountId(string id)
    {
      return _repo.GetByCreatorId(id);
    }

    internal List<Habit> GetByProfileId(string id)
    {
      List<Habit> habits = _repo.GetByCreatorId(id);
      habits = habits.FindAll(h => h.isPrivate == false);
      return habits;
    }
  }
}