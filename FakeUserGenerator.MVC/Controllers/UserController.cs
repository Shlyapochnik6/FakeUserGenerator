using AutoMapper;
using FakeUserGenerator.Application.CQs.User.Queries.GetUsersList;
using FakeUserGenerator.Domain;
using FakeUserGenerator.MVC.Models;
using FakeUserGenerator.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FakeUserGenerator.MVC.Controllers;

public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Index([FromBody] UserData userData, int rowsNum, string country)
    {
        var query = new GetUsersListQuery()
        {
            Seed = userData.Seed,
            RowsNum = rowsNum,
            Country = country,
            NumberErrors = userData.NumberErrors
        };
        var users = await _mediator.Send(query);
        return Ok(users);
    } 
}