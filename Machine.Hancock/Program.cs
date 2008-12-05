using System;
using System.IO;

namespace Machine.Hancock
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Configuration configuration = new Configuration(new Pathname(@"E:\Source\Machine.Mta\Build\Debug\Hancock"), new Pathname(@"E:\Source\Machine.Hancock\Machine.Hancock\Hancock.snk"));
      Pathname[] paths = new Pathname[]
      {
        new Pathname(@"E:\Source\Machine.Mta\Build\Debug\MassTransit.ServiceBus.dll"),
        new Pathname(@"E:\Source\Machine.Mta\Build\Debug\MassTransit.ServiceBus.MSMQ.dll"),
        new Pathname(@"E:\Source\Machine.Mta\Build\Debug\Machine.Mta.dll")
      };
      PublicKeyTokenProvider publicKeyTokenProvider = new PublicKeyTokenProvider();
      AssemblySetSigner signer = new AssemblySetSigner(new Assembler(), new Disassembler(), new IlReader(), new IlWriter(publicKeyTokenProvider), publicKeyTokenProvider);
      signer.SignAssemblies(paths, configuration);
      Console.ReadKey();
    }
  }
}
