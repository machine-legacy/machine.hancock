using System.IO;

namespace Machine.Hancock
{
  public class DisassembledIl
  {
    readonly string _body;

    public DisassembledIl(string body)
    {
      _body = body;
    }

    public TextReader OpenStream()
    {
      return new StringReader(_body);
    }
  }
}