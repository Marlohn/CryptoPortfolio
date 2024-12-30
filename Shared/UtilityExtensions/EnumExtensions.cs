using System.ComponentModel;
using System.Reflection;
using Ardalis.GuardClauses;

namespace UtilityExtensions
{
    public static class EnumExtensions
    {
        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct, Enum
        {
            Guard.Against.NullOrEmpty(value, nameof(value));

            if (Enum.TryParse(value, true, out TEnum result))
            {
                return result;
            }

            foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>(false);
                if (attribute != null && attribute.Description.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    return (TEnum)field.GetValue(null)!;
                }
            }

            throw new ArgumentException($"The provided string '{value}' cannot be converted to {typeof(TEnum).Name}.", nameof(value));
        }

        public static string GetDescription(this Enum enumValue)
        {
            Guard.Against.Null(enumValue, nameof(enumValue));

            var field = enumValue.GetType().GetField(enumValue.ToString())!;
            var attribute = field.GetCustomAttribute<DescriptionAttribute>(false);

            return attribute?.Description ?? enumValue.ToString();
        }
    }
}
