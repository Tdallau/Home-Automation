using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HomeAutomation.Areas.TwitterStayInformd.Helpers
{
  public class StringCipher
  {
    public static string encodingWinDataProtection(string password)
    {

      return Convert.ToBase64String(Encoding.Unicode.GetBytes(password));
    }

    public static string decodingWinDataProtection(string password)
    {
      return Encoding.Unicode.GetString(Convert.FromBase64String(password));
    }
  }
}