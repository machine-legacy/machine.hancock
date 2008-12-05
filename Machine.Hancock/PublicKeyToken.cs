using System;
using System.Text;

namespace Machine.Hancock
{
  public class PublicKeyToken
  {
    readonly byte[] _value;

    public PublicKeyToken(byte[] value)
    {
      _value = value;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      foreach (byte b in _value)
      {
        sb.Append(b.ToString("X2")).Append(" ");
      }
      return sb.ToString();
    }
  }
}