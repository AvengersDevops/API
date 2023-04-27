using AvengersAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Task = AvengersAPI.Entities.Task;

namespace AvengersAPI.Foo;

public abstract class TaskRequest
{
    public static dynamic Create(dynamic body)
    {
        if (body.title is null)
            return CustomResponse.Create("error", "Title is null");
        
        if (body.description is null)
            return CustomResponse.Create("error", "Description is null");
        
        if (body.dueDate is null)
            return CustomResponse.Create("error", "Due date is null");
        
        if (body.done is null)
            return CustomResponse.Create("error", "Done is null");
        
        return new Task
        {
            Title = body.title,
            Description = body.description,
            DueDate = body.dueDate,
            Done = body.done
        };
    }
}