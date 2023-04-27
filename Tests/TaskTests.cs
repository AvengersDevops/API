﻿using System.Text;
using Newtonsoft.Json;
using AvengersAPI.Controllers;
using AvengersAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Tests;

public class TaskTests
{
    [Test]
    public async Task Create()
    {
        var body = new Dictionary<string, string>
        {
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
}