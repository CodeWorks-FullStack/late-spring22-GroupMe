using System.Data;
using Dapper;
using GroupMe.Models;

namespace GroupMe.Repositories
{
  public class MembershipsRepository
  {

    private readonly IDbConnection _db;

    public MembershipsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Membership Create(Membership data)
    {
      string sql = @"
        INSERT INTO memberships
        (accountId, groupId)
        VALUES
        (@AccountId, @GroupId);
        SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM memberships WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

    public Membership Get(int id)
    {
      string sql = @"SELECT * FROM memberships WHERE id = @id";
      return _db.QueryFirstOrDefault<Membership>(sql, new { id });
    }

    // get by group id and account id
    internal Membership Get(int groupId, string accountId)
    {
      string sql = @"
      SELECT * FROM memberships
      WHERE groupId = @groupId AND accountId = @accountId
      ";

      return _db.QueryFirstOrDefault<Membership>(sql, new { groupId, accountId });
    }



  }
}