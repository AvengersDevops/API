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
        
        TaskController taskController = new();
        
        await TestHandler.Run(body, Expect.Success, taskController, taskController.Create);
    }
    
    [Test]
    public async Task CreateBad()
    {
        var nullId = new Dictionary<string, dynamic>
        {
            { "title", "Test" },
            { "description", "Test" },
            { "dueDate", "2021-01-01" },
            { "done", "false" }
        };
        var nullTitle = new Dictionary<string, dynamic>
        {
            { "userId", "1" },
            { "description", "Test" },
            { "dueDate", "2021-01-01" },
            { "done", "false" }
        };
        var nullDescription = new Dictionary<string, dynamic>
        {
            { "userId", "1" },
            { "title", "Test" },
            { "dueDate", "2021-01-01" },
            { "done", "false" }
        };
        var nullDueDate = new Dictionary<string, dynamic>
        {
            { "userId", "1" },
            { "title", "Test" },
            { "description", "Test" },
            { "done", "false" }
        };
        var nullDone = new Dictionary<string, dynamic>
        {
            { "userId", "1" },
            { "title", "Test" },
            { "description", "Test" },
            { "dueDate", "2021-01-01" }
        };

        TaskController taskController = new();
        
        await TestHandler.Run(nullId, Expect.Error, taskController, taskController.Create);
        await TestHandler.Run(nullTitle, Expect.Error, taskController, taskController.Create);
        await TestHandler.Run(nullDescription, Expect.Error, taskController, taskController.Create);
        await TestHandler.Run(nullDueDate, Expect.Error, taskController, taskController.Create);
        await TestHandler.Run(nullDone, Expect.Error, taskController, taskController.Create);
    }
    
    [Test]
    public async Task ReadGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", "2" }
        };
        
        TaskController taskController = new();
        
        await TestHandler.Run(body, Expect.Success, taskController, taskController.Read);
    }
    
    [Test]
    public async Task ReadBad()
    {
        var badId = new Dictionary<string, dynamic>
        {
            { "id", "0" }
        };
        var nullId = new Dictionary<string, dynamic>();
        
        TaskController taskController = new();
        
        await TestHandler.Run(badId, Expect.Error, taskController, taskController.Read);
        await TestHandler.Run(nullId, Expect.Error, taskController, taskController.Read);
    }

    [Test]
    public async Task ReadAllGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "userId", "1" }
        };
        
        TaskController taskController = new();
        
        await TestHandler.Run(body, Expect.Success, taskController, taskController.ReadAll);
    }

    [Test]
    public async Task ReadAllBad()
    {
        var badId = new Dictionary<string, dynamic>
        {
            { "userId", "0" }
        };
        var nullId = new Dictionary<string, dynamic>();
        
        TaskController taskController = new();
        
        await TestHandler.Run(badId, Expect.Error, taskController, taskController.ReadAll);
        await TestHandler.Run(nullId, Expect.Error, taskController, taskController.ReadAll);
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
        
        TaskController taskController = new();
        
        await TestHandler.Run(body, Expect.Success, taskController, taskController.Update);
    }

    [Test]
    public async Task UpdateBad()
    {
        var badId = new Dictionary<string, dynamic>
        {
            { "id", "0" },
            { "title", "Test" },
            { "description", "Test" },
            { "dueDate", "2021-01-01" },
            { "done", "false" }
        };
        var nullId = new Dictionary<string, dynamic>
        {
            { "title", "Test" },
            { "description", "Test" },
            { "dueDate", "2021-01-01" },
            { "done", "false" }
        };
        var nullTitle = new Dictionary<string, dynamic>
        {
            { "id", "2" },
            { "description", "Test" },
            { "dueDate", "2021-01-01" },
            { "done", "false" }
        };
        var nullDescription = new Dictionary<string, dynamic>
        {
            { "id", "2" },
            { "title", "Test" },
            { "dueDate", "2021-01-01" },
            { "done", "false" }
        };
        var nullDueDate = new Dictionary<string, dynamic>
        {
            { "id", "2" },
            { "title", "Test" },
            { "description", "Test" },
            { "done", "false" }
        };
        var nullDone = new Dictionary<string, dynamic>
        {
            { "id", "2" },
            { "title", "Test" },
            { "description", "Test" },
            { "dueDate", "2021-01-01" }
        };
        
        TaskController taskController = new();
        
        await TestHandler.Run(badId, Expect.Error, taskController, taskController.Update);
        await TestHandler.Run(nullId, Expect.Error, taskController, taskController.Update);
        await TestHandler.Run(nullTitle, Expect.Error, taskController, taskController.Update);
        await TestHandler.Run(nullDescription, Expect.Error, taskController, taskController.Update);
        await TestHandler.Run(nullDueDate, Expect.Error, taskController, taskController.Update);
        await TestHandler.Run(nullDone, Expect.Error, taskController, taskController.Update);
    }
    
    [Test]
    public async Task DeleteGood()
    {
        var maxId = _context.Tasks.Max(t => t.Id);

        var body = new Dictionary<string, dynamic>
        {
            { "id", maxId.ToString() }
        };
        TaskController taskController = new();
        
        await TestHandler.Run(body, Expect.Success, taskController, taskController.Delete);
    }

    [Test]
    public async Task DeleteBad()
    {
        var badId = new Dictionary<string, dynamic>
        {
            { "id", "0" }
        };
        var nullId = new Dictionary<string, dynamic>();
        
        TaskController taskController = new();
        
        await TestHandler.Run(badId, Expect.Error, taskController, taskController.Delete);
        await TestHandler.Run(nullId, Expect.Error, taskController, taskController.Delete);
    }
}