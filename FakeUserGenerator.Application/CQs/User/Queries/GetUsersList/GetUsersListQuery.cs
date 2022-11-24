using MediatR;

namespace FakeUserGenerator.Application.CQs.User.Queries.GetUsersList;

public class GetUsersListQuery : IRequest<List<UserDto>>
{
    public string Country { get; set; }
    public int Seed { get; set; }
    public int? RowsNum { get; set; }
    public double NumberErrors { get; set; }
}