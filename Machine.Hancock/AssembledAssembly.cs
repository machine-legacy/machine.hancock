using System;

namespace Machine.Hancock
{
  public class AssembledAssembly
  {
    readonly Pathname _path;
    readonly PublicKeyToken _publicKeyToken;

    public Pathname Path
    {
      get { return _path; }
    }

    public PublicKeyToken PublicKeyToken
    {
      get { return _publicKeyToken; }
    }

    public string Name
    {
      get { return _path.FileNameWithoutExtension; }
    }

    public bool IsNamed(string name)
    {
      return this.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase);
    }

    public AssembledAssembly(Pathname path, PublicKeyToken publicKeyToken)
    {
      _path = path;
      _publicKeyToken = publicKeyToken;
    }
  }
}