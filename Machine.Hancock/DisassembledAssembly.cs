using System;
using System.Collections.Generic;

namespace Machine.Hancock
{
  public class DisassembledAssembly
  {
    readonly Pathname _path;
    readonly AssemblyType _type;

    public Pathname Path
    {
      get { return _path; }
    }

    public AssemblyType Type
    {
      get { return _type; }
    }

    public DisassembledAssembly(Pathname path, AssemblyType type)
    {
      _path = path;
      _type = type;
    }

    public IEnumerable<Dependency> FindDependencies()
    {
      return null;
    }
  }
}