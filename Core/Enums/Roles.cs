namespace Vulns.Core;

public class Role : SmartEnum<Role>
{
    public static readonly Role None = new(nameof(None), 0);
    public static readonly Role User = new(nameof(User), 1);
    public static readonly Role Admin = new(nameof(Admin), 1);

    public Role(string name, int value) : base(name, value) { }
    public Role() : base(nameof(None), 0) { }
}
