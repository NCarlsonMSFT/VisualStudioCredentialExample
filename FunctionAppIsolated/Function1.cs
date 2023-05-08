using System.Net;
using Azure.Identity;
using Azure.ResourceManager;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionAppIsolated
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("Function1")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            ArmClient client = new ArmClient(new VisualStudioCredential());
            var subscriptionResource = await client.GetDefaultSubscriptionAsync();
            var subscription = await subscriptionResource.GetAsync();

            response.WriteString($"DisplayName: {subscription.Value.Data.DisplayName}");

            return response;
        }
    }
}
