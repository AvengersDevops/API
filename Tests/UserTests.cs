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

    [Test]
    public async Task CreateGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "name", "Testname" },
            { "email", "Testemail" },
            { "password", "Testpassword" }
        };
        
        UserController userController = new();
        
        await TestHandler.Run(body, Expect.Success, userController, userController.Create);
    }
    
    [Test]
    public async Task CreateBad()
    {
        var nullName = new Dictionary<string, dynamic>
        {
            { "email", "Testemail" },
            { "password", "Testpassword" }
        };
        var nullEmail = new Dictionary<string, dynamic>
        {
            { "name", "Testname" },
            { "password", "Testpassword" }
        };
        var nullPassword = new Dictionary<string, dynamic>
        {
            { "name", "Testname" },
            { "email", "Testemail" }
        };
        
        UserController userController = new();
        
        await TestHandler.Run(nullName, Expect.Error, userController, userController.Create);
        await TestHandler.Run(nullEmail, Expect.Error, userController, userController.Create);
        await TestHandler.Run(nullPassword, Expect.Error, userController, userController.Create);
    }

    [Test]
    public async Task ReadGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", "1" }
        };
        
        UserController userController = new();
        
        await TestHandler.Run(body, Expect.Success, userController, userController.Read);
    }
    
    [Test]
    public async Task ReadBad()
    {
        var nullId = new Dictionary<string, dynamic>();
        var badId = new Dictionary<string, dynamic>
        {
            { "id", "0" }
        };
        
        UserController userController = new();
        
        await TestHandler.Run(nullId, Expect.Error, userController, userController.Read);
        await TestHandler.Run(badId, Expect.Error, userController, userController.Read);
    }
    
    [Test]
    public async Task UpdateGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", "1" },
            { "name", "Testname" },
            { "email", "Testemail" },
            { "password", "Testpassword" }
        };
        
        UserController userController = new();
        
        await TestHandler.Run(body, Expect.Success, userController, userController.Update);
    }
    
    [Test]
    public async Task UpdateBad()
    {
        var badId = new Dictionary<string, dynamic>
        {
            { "id", "0" },
            { "name", "Testname" },
            { "email", "Testemail" },
            { "password", "Testpassword" }
        };
        var nullId = new Dictionary<string, dynamic>
        {
            { "name", "Testname" },
            { "email", "Testemail" },
            { "password", "Testpassword" }
        };
        var nullName = new Dictionary<string, dynamic>
        {
            { "id", "1" },
            { "email", "Testemail" },
            { "password", "Testpassword" }
        };
        var nullEmail = new Dictionary<string, dynamic>
        {
            { "id", "1" },
            { "name", "Testname" },
            { "password", "Testpassword" }
        };
        
        UserController userController = new();
        
        await TestHandler.Run(badId, Expect.Error, userController, userController.Update);
        await TestHandler.Run(nullId, Expect.Error, userController, userController.Update);
        await TestHandler.Run(nullName, Expect.Error, userController, userController.Update);
        await TestHandler.Run(nullEmail, Expect.Error, userController, userController.Update);
    }
    
    [Test]
    public async Task DeleteGood()
    {
        var maxId = _context.Users.Max(t => t.Id);

        var body = new Dictionary<string, dynamic>
        {
            { "id", maxId.ToString() }
        };
        
        UserController userController = new();
        
        await TestHandler.Run(body, Expect.Success, userController, userController.Delete);
    }

    [Test]
    public async Task DeleteBad()
    {
        var badId = new Dictionary<string, dynamic>
        {
            { "id", "0" }
        };
        var nullId = new Dictionary<string, dynamic>();
        
        UserController userController = new();
        
        await TestHandler.Run(badId, Expect.Error, userController, userController.Delete);
        await TestHandler.Run(nullId, Expect.Error, userController, userController.Delete);
    }

    [Test]
    public async Task LoginGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "email", "Testemail" },
            { "password", "Testpassword" }
        };
        
        UserController userController = new();
        
        await TestHandler.Run(body, Expect.Success, userController, userController.Login);
    }
    
    [Test]
    public async Task LoginBad()
    {
        var badEmail = new Dictionary<string, dynamic>
        {
            { "email", "AAAAAAAA" },
            { "password", "Testpassword" }
        };
        var badPassword = new Dictionary<string, dynamic>
        {
            { "email", "Testemail" },
            { "password", "AAAAAAAAA" }
        };
        var nullEmail = new Dictionary<string, dynamic>
        {
            { "password", "AAAAAAAAA" }
        };
        var nullPassword = new Dictionary<string, dynamic>
        {
            { "email", "Testemail" }
        };
        
        UserController userController = new();
        
        await TestHandler.Run(badEmail, Expect.Error, userController, userController.Login);
        await TestHandler.Run(badPassword, Expect.Error, userController, userController.Login);
        await TestHandler.Run(nullEmail, Expect.Error, userController, userController.Login);
        await TestHandler.Run(nullPassword, Expect.Error, userController, userController.Login);
    }
}