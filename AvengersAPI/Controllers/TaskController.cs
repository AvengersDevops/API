using AvengersAPI.Context;
using AvengersAPI.Foo;
using AvengersAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Task = AvengersAPI.Entities.Task;
using Validator = AvengersAPI.Models.Validator;

namespace AvengersAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly MyDbContext _context = new();

    [HttpPost]
    public async Task<dynamic> Create()
    {
        dynamic body = await Validator.Body(Request.Body, d => TaskRequest.Create(d));
        if (body is not Task task)
            return body;

        await _context.Tasks.AddAsync(task);
        
        await _context.SaveChangesAsync();
        
        return CustomResponse.Create("success", "Task created", task);
    }
}