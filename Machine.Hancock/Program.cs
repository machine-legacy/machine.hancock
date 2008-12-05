using System;
using System.IO;

namespace Machine.Hancock
{
  public class Program
  {
    public static string Framework = @"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727";
    public static string SDK = @"C:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin";

    public static void Main(string[] args)
    {
      Pathname key = new Pathname(@"E:\Source\Machine.Hancock\Machine.Hancock\Hancock.snk");
      if (!File.Exists(key.AsString))
      {
        throw new FileNotFoundException();
      }
      Pathname outputDirectory = new Pathname(@"E:\Source\Machine.Mta\Build\Debug\Hancock");
      if (!Directory.Exists(outputDirectory.AsString))
      {
        Directory.CreateDirectory(outputDirectory.AsString);
      }

      Pathname[] paths = new Pathname[]
      {
        new Pathname(@"E:\Source\Machine.Mta\Build\Debug\MassTransit.ServiceBus.dll"),
        new Pathname(@"E:\Source\Machine.Mta\Build\Debug\MassTransit.ServiceBus.MSMQ.dll"),
        new Pathname(@"E:\Source\Machine.Mta\Build\Debug\Machine.Mta.dll")
      };
      PublicKeyTokenProvider publicKeyTokenProvider = new PublicKeyTokenProvider();
      AssemblySetSigner signer = new AssemblySetSigner(new Assembler(), new Disassembler(), new IlReader(), new IlWriter(publicKeyTokenProvider), publicKeyTokenProvider);
      signer.SignAssemblies(paths, outputDirectory, key);
      Console.ReadKey();
    }
  }
}
