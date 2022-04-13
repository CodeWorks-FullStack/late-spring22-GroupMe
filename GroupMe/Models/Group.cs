using System;
using GroupMe.Interfaces;

namespace GroupMe.Models
{
  public class Group : IDbItem<int>
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string CreatorId { get; set; }
    public Profile Founder { get; set; }
  }

  public class MemberGroupViewModel : Group
  {
    public int MembershipId { get; set; }
  }
}