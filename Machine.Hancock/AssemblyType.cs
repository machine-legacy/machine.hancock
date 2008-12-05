using System;

namespace Machine.Hancock
{
  public enum AssemblyType
  {
    Dll,
    Exe
  }
  public static class AssemblyTypeHelpers
  {
    public static string ToExtension(this AssemblyType type)
    {
      switch (type)
      {
        case AssemblyType.Dll:
          return "dll";
        case AssemblyType.Exe:
          return "exe";
        default:
          throw new ArgumentException("type");
      }
    }
    public static string ToIlAsmArgument(this AssemblyType type)
    {
      switch (type)
      {
        case AssemblyType.Dll:
          return "/DLL";
        case AssemblyType.Exe:
          return "/EXE";
        default:
          throw new ArgumentException("type");
      }
    }
  }
}