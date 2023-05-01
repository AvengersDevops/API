using AvengersAPI.Entities;
using AvengersAPI.Models;
using AvengersAPI.Utils;

namespace AvengersAPI.Requests;

public abstract class UserRequest
{
    public static User? Create(dynamic body, out CustomResponse? customResponse)
    {
        if (ParameterHandler.IsNull(body.name))
        {
            customResponse = new CustomResponse("error", "Name is null");
            return null;
        }

        if (ParameterHandler.IsNull(body.email))
        {
            customResponse = new CustomResponse("error", "Email is null");
            return null;
        }

        if (ParameterHandler.IsNull(body.password))
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
        if (ParameterHandler.IsNull(body.id))
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
        if (ParameterHandler.IsNull(body.id))
        {
            customResponse = new CustomResponse("error", "Id is null");
            return null;
        }

        if (ParameterHandler.IsNull(body.name))
        {
            customResponse = new CustomResponse("error", "Name is null");
            return null;
        }
        if (ParameterHandler.IsNull(body.email))
        {
            customResponse = new CustomResponse("error", "Email is null");
            return null;
        }
        if (ParameterHandler.IsNull(body.password))
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
        if (ParameterHandler.IsNull(body.id))
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
