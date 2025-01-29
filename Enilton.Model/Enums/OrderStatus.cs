using System.ComponentModel;

namespace Enilton.Model.Enums
{
    public enum OrderStatus
    {
        [Description("In Progress")]
        InProgress = 1,

        [Description("Done")]
        Done = 2,
    }
}
