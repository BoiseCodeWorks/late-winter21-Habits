using System;
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
  public class TrackedHabitsController : ControllerBase
  {
    private readonly TrackedHabitsService _ths;

    public TrackedHabitsController(TrackedHabitsService ths)
    {
      _ths = ths;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<TrackedHabit>> Create([FromBody] TrackedHabit newTracked)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newTracked.AccountId = userInfo.Id;
        newTracked.CompletedAt = DateTime.Now;
        TrackedHabit tracked = _ths.Create(newTracked);
        return Ok(tracked);
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
        _ths.Delete(id, userInfo.Id);
        return Ok("I didn't believe you when you told me");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}