using System;
using GroupMe.Interfaces;

namespace GroupMe.Models
{
  public class Membership : IDbItem<int>
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int GroupId { get; set; }
    public string AccountId { get; set; }
  }
}