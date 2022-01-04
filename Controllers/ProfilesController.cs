using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyResolutions.Models;
using MyResolutions.Services;

namespace MyResolutions.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly HabitsService _hs;

    public ProfilesController(ProfilesService ps, HabitsService hs)
    {
      _ps = ps;
      _hs = hs;
    }

    [HttpGet("{id}/habits")]
    public ActionResult<List<Habit>> GetHabits(string id)
    {
      try
      {
        List<Habit> habits = _hs.GetByProfileId(id);
        return Ok(habits);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }

}
