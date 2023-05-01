using System.Collections;
using AvengersAPI.Entities;
using AvengersAPI.Models;
using AvengersAPI.Utils;
using Task = AvengersAPI.Entities.Task;

namespace AvengersAPI.Requests;

public abstract class TaskRequest
{
    public static Task? Create(dynamic body, out CustomResponse? customResponse)
    {
        if (ParameterHandler.IsNull(body.userId))
        {
            customResponse = new CustomResponse("error", "User id is null");
            return null;
        }

        if (ParameterHandler.IsNull(body.title))
        {
            customResponse = new CustomResponse("error", "Title is null");
            return null;
        }

        if (ParameterHandler.IsNull(body.description))
        {
            customResponse = new CustomResponse("error", "Description is null");
            return null;
        }

        if (ParameterHandler.IsNull(body.dueDate))
        {
            customResponse = new CustomResponse("error", "Due date is null");
            return null;
        }

        if (ParameterHandler.IsNull(body.done))
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
        if (ParameterHandler.IsNull(body.id))
        {
            customResponse = new CustomResponse("error", "Id is null");
            return null;
        }
        
        customResponse = null;
        return new Task { Id = int.Parse(body.id.ToString()) };
    }
    
    public static User? ReadAll(dynamic body, out CustomResponse? customResponse)
    {
        if (ParameterHandler.IsNull(body.userId)) 
        {
            customResponse = new CustomResponse("error", "User id is null");
            return null;
        }

        customResponse = null;
        return new User { Id = int.Parse(body.userId.ToString()) };
    }
    
    public static Task? Update(dynamic body, out CustomResponse? customResponse)
    {
        if (ParameterHandler.IsNull(body.id)) 
        {
            customResponse = new CustomResponse("error", "Id is null");
            return null;
        }

        if (ParameterHandler.IsNull(body.title)) 
        {
            customResponse = new CustomResponse("error", "Title is null");
            return null;
        }

        if (ParameterHandler.IsNull(body.description))
        {
            customResponse = new CustomResponse("error", "Description is null");
            return null;
        }

        if (ParameterHandler.IsNull(body.dueDate)) 
        {
            customResponse = new CustomResponse("error", "Due date is null");
            return null;
        }

        if (ParameterHandler.IsNull(body.done))
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
        if (ParameterHandler.IsNull(body.id))
        {
            customResponse = new CustomResponse("error", "Id is null");
            return null;
        }
        
        customResponse = null;
        return new Task { Id = int.Parse(body.id.ToString()) };
    }
}