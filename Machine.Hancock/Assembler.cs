using System;
using System.Reflection;

namespace Machine.Hancock
{
  public class Assembler
  {
    public AssembledAssembly Assemble(DisassembledAssembly assembly, Configuration configuration)
    {
      ProcessRunner runner = new ProcessRunner();
      Pathname binaryPath = assembly.Path.ChangeDirectory(configuration.OutputDirectory).ChangeExtension(assembly.Type.ToExtension());
      runner.Run(configuration.Framework + @"\ILASM.exe", assembly.Path.AsString, "/QUIET", "/DEBUG", assembly.Type.ToIlAsmArgument(), "/KEY:" + configuration.KeyFile.AsString);
      PublicKeyToken publicKeyToken = ReadAssemblyPublicKeyToken(binaryPath);
      return new AssembledAssembly(binaryPath, publicKeyToken);
    }

    private static PublicKeyToken ReadAssemblyPublicKeyToken(Pathname path)
    {
      Assembly assembly = Assembly.ReflectionOnlyLoadFrom(path.AsString);
      byte[] publicKeyToken = assembly.GetName().GetPublicKeyToken();
      return new PublicKeyToken(publicKeyToken);
    }
  }
}