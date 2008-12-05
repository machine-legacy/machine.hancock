using System;
using System.Collections.Generic;
using System.IO;

namespace Machine.Hancock
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Configuration configuration = new Configuration(
        new Pathname(@"E:\Source\Machine.Mta\Build\Debug\Hancock"),
        new Pathname(@"E:\Source\Machine.Hancock\Machine.Hancock\Hancock.snk")
      );
      Pathname application = new Pathname(@"E:\Source\MessagingExample\ConsoleApplication1\ConsoleApplication1\bin\Debug\ConsoleApplication1.exe");
      IncomingAssemblyFactory incomingAssemblyFactory = new IncomingAssemblyFactory();
      IncomingAssembly assembly = incomingAssemblyFactory.CreateDependencyGraph(application);
      ICollection<IncomingAssembly> unsigned = assembly.UnsignedAssemblies;
      foreach (IncomingAssembly toBeSigned in unsigned)
      {
        Console.WriteLine("Signing " + toBeSigned.Path.FileName);
      }
      PublicKeyTokenProvider publicKeyTokenProvider = new PublicKeyTokenProvider();
      AssemblySetSigner signer = new AssemblySetSigner(new Assembler(), new Disassembler(), new IlReader(), new IlWriter(publicKeyTokenProvider), publicKeyTokenProvider);
      signer.SignAssemblies(unsigned, configuration);
      Console.ReadKey();
    }
  }
}
