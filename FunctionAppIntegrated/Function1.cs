using Azure.Identity;
using Azure.ResourceManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FunctionAppIntegrated
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            ArmClient client = new ArmClient(new VisualStudioCredential());
            var subscriptionResource = await client.GetDefaultSubscriptionAsync();
            var subscription = await subscriptionResource.GetAsync();
            var responseMessage = $"DisplayName: {subscription.Value.Data.DisplayName}";

            return new OkObjectResult(responseMessage);


        }
    }
}
