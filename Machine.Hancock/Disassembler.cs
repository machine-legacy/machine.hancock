using System;

namespace Machine.Hancock
{
  public class Disassembler
  {
    public DisassembledAssembly Disassemble(IncomingAssembly assembly, Configuration configuration)
    {
      ProcessRunner runner = new ProcessRunner();
      Pathname ilPath = assembly.Path.ChangeDirectory(configuration.OutputDirectory).ChangeExtension("il");
      runner.Run(configuration.SDK + @"\ILDASM.exe", "/NOBAR", assembly.Path.AsString, "/OUT:" + ilPath);
      return new DisassembledAssembly(ilPath, assembly.Type);
    }
  }
}