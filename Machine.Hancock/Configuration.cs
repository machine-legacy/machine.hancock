using System;
using System.Collections.Generic;

namespace Machine.Hancock
{
  public class Configuration
  {
    private readonly Pathname _outputDirectory;
    private readonly Pathname _keyFile;

    public Pathname OutputDirectory
    {
      get { return _outputDirectory; }
    }

    public Pathname KeyFile
    {
      get { return _keyFile; }
    }

    private static Pathname Framework
    {
      get { return new Pathname(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727"); }
    }
    
    private static Pathname SDK
    {
      get { return new Pathname(@"C:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin"); }
    }

    public Pathname IlDasmPath
    {
      get { return SDK.Join(@"ILDASM.exe"); }
    }

    public Pathname IlAsmPath
    {
      get { return Framework.Join(@"ILASM.exe"); }
    }

    public Configuration(Pathname outputDirectory, Pathname keyFile)
    {
      _outputDirectory = outputDirectory;
      _keyFile = keyFile;
    }
  }
}
