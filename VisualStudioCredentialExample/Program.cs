using Azure.Identity;
using Azure.ResourceManager;

var creds = new DefaultAzureCredential(includeInteractiveCredentials: false);

ArmClient client = new ArmClient(new DefaultAzureCredential());
var subscriptionResource = await client.GetDefaultSubscriptionAsync();
var subscription = await subscriptionResource.GetAsync();
Console.WriteLine($"DisplayName: {subscription.Value.Data.DisplayName}");
