using System.Text;
using AvengersAPI.Context;
using AvengersAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tests;

public class UserTests
{
    private readonly MyDbContext _context = new();
    private int _maxId;

    [SetUp]

    public void Setup()
    {
        _maxId = _context.Users.Max(t => t.Id);
    }

    [Test]
    public async Task CreateGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "name", "Jhon Doe" },
            { "email", "caca@gmail.com" },
            { "password", "123456" }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };

        var userController = new UserController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };

        var r = JsonConvert.SerializeObject(await userController.Create());
        var response = JObject.Parse(r);

        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == "success", Is.True);
    }

    [Test]
    public async Task CreateBad()
    {
        var body = new Dictionary<string, string>
        {
            { "name", "Jhon Doe" },
            { "email", "culo@gmail.com" },
            { "password", "123456" }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };

        var userController = new UserController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };

        var r = JsonConvert.SerializeObject(await userController.Create());
        var response = JObject.Parse(r);

        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == "error", Is.True);
    }
    
    [Test]
    public async Task UpdateGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", _maxId },
            { "name", "Jhon Doe" },
            { "email", "cula@gmail.com" },
            { "password", "123456" }
        };
        
        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        
        var userController = new UserController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };
        
        var r = JsonConvert.SerializeObject(await userController.Update());
        var response = JObject.Parse(r);
        
        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == "success", Is.True);
        
        var user = _context.Users.Find(_maxId);
        
        Assert.That(user, Is.Not.Null);
        Assert.That(user, Is.Not.Empty);
        Assert.That(user.Name == "Jhon Doe", Is.True);
        
        _context.Users.Update(user);
        
        await _context.SaveChangesAsync();
    }
    [Test]
    public async Task UpdateBad()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", _maxId },
            { "name", "Jhon Doe" },
            { "email", "cula@gmail.com" },
            { "password", "123456" }
        };
        
        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        
        var userController = new UserController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };
        
        var r = JsonConvert.SerializeObject(await userController.Update());
        var response = JObject.Parse(r);
        
        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == "error", Is.True);
        
        var user = _context.Users.Find(_maxId);
        
        Assert.That(user, Is.Not.Null);
        Assert.That(user, Is.Not.Empty);
        Assert.That(user.Name == "Jhon Doe", Is.True);
        
        _context.Users.Update(user);
        
        await _context.SaveChangesAsync();
    }
    [Test]
    public async Task DeleteGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", _maxId }
        };
        
        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        
        var userController = new UserController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };
        
        var r = JsonConvert.SerializeObject(await userController.Delete());
        var response = JObject.Parse(r);
        
        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == "success", Is.True);
        
        var user = _context.Users.Find(_maxId);
        
        Assert.That(user, Is.Null);
    }
    
    [Test]
    public async Task DeleteBad()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", _maxId }
        };
        
        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        
        var userController = new UserController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };
        
        var r = JsonConvert.SerializeObject(await userController.Delete());
        var response = JObject.Parse(r);
        
        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == "error", Is.True);
        
        var user = _context.Users.Find(_maxId);
        
        Assert.That(user, Is.Null);
    }
}