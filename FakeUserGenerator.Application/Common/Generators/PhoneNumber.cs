using Bogus;
using FakeUserGenerator.Application.Common.Conditions;

namespace FakeUserGenerator.Application.Common.Generators;

public class PhoneNumber
{
    private readonly string _country;
    private readonly Faker _faker;
    
    public PhoneNumber(string country, Faker faker)
    {
        _faker = faker;
        _country = country;
    }

    public string GeneratePhone()
    {
        var phoneCode = PhoneCode.PhoneCodes[_country];
        var phoneNumber = _faker.Phone.PhoneNumber();
        return $"{phoneCode} {phoneNumber}";
    }
}