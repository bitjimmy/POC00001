using System.ComponentModel;

namespace CCAuction.Domain.Enums
{
    public enum RoleType
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Member")]
        Member,
    }
}
