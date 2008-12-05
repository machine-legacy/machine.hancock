using System;

namespace Machine.Hancock
{
  public class Disassembler
  {
    public DisassembledAssembly Disassemble(IncomingAssembly assembly, Pathname outputDirectory)
    {
      ProcessRunner runner = new ProcessRunner();
      Pathname ilPath = assembly.Path.ChangeDirectory(outputDirectory).ChangeExtension("il");
      runner.Run(Program.SDK + @"\ILDASM.exe", assembly.Path.AsString, "/OUT:" + ilPath);
      return new DisassembledAssembly(ilPath, assembly.Type);
    }
  }
}