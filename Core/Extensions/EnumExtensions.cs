using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Serialization;

namespace Vulns.Core;
public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null) return value.ToString();

        var attr = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
        if (attr == null) return value.ToString();

        var displayAttr = attr as DisplayAttribute;
        if (displayAttr == null) return value.ToString();

        return displayAttr.GetName() ?? value.ToString();
    }

    public static string GetXmlEnum(this Enum value)
    {
        Type type = value.GetType();
        string? name = Enum.GetName(type, value);
        if (name != null)
        {
            FieldInfo? field = type.GetField(name);
            if (field != null)
            {
                XmlEnumAttribute? attr = Attribute
                    .GetCustomAttribute(field, typeof(XmlEnumAttribute))
                    as XmlEnumAttribute;
                if (attr != null && attr.Name != null)
                    return attr.Name;
            }
        }
        return value.ToString();
    }
}