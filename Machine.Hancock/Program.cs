using System;
using System.Collections.Generic;

namespace Machine.Hancock
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CommandLine commandLine = new CommandLine(args);
      if (!commandLine.IsValid())
      {
        Environment.Exit(1);
        return;
      }
      Configuration configuration = commandLine.ToConfiguration();
      IncomingAssemblyFactory incomingAssemblyFactory = new IncomingAssemblyFactory();
      IncomingAssembly assembly = incomingAssemblyFactory.CreateDependencyGraph(commandLine.RootAssembly);
      ICollection<IncomingAssembly> unsigned = assembly.UnsignedAssemblies;
      foreach (IncomingAssembly toBeCopied in assembly.UnGacedAssemblies)
      {
        Console.WriteLine("Copying " + toBeCopied.Path.FileName);
      }
      foreach (IncomingAssembly toBeSigned in unsigned)
      {
        Console.WriteLine("Signing " + toBeSigned.Path.FileName);
      }
      PublicKeyTokenProvider publicKeyTokenProvider = new PublicKeyTokenProvider();
      AssemblySetSigner signer = new AssemblySetSigner(new Assembler(), new Disassembler(), new IlReader(), new IlWriter(publicKeyTokenProvider), publicKeyTokenProvider);
      signer.SignAssemblies(unsigned, configuration);
    }
  }
}
