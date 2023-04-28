using AvengersAPI.Context;
using Microsoft.AspNetCore.Mvc;

namespace AvengersAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController
{
    private readonly MyDbContext _context = new();
    
    [HttpPost]
    public async 
}