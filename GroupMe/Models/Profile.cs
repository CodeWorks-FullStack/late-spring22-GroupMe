using System;
using GroupMe.Interfaces;

namespace GroupMe.Models
{
  public class Profile : IDbItem<string>
  {
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
  }

  public class GroupMemberViewModel : Profile
  {
    public int MembershipId { get; set; }
  }
}