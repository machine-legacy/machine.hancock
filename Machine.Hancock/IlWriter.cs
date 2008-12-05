using System;
using System.IO;

namespace Machine.Hancock
{
  public class IlWriter : IlReader
  {
    readonly PublicKeyTokenProvider _publicKeyTokenProvider;

    public IlWriter(PublicKeyTokenProvider publicKeyTokenProvider)
    {
      _publicKeyTokenProvider = publicKeyTokenProvider;
    }

    public void Write(DisassembledIl il, Pathname path)
    {
      using (StreamWriter writer = new StreamWriter(path.AsString))
      {
        using (TextReader reader = il.OpenStream())
        {
          AssemblyHeaderParser parser = null;
          foreach (string line in reader.Lines())
          {
            if (line.StartsWith(".assembly extern"))
            {
              parser = new AssemblyHeaderParser(_publicKeyTokenProvider, line.Split(' ')[2]);
            }
            if (parser != null)
            {
              if (!parser.Line(line, writer))
              {
                parser = null;
              }
            }
            writer.WriteLine(line);
          }
        }
      }
    }

    class AssemblyHeaderParser
    {
      readonly PublicKeyTokenProvider _publicKeyTokenProvider;
      readonly string _name;
      bool _isSigned;

      public AssemblyHeaderParser(PublicKeyTokenProvider publicKeyTokenProvider, string name)
      {
        _name = name;
        _publicKeyTokenProvider = publicKeyTokenProvider;
      }

      public bool Line(string line, TextWriter writer)
      {
        if (line.Trim().StartsWith(".publickeytoken"))
        {
          _isSigned = true;
        }
        if (line == "}")
        {
          if (!_isSigned)
          {
            PublicKeyToken publicKeyToken = _publicKeyTokenProvider.LookupPublicKeyToken(_name);
            if (publicKeyToken == null)
            { 
              throw new InvalidOperationException();
            }
            writer.WriteLine("  .publickeytoken = ({0})", publicKeyToken);
          }
          return false;
        }
        return true;
      }
    }
  }
}