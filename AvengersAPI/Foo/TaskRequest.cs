using System.Collections;
using AvengersAPI.Entities;
using AvengersAPI.Models;
using Task = AvengersAPI.Entities.Task;

namespace AvengersAPI.Foo;

public abstract class TaskRequest
{
    public static Task? Create(dynamic body, out CustomResponse? customResponse)
    {
        if (body.userId is null)
        {
            customResponse = new CustomResponse("error", "User id is null");
            return null;
        }

        if (body.title is null)
        {
            customResponse = new CustomResponse("error", "Title is null");
            return null;
        }

        if (body.description is null)
        {
            customResponse = new CustomResponse("error", "Description is null");
            return null;
        }

        if (body.dueDate is null)
        {
            customResponse = new CustomResponse("error", "Due date is null");
            return null;
        }

        if (body.done is null)
        {
            customResponse = new CustomResponse("error", "Done is null");
            return null;
        }
        
        bool doneValue = bool.Parse(body.done.ToString());
        var doneBitArray = new BitArray(1); // Specify the desired length of the BitArray
        doneBitArray.Set(0, doneValue);
        
        customResponse = null;
        return new Task
        {
            UserId = int.Parse(body.userId.ToString()),
            Title = body.title,
            Description = body.description,
            DueDate = body.dueDate,
            Done = doneBitArray
        };
    }
    
    public static Task? Read(dynamic body, out CustomResponse? customResponse)
    {
        if (body.id is null)
        {
            customResponse = new CustomResponse("error", "Id is null");
            return null;
        }
        
        customResponse = null;
        return new Task { Id = int.Parse(body.id.ToString()) };
    }
    
    public static User? ReadAll(dynamic body, out CustomResponse? customResponse)
    {
        if (body.userId is null)
        {
            customResponse = new CustomResponse("error", "User id is null");
            return null;
        }

        customResponse = null;
        return new User { Id = int.Parse(body.userId.ToString()) };
    }
    
    public static Task? Update(dynamic body, out CustomResponse? customResponse)
    {
        if (body.id is null)
        {
            customResponse = new CustomResponse("error", "Id is null");
            return null;
        }

        if (body.title is null)
        {
            customResponse = new CustomResponse("error", "Title is null");
            return null;
        }

        if (body.description is null)
        {
            customResponse = new CustomResponse("error", "Description is null");
            return null;
        }

        if (body.dueDate is null)
        {
            customResponse = new CustomResponse("error", "Due date is null");
            return null;
        }

        if (body.done is null)
        {
            customResponse = new CustomResponse("error", "Done is null");
            return null;
        }
        
        bool doneValue = bool.Parse(body.done.ToString());
        var doneBitArray = new BitArray(1); // Specify the desired length of the BitArray
        doneBitArray.Set(0, doneValue);
        
        customResponse = null;
        return new Task
        {
            Id = int.Parse(body.id.ToString()),
            Title = body.title,
            Description = body.description,
            DueDate = body.dueDate,
            Done = doneBitArray
        };
    }

    public static Task? Delete(dynamic body, out CustomResponse? customResponse)
    {
        if (body.id is null)
        {
            customResponse = new CustomResponse("error", "Id is null");
            return null;
        }
        
        customResponse = null;
        return new Task { Id = int.Parse(body.id.ToString()) };
    }
}