using AvengersAPI.Entities;
using AvengersAPI.Models;
using Task = System.Threading.Tasks.Task;

namespace AvengersAPI.Foo;

public abstract class UserRequest
{
    public static User? Create(dynamic body, out CustomResponse? customResponse)
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
        return new User
        {
            Name = body.name,
            Email = body.email,
            Password = body.password
        };
    }

    public static User? Read(dynamic body, out CustomResponse? customResponse)
    {
        if (body.id is null)
        {
            customResponse = new CustomResponse("error", "Id is null");
            return null;
        }

        customResponse = null;
        
        return new User
        {
            Id = int.Parse(body.id.ToString())
        };
    }

    public static User? Update(dynamic body, out CustomResponse? customResponse)
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
        return new User
        {
            Id = int.Parse(body.id.ToString()),
            Name = body.name,
            Email = body.email,
            Password = body.password
        };
    }
    
    public static User? Delete(dynamic body, out CustomResponse? customResponse)
    {
        if (body.id is null)
        {
            customResponse = new CustomResponse("error", "Id is null");
            return null;
        }

        customResponse = null;
        
        return new User
        {
            Id = int.Parse(body.id.ToString())
        };
    }
}
