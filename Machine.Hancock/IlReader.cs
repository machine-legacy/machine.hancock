using System.IO;

namespace Machine.Hancock
{
  public class IlReader
  {
    public DisassembledIl Read(Pathname path)
    {
      using (StringWriter writer = new StringWriter())
      {
        using (StreamReader reader = new StreamReader(path.AsString))
        {
          foreach (string line in reader.Lines())
          {
            writer.WriteLine(line);
          }
        }
        return new DisassembledIl(writer.ToString());
      }
    }
  }
}
