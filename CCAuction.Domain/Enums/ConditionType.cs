using System.ComponentModel;

namespace CCAuction.Domain.Enums
{
    public enum ConditionType
    {
        [Description("New")]
        New = 1,
        [Description("Used")]
        Used,
    }
}
