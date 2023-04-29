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
            { "name", "Testname" },
            { "email", "Testemail" },
            { "password", "Testpassword" }
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
        var body = new Dictionary<string, dynamic>
        {
            { "name", "Testname" },
            { "password", "Testpassword" }
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
    public async Task ReadGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", _maxId.ToString() }
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

        var r = JsonConvert.SerializeObject(await userController.Read());
        var response = JObject.Parse(r);

        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == "success", Is.True);
    }
    
    [Test]
    public async Task ReadBad()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", "0" }
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

        var r = JsonConvert.SerializeObject(await userController.Read());
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
            { "id", _maxId.ToString() },
            { "name", "Testname" },
            { "email", "Testemail" },
            { "password", "Testpassword" }
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
    }
    
    [Test]
    public async Task UpdateBad()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", "0" },
            { "name", "Testname" },
            { "email", "Testemail" },
            { "password", "Testpassword" }
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
    }
    
    [Test]
    public async Task DeleteGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", (_maxId-1).ToString() }
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
    }

    [Test]
    public async Task DeleteBad()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", "0" }
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
    }

}