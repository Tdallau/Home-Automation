using System;

namespace HomeAutomation.Areas.MyRecipes.Interfaces
{
  public interface IMapper<A, B> where A : class where B : class
  {
    public B Convert(A obj, Guid? userId);
    public B Convert(A obj, bool getOne, Guid? userId);
  }
}