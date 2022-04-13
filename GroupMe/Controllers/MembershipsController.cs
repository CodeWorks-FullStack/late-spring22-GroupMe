using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using GroupMe.Models;
using GroupMe.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupMe.Controllers
{
  [ApiController]
  [Authorize]
  [Route("api/[controller]")]
  public class MembershipsController : ControllerBase
  {
    private readonly MembershipsService _ms;

    public MembershipsController(MembershipsService ms)
    {
      _ms = ms;
    }


    // Create
    [HttpPost]
    public async Task<ActionResult<Membership>> Create([FromBody] Membership membershipData)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        membershipData.AccountId = user.Id;
        Membership created = _ms.Create(membershipData);
        return Ok(created);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    // Delete
    // Create
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        _ms.Delete(id, user.Id);
        return Ok("Delorted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }
}