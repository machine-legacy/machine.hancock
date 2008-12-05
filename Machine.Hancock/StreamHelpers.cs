using System;
using System.Collections.Generic;
using System.IO;

namespace Machine.Hancock
{
  public static class StreamHelpers
  {
    public static IEnumerable<string> Lines(this TextReader reader)
    {
      while (true)
      {
        string line = reader.ReadLine();
        if (line == null)
        {
          break;
        }
        yield return line;
      }
    }
  }
}