using Azure.Identity;
using Azure.ResourceManager;

ArmClient client = new ArmClient(new VisualStudioCredential());
var subscriptionResource = await client.GetDefaultSubscriptionAsync();
var subscription = await subscriptionResource.GetAsync();
Console.WriteLine($"DisplayName: {subscription.Value.Data.DisplayName}");
