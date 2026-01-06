using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;

namespace AzureFuncApp;

public class HelloFunction
{
    private readonly ILogger<HelloFunction> _logger;

    public HelloFunction(ILogger<HelloFunction> logger)
    {
        _logger = logger;
    }

    [Function("HelloFunction")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        // Получение параметра из query string
        string name = req.Query["name"];

        //// Если не найден в query, пробуем из body
        //if (string.IsNullOrEmpty(name))
        //{
        //    using var reader = new StreamReader(req.Body);
        //    var requestBody = reader.ReadToEnd();
        //    dynamic? data = JsonConvert.DeserializeObject(requestBody);
        //    name = data?.name;
        //}

        // Ответ
        return name != null
            ? (ActionResult)new OkObjectResult($"Hello, {name}!")
            : new BadRequestObjectResult("Please pass a name on the query string or in the request body");

    }
}