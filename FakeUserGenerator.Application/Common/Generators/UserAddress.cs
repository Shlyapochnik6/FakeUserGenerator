using Bogus;
using FakeUserGenerator.Application.Common.Interfaces;

namespace FakeUserGenerator.Application.Common.Generators;

public class UserAddress
{
    private readonly string _country;
    private readonly Faker _faker;
    private readonly IVillagesDbContext _context;
    private readonly Random? _random;
    private readonly int _seed;

    public UserAddress(string country, IVillagesDbContext context,
        int seed)
    {
        _country = country;
        _seed = seed;
        _faker = new Faker(_country)
        {
            Random = new Randomizer(_seed)
        };
        _context = context;
        _random = new Random(_seed);
    }

    public string GenerateAddress()
    {
        if (_random!.Next(1, 3) == 1)
        {
            var apartment = _faker.Address.SecondaryAddress();
            var building = _faker.Address.BuildingNumber();
            var city = _faker.Address.City();
            var direction = _faker.Address.Direction();
            var street = _faker.Address.StreetName();
            return $"{direction}, {city}, {street}, {building}, {apartment}";
        }
        else
        {
            var count = _context.Villages.Count(c => c.Country.Name == _country);
            var street = _faker.Address.StreetName();
            var building = _faker.Address.BuildingNumber();
            var village = _context.Villages
                .Where(v => v.Country.Name == _country)
                .Select(v => v.Name)
                .ToList()
                .ElementAt(_random.Next(count));
            return $"{village}, {street}, {building}";
        }
    }
}