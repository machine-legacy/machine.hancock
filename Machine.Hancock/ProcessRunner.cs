using System;
using System.Diagnostics;

namespace Machine.Hancock
{
  public class ProcessRunner
  {
    public void Run(string path, params string[] args)
    {
      ProcessStartInfo startInfo = new ProcessStartInfo(path);
      startInfo.Arguments = args.Join(" ");
      startInfo.UseShellExecute = false;
      startInfo.CreateNoWindow = true;
      Process process = Process.Start(startInfo);
      if (process == null)
      {
        throw new InvalidOperationException();
      }
      process.WaitForExit();
      if (process.ExitCode != 0)
      {
        throw new InvalidOperationException();
      }
    }
  }
}