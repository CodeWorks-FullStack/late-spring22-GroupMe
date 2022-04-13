using System;
using GroupMe.Models;
using GroupMe.Repositories;

namespace GroupMe.Services
{
  public class MembershipsService
  {
    private readonly MembershipsRepository _repo;

    public MembershipsService(MembershipsRepository repo)
    {
      _repo = repo;
    }

    private Membership Get(int id)
    {
      Membership found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid ID");
      }
      return found;
    }

    internal Membership Create(Membership membershipData)
    {
      Membership exists = _repo.Get(membershipData.GroupId, membershipData.AccountId);
      if (exists != null)
      {
        return exists;
      }
      return _repo.Create(membershipData);

    }

    internal void Delete(int membershipId, string userId)
    {
      Membership found = Get(membershipId);
      if (found.AccountId != userId)
      {
        throw new Exception("You do not have permission to do that");
      }
      _repo.Delete(membershipId);
    }
  }
}