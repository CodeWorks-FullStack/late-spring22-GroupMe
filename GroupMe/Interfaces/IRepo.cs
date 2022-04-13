using System.Collections.Generic;

namespace GroupMe.Interfaces
{
  public interface IRepo<T, TId>
  {
    List<T> Get();
    T Get(TId id);
    T Create(T data);
    void Update(T data);
    void Delete(TId id);
  }
}