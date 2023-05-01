using AvengersAPI.Context;
using AvengersAPI.Entities;
using AvengersAPI.Models;
using AvengersAPI.Requests;
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
    [Route(nameof(Create))]
    public async Task<CustomResponse> Create()
    {
        CustomResponse? customResponse = null;
        Task? task = await Validator.Body<Task>(Request.Body, d => TaskRequest.Create(d, out customResponse));
        if (customResponse is not null)
            return customResponse;
        
        await _context.Tasks.AddAsync(task!);
        await _context.SaveChangesAsync();
        
        return new CustomResponse("success", $"Task created and associated with user {task!.UserId}", task);
    }

    [HttpPost]
    [Route(nameof(Read))]
    public async Task<CustomResponse> Read()
    {
        CustomResponse? customResponse = null;
        Task? task = await Validator.Body<Task>(Request.Body, d => TaskRequest.Read(d, out customResponse));
        if (customResponse is not null)
            return customResponse;
        
        var t = await _context.Tasks.FindAsync(task!.Id);
        return t is null ?
            new CustomResponse("error", $"Task with id {task.Id} not found") :
            new CustomResponse("success", $"Task with id {task.Id} found", t);
    }

    [HttpPost]
    [Route(nameof(ReadAll))]
    public async Task<CustomResponse> ReadAll()
    {
        CustomResponse? customResponse = null;
        User? user = await Validator.Body<User>(Request.Body, d => TaskRequest.ReadAll(d, out customResponse));
        if (customResponse is not null)
            return customResponse;
        
        var tasks = await _context.Tasks.Where(t => t.UserId == user!.Id).ToListAsync();

        return tasks.Count == 0 ?
            new CustomResponse("error", $"No tasks found for user {user!.Id}") :
            new CustomResponse("success", $"Tasks found for user {user!.Id}", tasks);
    }

    [HttpPost]
    [Route(nameof(Update))]
    public async Task<CustomResponse> Update()
    {
        CustomResponse? customResponse = null;
        Task? task = await Validator.Body<Task>(Request.Body, d => TaskRequest.Update(d, out customResponse));
        if (customResponse is not null)
            return customResponse;

        var t = await _context.Tasks.FindAsync(task!.Id);
        if (t is null)
            return new CustomResponse("error", $"Task with id {task.Id} not found");
        
        t.Title = task.Title;
        t.Description = task.Description;
        t.DueDate = task.DueDate;
        t.Done = task.Done;
        
        await _context.SaveChangesAsync();
        
        return new CustomResponse("success", $"Task with id {task.Id} updated", t);
    }


    [HttpPost]
    [Route(nameof(Delete))]
    public async Task<CustomResponse> Delete()
    {
        CustomResponse? customResponse = null;
        Task? task = await Validator.Body<Task>(Request.Body, d => TaskRequest.Delete(d, out customResponse));
        if (customResponse is not null)
            return customResponse;
        
        var t = await _context.Tasks.FindAsync(task!.Id);
        if (t is null)
            return new CustomResponse("error", $"Task with id {task.Id} not found");
        
        _context.Tasks.Remove(t);
        await _context.SaveChangesAsync();
        
        return new CustomResponse("success", $"Task with id {task.Id} deleted", t);
    }
}                                   