using System.ComponentModel;

namespace CCAuction.Domain.Enums
{
    public enum GenderType
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female,
        [Description("Prefer Not To Disclose")]
        PreferNotToDisclose,
    }
}
