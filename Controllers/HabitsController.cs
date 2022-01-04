using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyResolutions.Models;
using MyResolutions.Services;

namespace MyResolutions.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class HabitsController : ControllerBase
  {
    private readonly HabitsService _hs;
    private readonly TrackedHabitsService _ths;

    public HabitsController(HabitsService hs, TrackedHabitsService ths)
    {
      _hs = hs;
      _ths = ths;
    }

    [HttpGet("{id}/tracked")]
    public ActionResult<List<TrackedHabitProfile>> GetProfilesThatTrackedThisHabit(int id)
    {
      try
      {
        List<TrackedHabitProfile> profilesTracked = _ths.GetTrackedProfilesByHabitId(id);
        return Ok(profilesTracked);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }



    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Habit>> Create([FromBody] Habit newHabit)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newHabit.CreatorId = userInfo.Id;
        Habit habit = _hs.Create(newHabit);
        return Ok(habit);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Habit>> Edit([FromBody] Habit update, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        update.CreatorId = userInfo.Id;
        update.Id = id;
        Habit habit = _hs.Edit(update);
        return Ok(habit);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _hs.Delete(id, userInfo.Id);
        return Ok("It was a bad habit anyway");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}