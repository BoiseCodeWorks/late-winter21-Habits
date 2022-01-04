using System;
using System.Threading.Tasks;
using MyResolutions.Models;
using MyResolutions.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MyResolutions.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly HabitsService _hs;

    public AccountController(AccountService accountService, HabitsService hs)
    {
      _accountService = accountService;
      _hs = hs;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("habits")]
    [Authorize]
    public async Task<ActionResult<List<Habit>>> GetHabits()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Habit> habits = _hs.GetByAccountId(userInfo.Id);
        return Ok(habits);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}