using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions
{
    public static class EnumHelper
    {
        public static TEnum ToEnum<TEnum>(this string enumText, Exception? exception = null)
            where TEnum : struct
        {
            if ((!Enum.TryParse<TEnum>(enumText, true, out var res) || !Enum.IsDefined(typeof(TEnum), res)) && exception != null)
            {
                throw exception;
            }

            return res;
        }

        public static IEnumerable<int> GetAllValues<TEnum>()
            where TEnum : struct, Enum
        {
            foreach (var i in Enum.GetValues(typeof(TEnum)))
            {
                yield return (int)i;
            }
        }

        public static IEnumerable<string> GetAllMembers<TEnum>()
            where TEnum : struct, Enum
        {
            foreach (var i in Enum.GetValues(typeof(TEnum)))
            {
                var item = Enum.Parse<TEnum>(i.ToString());
                yield return item.ToEnumMemberAttrValue();
            }
        }

        public static string ToEnumMemberAttrValue(this Enum @enum)
        {
            var attr =
                @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.
                    GetCustomAttributes(false).OfType<EnumMemberAttribute>().
                    FirstOrDefault();

            if (attr != null)
            {
                return attr?.Value ?? throw new InvalidOperationException($"Unable to get {nameof(EnumMemberAttribute)} value for enum: {@enum}");
            }
            else
            {
                return @enum.ToString();
            }
        }

        public static T? GetAttribute<T>(this Enum @enum)
        {
            var attr = @enum?.GetType()?.GetMember(@enum.ToString())?.FirstOrDefault()
                 ?.GetCustomAttributes(false)
                 ?.OfType<T>();

            if (attr == null)
            {
                throw new InvalidOperationException($"Unable to get {nameof(T)} attribute for enum: {@enum}");
            }

            return attr.FirstOrDefault();
        }
    }
}
