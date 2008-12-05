using System;
using System.Reflection;

namespace Machine.Hancock
{
  public class Assembler
  {
    public AssembledAssembly Assemble(DisassembledAssembly assembly, Pathname outputDirectory, Pathname key)
    {
      ProcessRunner runner = new ProcessRunner();
      Pathname binaryPath = assembly.Path.ChangeDirectory(outputDirectory).ChangeExtension(assembly.Type.ToExtension());
      runner.Run(Program.Framework + @"\ILASM.exe", assembly.Path.AsString, "/DEBUG", assembly.Type.ToIlAsmArgument(), "/KEY:" + key.AsString);
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