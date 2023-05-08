using Microsoft.Extensions.Hosting;

// Work-around for: https://github.com/Azure/azure-functions-dotnet-worker/issues/1318
Environment.SetEnvironmentVariable("DOTNET_STARTUP_HOOKS", null);

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();