using Bogus;

namespace FakeUserGenerator.Application.Common.Generators;

public class UserName
{
    private readonly Faker _faker;
    private readonly int _seed;
    private readonly string _country;

    public UserName(string country, int seed)
    {
        _seed = seed;
        _country = country;
        _faker = new Faker(_country)
        { 
            Random = new Randomizer(_seed)
        };
    }

    public string GenerateName()
    {
        var firstName = _faker.Name.FirstName();
        var lastName = _faker.Name.LastName();
        return $"{firstName} {lastName}";
    }
}