using System.Text;
using AvengersAPI.Context;
using Newtonsoft.Json;
using AvengersAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Tests;

public class TaskTests
{
    private readonly MyDbContext _context = new();
    private int _maxId;
    
    [SetUp]
    public void Setup()
    {
        _maxId = _context.Tasks.Max(t => t.Id);
    }
    
    [Test]
    public async Task CreateGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "userId", "1" },
            { "title", "Test" },
            { "description", "Test" },
            { "dueDate", "2021-01-01" },
            { "done", "false" }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        
        var taskController = new TaskController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };
        
        var r = JsonConvert.SerializeObject(await taskController.Create());
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
            { "title", "Test" },
            { "description", "Test" },
            { "dueDate", "2021-01-01" }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        
        var taskController = new TaskController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };
        
        var r = JsonConvert.SerializeObject(await taskController.Create());
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
            { "id", "2" }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        
        var taskController = new TaskController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };
        
        var r = JsonConvert.SerializeObject(await taskController.Read());
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
        
        var taskController = new TaskController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };
        
        var r = JsonConvert.SerializeObject(await taskController.Read());
        var response = JObject.Parse(r);
        
        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == "error", Is.True);
    }

    [Test]
    public async Task ReadAllGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "userId", "1" }
        };
        
        var json = JsonConvert.SerializeObject(body);
var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };

        var taskController = new TaskController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };
        
        var r = JsonConvert.SerializeObject(await taskController.ReadAll());
        var response = JObject.Parse(r);
        
        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == "success", Is.True);
    }

    [Test]
    public async Task ReadAllBad()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "userId", "0" }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };

        var taskController = new TaskController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };

        var r = JsonConvert.SerializeObject(await taskController.ReadAll());
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
            { "id", "2" },
            { "title", "Test" },
            { "description", "Test" },
            { "dueDate", "2021-01-01" },
            { "done", "false" }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        
        var taskController = new TaskController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };
        
        var r = JsonConvert.SerializeObject(await taskController.Update());
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
            { "title", "Test" },
            { "description", "Test" },
            { "dueDate", "2021-01-01" },
            { "done", "false" }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        
        var taskController = new TaskController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };
        
        var r = JsonConvert.SerializeObject(await taskController.Update());
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
        
        var taskController = new TaskController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };
        
        var r = JsonConvert.SerializeObject(await taskController.Delete());
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

        var taskController = new TaskController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };

        var r = JsonConvert.SerializeObject(await taskController.Delete());
        var response = JObject.Parse(r);

        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == "error", Is.True);
    }
}