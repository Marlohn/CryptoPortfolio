using System.ComponentModel;

namespace Application.Models.Enums
{
    public enum RiskLevel
    {
        [Description("None")]
        None = 0,

        [Description("Low")]
        Low = 1,

        [Description("Medium")]
        Medium = 2,

        [Description("High")]
        High = 3,

        [Description("Very High")]
        VeryHigh = 4
    }
}