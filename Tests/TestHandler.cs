﻿using System.Text;
using AvengersAPI.Controllers;
using AvengersAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tests;

public enum Expect
{
    Success,
    Error
}

public abstract class TestHandler
{
    public static async Task Run(Dictionary<string, dynamic> body, Expect expect, dynamic controller, Func<Task<CustomResponse>> func)
    {
        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = request
        };
        
        var r = JsonConvert.SerializeObject(await func());
        var response = JObject.Parse(r);
        
        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == (expect == Expect.Success ? "success" : "error"), Is.True);
    }
}