using System;

namespace Machine.Hancock
{
  public class CommandLine
  {
    private readonly string[] _args;
    private Pathname _outputDirectory;
    private Pathname _rootAssembly;
    private Pathname _keyFile;

    public Pathname RootAssembly
    {
      get { return _rootAssembly; }
    }

    public Configuration ToConfiguration()
    {
      return new Configuration(_outputDirectory, _keyFile);
    }

    public CommandLine(string[] args)
    {
      _args = args;
    }

    public bool IsValid()
    {
      if (_args.Length != 3)
      {
        Console.WriteLine("{0}: <root-assembly> <key-file> <output-directory>", "Machine.Hancock.exe");
        return false;
      }
      _rootAssembly = new Pathname(_args[0]);
      if (!_rootAssembly.IsFile)
      {
        Console.WriteLine("Root assembly is missing!");
        return false;
      }
      _keyFile = new Pathname(_args[1]);
      if (!_keyFile.IsFile)
      {
        Console.WriteLine("Key file is missing!");
        return false;
      }
      _outputDirectory = new Pathname(_args[2]);
      if (!_outputDirectory.IsDirectory)
      {
        Console.WriteLine("Output directory is missing!");
        return false;
      }
      return true;
    }
  }
}