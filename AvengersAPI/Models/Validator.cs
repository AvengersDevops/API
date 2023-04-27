using Newtonsoft.Json;

namespace AvengersAPI.Models;

public abstract class Validator
{
    
    public static async Task<object> Body<T>(
        Stream requestBody,
        Func<dynamic, T> func) {
        var requestJson = await new StreamReader(requestBody).ReadToEndAsync();
        var body = JsonConvert.DeserializeObject(requestJson);
        
        if (body is null)
            return CustomResponse.Create("error", "Body is null");

        T value = func(body);

        return value;
    }
}