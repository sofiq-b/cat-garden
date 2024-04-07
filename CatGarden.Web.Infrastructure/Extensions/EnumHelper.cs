using System.ComponentModel.DataAnnotations;
using System.Reflection;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var displayAttribute = enumValue.GetType()
                                        .GetMember(enumValue.ToString())
                                        .FirstOrDefault()
                                        ?.GetCustomAttribute<DisplayAttribute>();

        if (displayAttribute != null)
        {
            return displayAttribute.GetName()!;
        }
        else
        {
            return enumValue.ToString(); // Fallback to enum's string representation
        }
    }
}