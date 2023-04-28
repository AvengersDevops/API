using AvengersAPI.Context;
using AvengersAPI.Entities;
using AvengersAPI.Foo;
using AvengersAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AvengersAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly MyDbContext _context = new();

    [HttpPost]
    public async Task<CustomResponse> Create()
    {
        CustomResponse? customResponse = null;
        User? user = await Validator.Body<User>(Request.Body, d => UserRequest.Create(d, out customResponse));
        if (customResponse is not null)
            return customResponse;
        
        await _context.Users.AddAsync(user!);
        await _context.SaveChangesAsync();
        
        return new CustomResponse("success", $"User created", user);
    }
}