using System;
using System.IO;
using System.Reflection;

namespace Machine.Hancock
{
  public class Pathname
  {
    readonly private string _value;

    public Pathname(string value)
    {
      _value = value;
    }

    public string FileName
    {
      get { return Path.GetFileName(_value); }
    }

    public string FileNameWithoutExtension
    {
      get { return Path.GetFileNameWithoutExtension(_value); }
    }

    public Pathname Directory
    {
      get { return new Pathname(Path.GetDirectoryName(_value)); }
    }

    public Pathname Join(string other)
    {
      return Join(new Pathname(other));
    }

    public Pathname Join(Pathname other)
    {
      return new Pathname(Path.Combine(_value, other.AsString));
    }

    public string AsString
    {
      get { return _value; }
    }

    public Pathname ChangeFileName(string fileName)
    {
      return new Pathname(Path.Combine(Path.GetDirectoryName(_value), fileName));
    }

    public Pathname ChangeDirectory(Pathname directory)
    {
      return directory.Join(this.FileName);
    }

    public Pathname ChangeExtension(string extension)
    {
      return new Pathname(Path.Combine(Path.GetDirectoryName(_value), Path.GetFileNameWithoutExtension(_value) + "." + extension));
    }

    public override string ToString()
    {
      return _value;
    }
  }
  public static class PathnameHelpers
  {
    public static Pathname Pathname(this Assembly assembly)
    {
      return new Pathname(assembly.Location);
    }
  }
}