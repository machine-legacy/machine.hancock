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

    public string Framework
    {
      get { return @"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727"; }
    }
    
    public string SDK
    {
      get { return @"C:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin"; }
    }

    public Configuration(Pathname outputDirectory, Pathname keyFile)
    {
      _outputDirectory = outputDirectory;
      _keyFile = keyFile;
    }
  }
}
