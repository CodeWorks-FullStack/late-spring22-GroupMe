using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using GroupMe.Models;
using GroupMe.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupMe.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class GroupsController : ControllerBase
  {

    private readonly GroupsService _bs;
    private readonly AccountService _as;

    public GroupsController(GroupsService bs, AccountService @as)
    {
      _bs = bs;
      _as = @as;
    }

    [HttpGet]
    public ActionResult<List<Group>> Get()
    {
      try
      {
        List<Group> groups = _bs.Get();
        return Ok(groups);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Group> Get(int id)
    {
      try
      {
        Group group = _bs.Get(id);
        return Ok(group);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/memberships")]
    public ActionResult<List<GroupMemberViewModel>> GetMembers(int id)
    {
      try
      {
        List<GroupMemberViewModel> members = _as.GetGroupMembers(id);
        return Ok(members);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Group>> CreateAsync([FromBody] Group groupData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        groupData.CreatorId = userInfo.Id;
        Group group = _bs.Create(groupData);
        group.Founder = userInfo;
        return Created($"api/groups/{group.Id}", group);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Group>> UpdateAsync(int id, [FromBody] Group groupData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        groupData.Id = id;
        groupData.CreatorId = userInfo.Id;
        Group group = _bs.Update(groupData);
        return Ok(group);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteAsync(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _bs.Delete(id, userInfo.Id);
        return Ok("Delorted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}