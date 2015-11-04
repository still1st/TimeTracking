using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace TimeTracking.Common.Extensions
{
    public static class EnumExtensions
    {
        public static String GetDescription(this Enum member)
        {
            if (member.GetType().IsEnum == false)
                throw new ArgumentOutOfRangeException("member", "member is not enum");

            FieldInfo fieldInfo = member.GetType().GetField(member.ToString());

            if (fieldInfo == null)
                return null;

            var attributes = fieldInfo.GetCustomAttributes<DescriptionAttribute>(false).ToArray();

            if (attributes.Length > 0)
                return attributes[0].Description;

            return member.ToString();
        }

        public static IEnumerable<KeyValuePair<Int32, String>> ToKeyValuePairs<TEnum>() where TEnum : struct, IConvertible
        {
            if (typeof(TEnum).IsEnum == false)
                throw new ArgumentException("TEnum must be an enumerated type");

            List<KeyValuePair<Int32, String>> items = Enum.GetValues(typeof(TEnum))
                .Cast<Enum>()
                .Select(x => new KeyValuePair<Int32, String>(Int32.Parse(x.ToString("D")), x.GetDescription()))
                .ToList();

            return items;
        }
    }
}
