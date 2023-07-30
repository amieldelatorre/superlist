using Amazon.Runtime.Internal.Transform;
using Microsoft.AspNetCore.Mvc;
using vlist.Data;
using vlist.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add(new ConsumesAttribute("application/json"));
})
    .AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

IConfiguration config = new ConfigurationBuilder()
    .AddEnvironmentVariables(prefix: InitializationHelper.ENV_PREFIX_FULL)
    .Build();


var DB_HOST = config.GetValue<string?>("DB_HOST", null);
var DB_USERNAME = config.GetValue<string?>("DB_USERNAME", null);
var DB_PASSWORD = config.GetValue<string?>("DB_PASSWORD", null);
var DB_DATABASE_NAME = config.GetValue<string?>("DB_DATABASE_NAME", null);
var DB_COLLECTION_NAME = config.GetValue<string?>("DB_COLLECTION_NAME", null);
var FRONTEND_URL = config.GetValue<string?>("FRONTEND_URL");

var initHelper = new InitializationHelper(DB_HOST, DB_USERNAME, DB_PASSWORD, DB_DATABASE_NAME, DB_COLLECTION_NAME, FRONTEND_URL);

if (!initHelper.IsValidEnvironmentVariables())
{
    foreach (string error in initHelper.Errors)
        Console.WriteLine(error);

    Environment.Exit(1);
}

builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
{
    { "MongoDbSettings:ConnectionString", initHelper.ConnectionStringBuilder() },
    { "MongoDbSettings:DatabaseName", initHelper.DB_DATABASE_NAME },
    { "MongoDbSettings:CollectionName", initHelper.DB_COLLECTION_NAME }
});

builder.Services.Configure<MongoDbDatabaseSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IRepo, MongoDbRepo>();

var AllowSpecificOrigins = "AllowedSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins, builder =>
    {
        builder.WithOrigins(initHelper.FRONTEND_URL)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(AllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("health");
app.UseHttpLogging();

app.Run();
