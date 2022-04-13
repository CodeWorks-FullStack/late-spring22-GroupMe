using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using GroupMe.Interfaces;
using GroupMe.Models;

namespace GroupMe.Repositories
{
  public class GroupsRepository : IRepo<Group, int>
  {
    private readonly IDbConnection _db;

    public GroupsRepository(IDbConnection db)
    {
      _db = db;

    }
    public List<Group> Get()
    {
      string sql = @"
      SELECT 
        g.*,
        a.*
      FROM groups g
      JOIN accounts a ON g.creatorId = a.id;
      ";
      return _db.Query<Group, Profile, Group>(sql, (group, prof) =>
      {
        group.Founder = prof;
        return group;
      }).ToList();
    }
    public Group Get(int id)
    {
      string sql = @"
      SELECT 
        g.*,
        a.*
      FROM groups g
      JOIN accounts a ON g.creatorId = a.id
      WHERE g.id = @id;
      ";
      return _db.Query<Group, Profile, Group>(sql, (gp, prof) =>
      {
        gp.Founder = prof;
        return gp;
      }, new { id }).FirstOrDefault();
    }

    public Group Create(Group groupData)
    {
      string sql = @"
        INSERT INTO groups
        (name, creatorId)
        VALUES
        (@Name, @CreatorId);
        SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, groupData);
      groupData.Id = id;
      return groupData;
    }

    internal List<MemberGroupViewModel> GetByMembershipAccountId(string userId)
    {
      string sql = @"
        SELECT
          a.*,
          g.*,
          m.id AS MembershipId
        FROM memberships m
        JOIN groups g ON g.id = m.groupId
        JOIN accounts a ON a.id = g.creatorId
        WHERE m.accountId = @userId
      ";
      return _db.Query<Account, MemberGroupViewModel, MemberGroupViewModel>(sql, (a, mgvm) =>
      {
        mgvm.Founder = a;
        return mgvm;
      }, new { userId }).ToList();
    }


    public void Update(Group original)
    {
      string sql = @"
      UPDATE groups
      SET
       name = @Name
      WHERE id = @Id;";
      _db.Execute(sql, original);
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM groups WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}
