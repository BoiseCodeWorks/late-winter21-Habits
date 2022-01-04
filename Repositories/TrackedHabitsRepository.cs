using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MyResolutions.Models;

namespace MyResolutions.Repositories
{
  public class TrackedHabitsRepository
  {
    private readonly IDbConnection _db;

    public TrackedHabitsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal TrackedHabit Create(TrackedHabit newTracked)
    {
      string sql = @"
      INSERT INTO trackedhabits
      (accountId, habitId, completedAt)
      VALUES
      (@AccountId, @HabitId, @CompletedAt);
      SELECT LAST_INSERT_ID();";

      int id = _db.ExecuteScalar<int>(sql, newTracked);
      newTracked.Id = id;
      return newTracked;
    }

    internal void Delete(int id)
    {
      string sql = @"DELETE FROM trackedhabits WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

    internal TrackedHabit GetById(int id)
    {
      string sql = "SELECT * FROM trackedhabits WHERE id = @id";
      return _db.QueryFirstOrDefault<TrackedHabit>(sql, new { id });
    }

    // Given a habit id
    // return all profiles and the tracked information that is connected to that habit

    internal List<TrackedHabitProfile> GetTrackedProfilesByHabitId(int id)
    {
      string sql = @"
      SELECT
       a.*,
       th.id AS TrackedHabitId,
       th.CompletedAt
      FROM trackedhabits th
      JOIN accounts a ON a.id = th.accountId
      WHERE th.habitId = @id;
      ";

      return _db.Query<TrackedHabitProfile>(sql, new { id }).ToList();
    }

    // given a account Id 
    // return all the habits they have tracked and the habits creator

    internal List<TrackedHabitHabitViewModel> GetAllTrackedHabitsByAccountId(string id)
    {
      string sql = @"
            SELECT
                h.*,
                th.id AS TrackedHabitId,
                a.*
            FROM trackedhabits th
            JOIN habits h ON h.id = th.habitId
            JOIN account a ON h.creatorId = a.id
            WHERE th.accountId = @id;";
      return _db.Query<TrackedHabitHabitViewModel, Profile, TrackedHabitHabitViewModel>(sql, (thhvm, prof) =>
      {
        thhvm.Creator = prof;
        return thhvm;
      }, new { id }).ToList();
    }


  }
}