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
        
        await TestHandler.Do(body, Expect.Success, taskController, taskController.Create);
    }
    
    [Test]
    public async Task CreateBad()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "title", "Test" },
            { "description", "Test" },
            { "dueDate", "2021-01-01" }
        };
        TaskController taskController = new();
        
        await TestHandler.Do(body, Expect.Error, taskController, taskController.Create);
    }
    
    [Test]
    public async Task ReadGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", "2" }
        };
        TaskController taskController = new();
        
        await TestHandler.Do(body, Expect.Success, taskController, taskController.Read);
    }
    
    [Test]
    public async Task ReadBad()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", "0" }
        };
        TaskController taskController = new();
        
        await TestHandler.Do(body, Expect.Error, taskController, taskController.Read);
    }

    [Test]
    public async Task ReadAllGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "userId", "1" }
        };
        TaskController taskController = new();
        
        await TestHandler.Do(body, Expect.Success, taskController, taskController.ReadAll);
    }

    [Test]
    public async Task ReadAllBad()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "userId", "0" }
        };
        TaskController taskController = new();
        
        await TestHandler.Do(body, Expect.Error, taskController, taskController.ReadAll);
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
        
        await TestHandler.Do(body, Expect.Success, taskController, taskController.Update);
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
        TaskController taskController = new();
        
        await TestHandler.Do(body, Expect.Error, taskController, taskController.Update);
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
        
        await TestHandler.Do(body, Expect.Success, taskController, taskController.Delete);
    }

    [Test]
    public async Task DeleteBad()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", "0" }
        };
        TaskController taskController = new();
        
        await TestHandler.Do(body, Expect.Error, taskController, taskController.Delete);
    }
}