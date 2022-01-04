using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MyResolutions.Models;

namespace MyResolutions.Repositories
{
  public class HabitsRepository
  {
    private readonly IDbConnection _db;

    public HabitsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Habit Create(Habit newHabit)
    {
      string sql = @"
      INSERT INTO habits
      (description, isPrivate, creatorId)
      VALUES
      (@Description, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID();";

      int id = _db.ExecuteScalar<int>(sql, newHabit);
      newHabit.Id = id;
      return newHabit;
    }
    // NOTE one-to-many populate
    internal Habit GetById(int id)
    {
      string sql = @"
      SELECT 
        h.*,
        a.* 
      FROM habits h
      JOIN accounts a ON h.creatorId = a.id
      WHERE h.id = @id;";
      return _db.Query<Habit, Profile, Habit>(sql, (habit, prof) =>
      {
        habit.Creator = prof;
        return habit;
      }, new { id }).FirstOrDefault();
    }

    internal void Edit(Habit habit)
    {
      string sql = @"
        UPDATE habits
        SET 
         description = @Description,
         isPrivate = @IsPrivate
        WHERE id = @Id;";
      _db.Execute(sql, habit);
    }

    internal void Delete(int id)
    {
      string sql = @"DELETE FROM habits WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

    internal List<Habit> GetByCreatorId(string id)
    {
      string sql = @"
      SELECT 
        h.*,
        a.* 
      FROM habits h
      JOIN accounts a ON h.creatorId = a.id
      WHERE h.creatorId = @id;";
      return _db.Query<Habit, Profile, Habit>(sql, (habit, prof) =>
      {
        habit.Creator = prof;
        return habit;
      }, new { id }).ToList();
    }
  }
}