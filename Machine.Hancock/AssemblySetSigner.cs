using System;
using System.Collections.Generic;

namespace Machine.Hancock
{
  public class AssemblySetSigner
  {
    readonly Assembler _assembler;
    readonly Disassembler _disassembler;
    readonly IlReader _ilReader;
    readonly IlWriter _ilWriter;
    readonly PublicKeyTokenProvider _publicKeyTokenProvider;

    public AssemblySetSigner(Assembler assembler, Disassembler disassembler, IlReader ilReader, IlWriter ilWriter, PublicKeyTokenProvider publicKeyTokenProvider)
    {
      _assembler = assembler;
      _disassembler = disassembler;
      _ilReader = ilReader;
      _ilWriter = ilWriter;
      _publicKeyTokenProvider = publicKeyTokenProvider;
    }

    public void SignAssemblies(IEnumerable<IncomingAssembly> assemblies, Configuration configuration)
    {
      foreach (IncomingAssembly incoming in assemblies)
      {
        Console.WriteLine("Disassembling " + incoming.Path.FileName);
        DisassembledAssembly disassembledAssembly = _disassembler.Disassemble(incoming, configuration);
        Console.WriteLine("Transforming");
        DisassembledIl il = _ilReader.Read(disassembledAssembly.Path);
        _ilWriter.Write(il, disassembledAssembly.Path);
        Console.WriteLine("Assembling");
        AssembledAssembly assembledAssembly = _assembler.Assemble(disassembledAssembly, configuration);
        _publicKeyTokenProvider.Add(assembledAssembly);
        Console.WriteLine("Done with " + incoming.Path.FileName);
      }
    }
  }
}