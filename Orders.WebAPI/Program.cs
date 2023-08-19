using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using OrdersManager.Infrastructure.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json")); //Sends Response only in 'application/json' format
    options.Filters.Add(new ConsumesAttribute("application/json")); //Consumes request only in 'application/json' format
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

builder.Services.AddApiVersioning(config =>
{
    config.ApiVersionReader = new UrlSegmentApiVersionReader(); //Versioning is included as a part of URL

    config.DefaultApiVersion = new ApiVersion(1, 0); //Default version number
    config.AssumeDefaultVersionWhenUnspecified = true; //When user doesn't define explicitly, the default versioning will be prior
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")); //Getting connection string from appsetting (CS name is "Default")
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); //Enables swagger to read endpoints of the application
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Orders Web API", Version = "1.0" });
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Orders Web API", Version = "2.0" });

}); //Adds Swagger documentation in the current working projects 



builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); //Enables Endpoint for swagger.json 
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");

    }); //Creates swagger UI for testing all Web API endpoints / action methods 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
