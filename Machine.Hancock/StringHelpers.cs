using System;
using System.Text;

namespace Machine.Hancock
{
  public static class StringHelpers
  {
    public static string Join(this string[] values, string separator)
    {
      StringBuilder sb = new StringBuilder();
      foreach (string value in values)
      {
        if (sb.Length != 0)
        {
          sb.Append(separator);
        }
        sb.Append(value);
      }
      return sb.ToString();
    }
  }
}