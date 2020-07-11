using System.Runtime.CompilerServices;
using System;
using HomeAutomation.Models.Base;
using System.Collections.Generic;
using System.Linq;

namespace HomeAutomation.Helpers
{
  public static class HelperBox
  {
    public static int GetRandomNumber(int min = 0, int max = 100)
    {
      var random = new Random();
      return random.Next(min, max + 1);
    }

    public static Response<T> DataToResponse<T>(bool success, T data, string error = null)
    {
        if(success) {
            return new Response<T>() {
                Data = data,
                Success = success
            };
        }

        return new Response<T>() {
            Error = error,
            Success = success
        };
    }

    public static Response<ResponseList<T>> DataToReponseList<T>(bool success, IEnumerable<T> data, string error = null)
    {
      return HelperBox.DataToResponse<ResponseList<T>>(success, new ResponseList<T>()
          {
            Count = data.Count(),
            List = data
          }, error);
    }

  }
}