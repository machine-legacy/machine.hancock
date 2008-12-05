using System;
using System.Collections.Generic;

namespace Machine.Hancock
{
  public class IncomingAssembly
  {
    readonly Pathname _path;
    readonly bool _isSigned;
    readonly bool _inGac;
    readonly List<IncomingAssembly> _referencedAssemblies = new List<IncomingAssembly>();

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

    public IncomingAssembly(Pathname path, bool isSigned, bool inGac)
    {
      _path = path;
      _inGac = inGac;
      _isSigned = isSigned;
    }

    public void Reference(IncomingAssembly assembly)
    {
      _referencedAssemblies.Add(assembly);
    }

    public IEnumerable<IncomingAssembly> ReferencedAssemblies
    {
      get { return _referencedAssemblies; }
    }

    public override string ToString()
    {
      return "Incoming<" + _path + ", " + _isSigned + ">";
    }

    public ICollection<IncomingAssembly> UnGacedAssemblies
    {
      get
      {
        List<IncomingAssembly> visited = new List<IncomingAssembly>();
        List<IncomingAssembly> unsigned = new List<IncomingAssembly>();
        CollectAssemblies(visited, unsigned, incoming => !incoming._inGac);
        return unsigned;
      }
    }

    public ICollection<IncomingAssembly> UnsignedAssemblies
    {
      get
      {
        List<IncomingAssembly> visited = new List<IncomingAssembly>();
        List<IncomingAssembly> unsigned = new List<IncomingAssembly>();
        CollectAssemblies(visited, unsigned, incoming => !incoming._isSigned);
        return unsigned;
      }
    }

    private void CollectAssemblies(ICollection<IncomingAssembly> visited, ICollection<IncomingAssembly> collected, Predicate<IncomingAssembly> condition)
    {
      if (visited.Contains(this))
      {
        return;
      }
      visited.Add(this);
      foreach (IncomingAssembly referenced in _referencedAssemblies)
      {
        referenced.CollectAssemblies(visited, collected, condition);
      }
      if (condition(this))
      {
        collected.Add(this);
      }
    }
  }
}