using System;
using System.Collections.Generic;
using System.Reflection;

namespace Machine.Hancock
{
  public class IncomingAssemblyFactory
  {
    public IncomingAssembly CreateDependencyGraph(Pathname path)
    {
      Dictionary<string, IncomingAssembly> map = new Dictionary<string, IncomingAssembly>();
      Assembly assembly = Assembly.ReflectionOnlyLoadFrom(path.AsString);
      return CreateDependencyGraph(map, assembly);
    }

    private static IncomingAssembly CreateDependencyGraph(IDictionary<string, IncomingAssembly> map, Assembly assembly)
    {
      if (String.IsNullOrEmpty(assembly.FullName))
      {
        throw new InvalidOperationException();
      }
      if (map.ContainsKey(assembly.FullName))
      {
        return map[assembly.FullName];
      }
      byte[] publicKeyToken = assembly.GetName().GetPublicKeyToken();
      bool inGac = assembly.GlobalAssemblyCache;
      IncomingAssembly incomingAssembly = new IncomingAssembly(assembly.Pathname(), publicKeyToken.Length > 0, inGac);
      map[assembly.FullName] = incomingAssembly; 
      foreach (AssemblyName name in assembly.GetReferencedAssemblies())
      {
        Assembly referenced;
        try
        {
          referenced = Assembly.ReflectionOnlyLoad(name.ToString());
        }
        catch (Exception error)
        {
          referenced = Assembly.ReflectionOnlyLoadFrom(incomingAssembly.Path.ChangeFileName(name.Name + ".dll").AsString);
        }
        incomingAssembly.Reference(CreateDependencyGraph(map, referenced));
      }
      return incomingAssembly;
    }
  }
}