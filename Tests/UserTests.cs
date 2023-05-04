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
        
        await TestHandler.Do(body, Expect.Success, userController, userController.Create);
    }
    
    [Test]
    public async Task CreateBad()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "name", "Testname" },
            { "password", "Testpassword" }
        };
        UserController userController = new();
        
        await TestHandler.Do(body, Expect.Error, userController, userController.Create);
    }

    [Test]
    public async Task ReadGood()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", "1" }
        };
        UserController userController = new();
        
        await TestHandler.Do(body, Expect.Success, userController, userController.Read);
    }
    
    [Test]
    public async Task ReadBad()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", "0" }
        };
        UserController userController = new();
        
        await TestHandler.Do(body, Expect.Error, userController, userController.Read);
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
        
        await TestHandler.Do(body, Expect.Success, userController, userController.Update);
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
        UserController userController = new();
        
        await TestHandler.Do(body, Expect.Error, userController, userController.Update);
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
        
        await TestHandler.Do(body, Expect.Success, userController, userController.Delete);
    }

    [Test]
    public async Task DeleteBad()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "id", "0" }
        };
        UserController userController = new();
        
        await TestHandler.Do(body, Expect.Error, userController, userController.Delete);
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
        
        await TestHandler.Do(body, Expect.Success, userController, userController.Login);
    }
    
    [Test]
    public async Task LoginBad()
    {
        var body = new Dictionary<string, dynamic>
        {
            { "email", "Testemail" },
            { "password", "AAAAAAAAA" }
        };
        UserController userController = new();
        
        await TestHandler.Do(body, Expect.Error, userController, userController.Login);
    }
}