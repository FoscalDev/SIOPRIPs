using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SIOP.DumpsAbap.Services;

class Program
{
    public static void Main(string[] args) => Run().GetAwaiter().GetResult();
    public static async Task Run()
    {
        var configuration = new ConfigurationBuilder()
                   .AddJsonFile("configuraciones.json")
                   .Build();

        Serilog.Log.Logger = new LoggerConfiguration()
                   .ReadFrom.Configuration(configuration)
                   .CreateLogger();

        var serviceCollection = new ServiceCollection();
        Configure(serviceCollection);

        var services = serviceCollection.BuildServiceProvider();

        var github = services.GetRequiredService<DumpsService>();

        await github.ObtenerDumps();

        Console.ReadKey();
    }
    public static void Configure(IServiceCollection services)
    {
        var MyConfig = new ConfigurationBuilder().AddJsonFile("configuraciones.json").Build();
        var uri = MyConfig.GetValue<string>("api:url");

        services.AddHttpClient("api", c =>
        {
            c.BaseAddress = new Uri(uri);
            c.Timeout = TimeSpan.FromMinutes(30);

            //c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json"); // GitHub API versioning
            //c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample"); // GitHub requires a user-agent
        })
        .AddTypedClient<DumpsService>();

        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("api"));

    }

}