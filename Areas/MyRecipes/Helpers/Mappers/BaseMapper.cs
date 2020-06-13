using System;
using HomeAutomation.Areas.MyRecipes.Interfaces;

namespace HomeAutomation.Areas.MyRecipes.Helpers.Mappers
{
    public class BaseMapper<A, B> : IMapper<A, B> where A : class where B : class, new()
    {
        public virtual B Convert(A obj, Guid? userId)
        {
            var typeOfA = obj.GetType();
            var propertiesOfA = typeOfA.GetProperties();

            var objB = new B();

            foreach (var propertyOfA in propertiesOfA)
            {
                if (typeof(B).GetProperty(propertyOfA.Name) != null)
                {
                    var value = propertyOfA.GetValue(obj, null);
                    try
                    {
                        objB.GetType().GetProperty(propertyOfA.Name).SetValue(objB, value);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Somthing went wrong by inputing te value {value} by the key {propertyOfA.Name}");
                    }

                }
            }
            return objB;
        }

        public virtual B Convert(A obj, bool getOne, Guid? userId)
        {
            var typeOfA = obj.GetType();
            var propertiesOfA = typeOfA.GetProperties();

            var objB = new B();

            foreach (var propertyOfA in propertiesOfA)
            {
                if (typeof(B).GetProperty(propertyOfA.Name) != null)
                {
                    var value = propertyOfA.GetValue(obj, null);
                    try
                    {
                        objB.GetType().GetProperty(propertyOfA.Name).SetValue(objB, value);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Somthing went wrong by inputing te value {value} by the key {propertyOfA.Name}");
                    }

                }
            }
            return objB;
        }
    }
}