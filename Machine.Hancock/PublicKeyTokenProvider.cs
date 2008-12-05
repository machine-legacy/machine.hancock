using System;
using System.Collections.Generic;

namespace Machine.Hancock
{
  public interface IPublicKeyTokenProvider
  {
    PublicKeyToken LookupPublicKeyToken(string name);
  }
  public class PublicKeyTokenProvider : IPublicKeyTokenProvider
  {
    readonly List<AssembledAssembly> _assemblies = new List<AssembledAssembly>();

    public void Add(AssembledAssembly assembly)
    {
      _assemblies.Add(assembly);
    }

    public PublicKeyToken LookupPublicKeyToken(string name)
    {
      foreach (AssembledAssembly assembly in _assemblies)
      {
        if (assembly.IsNamed(name))
        {
          return assembly.PublicKeyToken;
        }
      }
      return null;
    }
  }
}