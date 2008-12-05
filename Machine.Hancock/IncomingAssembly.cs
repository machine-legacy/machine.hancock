using System;

namespace Machine.Hancock
{
  public class IncomingAssembly
  {
    readonly Pathname _path;

    public Pathname Path
    {
      get { return _path; }
    }
    
    public AssemblyType Type
    {
      get
      {
        if (_path.AsString.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase))
        {
          return AssemblyType.Dll;
        }
        return AssemblyType.Exe;
      }
    }

    public IncomingAssembly(Pathname path)
    {
      _path = path;
    }
  }
}