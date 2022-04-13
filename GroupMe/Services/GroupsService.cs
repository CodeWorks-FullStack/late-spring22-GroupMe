using System;
using System.Collections.Generic;
using GroupMe.Models;
using GroupMe.Repositories;

namespace GroupMe.Services
{
  public class GroupsService
  {
    private readonly GroupsRepository _repo;

    public GroupsService(GroupsRepository repo)
    {
      _repo = repo;
    }
    internal List<Group> Get()
    {
      return _repo.Get();
    }

    internal Group Get(int id)
    {
      return _repo.Get(id);
    }

    internal Group Create(Group groupData)
    {
      return _repo.Create(groupData);
    }

    internal Group Update(Group groupData)
    {
      Group original = Get(groupData.Id);
      ValidateOwner(groupData.CreatorId, original);
      original.Name = groupData.Name ?? original.Name;
      _repo.Update(original);
      return original;
    }

    internal void Delete(int id, string userId)
    {
      Group original = Get(id);
      ValidateOwner(userId, original);
      _repo.Delete(id);
    }


    private static void ValidateOwner(string userId, Group data)
    {
      if (userId != data.CreatorId)
      {
        throw new Exception("You Cannot Edit a group you did not create");
      }
    }

    internal List<MemberGroupViewModel> GetUserGroups(string userId)
    {
      return _repo.GetByMembershipAccountId(userId);
    }


  }
}