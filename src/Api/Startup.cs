using System.Text.Json.Serialization;
using Application;
using Infrastructure;

namespace Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocument(settings =>
        {
            settings.PostProcess = document =>
            {
                document.Info.Version = "v1";
                document.Info.Title = "GeoDistanceCalculator API";
                document.Info.Description = "GeoDistanceCalculator API";
            };
        });
        services.AddCors();
        services.AddInfrastructure();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            
            app.UseCors(builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            );
        }

        app.UseHttpsRedirection();
        app.UseApplication();

        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
        });
    }
}