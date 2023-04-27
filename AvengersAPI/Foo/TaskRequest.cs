using AvengersAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Task = AvengersAPI.Entities.Task;

namespace AvengersAPI.Foo;

public abstract class TaskRequest
{
    public static Task Create(dynamic body)
    {
        return new Task
        {
            Title = body.title,
            Description = body.description,
            DueDate = body.dueDate,
            Done = body.done
        };
    }
}