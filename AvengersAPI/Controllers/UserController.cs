using AvengersAPI.Context;
using AvengersAPI.Entities;
using AvengersAPI.Models;
using AvengersAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AvengersAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly MyDbContext _context = new();

    [HttpPost]
    [Route(nameof(Create))]
    public async Task<CustomResponse> Create()
    {
        CustomResponse? customResponse = null;
        User? user = await Validator.Body<User>(Request.Body, d => UserRequest.Create(d, out customResponse));
        if (customResponse is not null)
            return customResponse;
        
        await _context.Users.AddAsync(user!);
        await _context.SaveChangesAsync();
        
        return new CustomResponse("success", "User created", user);
    }
    
    [HttpPost]
    [Route(nameof(Read))]
    public async Task<CustomResponse> Read()
    {
        CustomResponse? customResponse = null;
        User? user = await Validator.Body<User>(Request.Body, d => UserRequest.Read(d, out customResponse));
        if (customResponse is not null)
            return customResponse;
        
        var u = await _context.Users.FindAsync(user!.Id);
        if (u is null)
            return new CustomResponse("error", $"User with id {user.Id} not found");
        
        return new CustomResponse("success", $"User with id {user.Id} found", u);
    }

    [HttpPost]
    [Route(nameof(Update))]
    public async Task<CustomResponse> Update()
    {
        CustomResponse? customResponse = null;
        User? user = await Validator.Body<User>(Request.Body, d => UserRequest.Update(d, out customResponse));
        if (customResponse is not null)
            return customResponse;
        
        var u = await _context.Users.FindAsync(user!.Id);
        if (u is null)
            return new CustomResponse("error", $"User with id {user.Id} not found");
        
        u.Name = user.Name;
        u.Email = user.Email;
        u.Password = user.Password;
        await _context.SaveChangesAsync();
        
        return new CustomResponse("success", $"User with id {user.Id} updated", u);
    }
    
    [HttpPost]
    [Route(nameof(Delete))]
    public async Task<CustomResponse> Delete()
    {
        CustomResponse? customResponse = null;
        User? user = await Validator.Body<User>(Request.Body, d => UserRequest.Delete(d, out customResponse));
        if (customResponse is not null)
            return customResponse;
        
        var u = await _context.Users.FindAsync(user!.Id);
        if (u is null)
            return new CustomResponse("error", $"User with id {user.Id} not found");
        
        _context.Users.Remove(u);
        await _context.SaveChangesAsync();
        
        return new CustomResponse("success", $"User with id {user.Id} deleted", u);
    }
}
