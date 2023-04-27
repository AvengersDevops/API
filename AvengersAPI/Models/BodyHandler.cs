using Newtonsoft.Json;

namespace AvengersAPI.Models;

public abstract class Validator
{
    public static async Task<object> Body(
        Stream requestBody,
        Func<dynamic, dynamic> func) {
        var requestJson = await new StreamReader(requestBody).ReadToEndAsync();
        var body = JsonConvert.DeserializeObject(requestJson);
        
        if (body is null)
            return CustomResponse.Create("error", "Body is null");

        var value = func(body);

        return value;
    }
}