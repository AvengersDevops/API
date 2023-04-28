using AvengersAPI.Models;
using Task = AvengersAPI.Entities.User;

namespace AvengersAPI.Foo;

public abstract class UserRequest
{
    public static Task? Create(dynamic body, out CustomResponse? customResponse)
    {
        if (body.name is null)
        {
            customResponse = new CustomResponse("error", "Name is null");
            return null;
        }

        if (body.email is null)
        {
            customResponse = new CustomResponse("error", "Email is null");
            return null;
        }

        if (body.password is null)
        {
            customResponse = new CustomResponse("error", "Password is null");
            return null;
        }
        
        customResponse = null;
        return new Task
        {
            Name = body.name,
            Email = body.email,
            Password = body.password
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
        
        return new Task
        {
            Id = int.Parse(body.id.ToString())
        };
    }

    public static Task? Update(dynamic body, out CustomResponse? customResponse)
    {
        if (body.id is null)
        {
            customResponse = new CustomResponse("error", "Id is null");
            return null;
        }

        if (body.name is null)
        {
            customResponse = new CustomResponse("error", "Name is null");
            return null;
        }
        if (body.email is null)
        {
            customResponse = new CustomResponse("error", "Email is null");
            return null;
        }
        if (body.password is null)
        {
            customResponse = new CustomResponse("error", "Password is null");
            return null;
        }

        customResponse = null;
        return new Task
        {
            Id = int.Parse(body.id.ToString()),
            Name = body.name,
            Email = body.email,
            Password = body.password
        };
    }
}