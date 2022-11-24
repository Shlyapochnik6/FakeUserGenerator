namespace FakeUserGenerator.Application.Common.Conditions;

public class PhoneCode
{
    public static readonly Dictionary<string, string> PhoneCodes
        = new()
        {
            { "en_CA", "+1" },
            { "ja", "+81" },
            { "sv", "+46" }
        };
}