using System;

namespace Machine.Hancock
{
  public class Dependency
  {
    readonly string _name;
    readonly bool _isSigned;

    public string Name
    {
      get { return _name; }
    }

    public bool IsSigned
    {
      get { return _isSigned; }
    }

    public Dependency(string name, bool hasSignature)
    {
      _name = name;
      _isSigned = hasSignature;
    }

    public override string ToString()
    {
      return "Dependency<" + _name + ", " + _isSigned + ">";
    }
  }
}