using Bogus;
using FakeUserGenerator.Application.Common.Generators;
using FakeUserGenerator.Application.Common.Interfaces;
using MediatR;

namespace FakeUserGenerator.Application.CQs.User.Queries.GetUsersList;

public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, List<UserDto>>
{
    private readonly IVillagesDbContext _dbContext;

    public GetUsersListQueryHandler(IVillagesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<UserDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
    {
        var faker = new Faker(request.Country);
        var people = new List<UserDto>();
        var rows = request.RowsNum;
        var errorGenerator = new Error(request.Country, request.Seed, request.NumberErrors);
        for (var s = request.Seed; s < request.Seed + rows; s++)
        {
            faker.Random = new Randomizer(s);
            var fullName = new UserName(request.Country, s).GenerateName();
            var phoneNumber = new PhoneNumber(request.Country, faker).GeneratePhone();
            var address = new UserAddress(request.Country, _dbContext, s).GenerateAddress();
            var errors = errorGenerator.Generate(fullName, address, phoneNumber);
            var user = new UserDto()
            {
                Address = errors[0],
                Name = errors[1],
                PhoneNumber = errors[2]
            };
            people.Add(user);
        }
        return people;
    }
}