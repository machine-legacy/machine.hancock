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

    public void SignAssemblies(IEnumerable<Pathname> paths, Configuration configuration)
    {
      foreach (Pathname path in paths)
      {
        IncomingAssembly incoming = new IncomingAssembly(path);
        DisassembledAssembly disassembledAssembly = _disassembler.Disassemble(incoming, configuration);
        DisassembledIl il = _ilReader.Read(disassembledAssembly.Path);
        _ilWriter.Write(il, disassembledAssembly.Path);
        AssembledAssembly assembledAssembly = _assembler.Assemble(disassembledAssembly, configuration);
        _publicKeyTokenProvider.Add(assembledAssembly);
      }
    }
  }
}