using AvengersAPI.Context;
using AvengersAPI.Entities;
using AvengersAPI.Foo;
using AvengersAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    [HttpGet]
    public async Task<dynamic> Read()
    {
        dynamic body = await Validator.Body(Request.Body, d => TaskRequest.Read(d));
        if (body is not TaskToUserAssociation taskToUserAssociation)
            return body;
        
        var taskToUser = await _context.TaskToUsers.FindAsync(taskToUserAssociation.UserId);
        if (taskToUser is null)
            return CustomResponse.Create("error", $"Task to user association with id {taskToUserAssociation.UserId} not found");
        
        var task = await _context.Tasks.FindAsync(taskToUser.TaskId);
        if (task is null)
            return CustomResponse.Create("error", $"Task with id {taskToUser.TaskId} not found");
        
        return CustomResponse.Create("success", $"Task to user association with id {taskToUserAssociation.UserId} found", task);
    }

    [HttpGet("all")]
    public async Task<dynamic> ReadAll()
    {
        var tasks = await _context.Tasks.ToListAsync();
        return CustomResponse.Create("success", "All tasks", tasks);
    }

    [HttpDelete]
    public async Task<dynamic> Delete()
    {
        dynamic body = await Validator.Body(Request.Body, d => TaskRequest.Delete(d));
        if (body is not TaskToUserAssociation taskToUserAssociation)
            return body;
        
        var taskToUser = await _context.TaskToUsers.FindAsync(taskToUserAssociation.UserId);
        if (taskToUser is null)
            return CustomResponse.Create("error", $"Task to user association with id {taskToUserAssociation.UserId} not found");
        
        var task = await _context.Tasks.FindAsync(taskToUser.TaskId);
        if (task is null)
            return CustomResponse.Create("error", $"Task with id {taskToUser.TaskId} not found");
        
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        
        _context.TaskToUsers.Remove(taskToUser);
        await _context.SaveChangesAsync();
        
        return CustomResponse.Create("success", $"Task to user association with id {taskToUserAssociation.UserId} deleted", task);
    }
}                                   