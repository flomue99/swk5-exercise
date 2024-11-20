using OrderManagement.Logic;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration, builder.Environment);

var app = builder.Build();

ConfigureMiddleware(app as IApplicationBuilder, builder.Environment);
ConfigureEndpoints(app as IEndpointRouteBuilder);

app.Run();
//dotnet watch  run --launch-profile https --project .\OrderManagement.Api\

void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
{
    //returns 406 if media type is net accepted
    services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            })
            .AddXmlDataContractSerializerFormatters();
    //env can be used like this
    //env.IsDevelopment()
    services.AddScoped<IOrderManagementLogic, OrderManagementLogic>();
}

void ConfigureMiddleware(IApplicationBuilder app, IHostEnvironment env)
{
    app.UseHttpsRedirection();
    app.UseAuthorization();
}

void ConfigureEndpoints(IEndpointRouteBuilder app)
{
    app.MapControllers();
}