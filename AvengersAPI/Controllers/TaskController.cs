using AvengersAPI.Context;
using AvengersAPI.Entities;
using AvengersAPI.Foo;
using AvengersAPI.Models;
using Microsoft.AspNetCore.Mvc;
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
        if (body is not TaskToUserAssociation taskToUserAssociation)
            return body;
        
        await _context.Tasks.AddAsync(taskToUserAssociation.Task);
        await _context.SaveChangesAsync();
       
        TaskToUser taskToUser = new()
        {
            TaskId = taskToUserAssociation.Task.Id,
            UserId = taskToUserAssociation.UserId
        };
        
        await _context.TaskToUsers.AddAsync(taskToUser);
        await _context.SaveChangesAsync();
        
        return CustomResponse.Create("success", $"Task created and associated with user {taskToUserAssociation.UserId}", taskToUserAssociation.Task);
    }
}