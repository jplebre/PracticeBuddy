using MySqlConnector;
using PracticeBuddy.API.Infrastructure.Extensions;
using PracticeBuddy.API.Infrastructure.Options;
using PracticeBuddy.Core.DataAccess;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

// Must be registered before auth/cors/etc
builder.Services.RegisterHealthChecks();

builder.Services
    .AddOptions()
    .Configure<PracticeBuddyApiOptions>(o => builder.Configuration.GetSection("PracticeBuddyApi").Bind(o))
    .AddScoped<IDapperDb, DapperDb>()
    .AddMySqlDataSource(builder.Configuration.GetSection("PracticeBuddyApi:MySql").Get<MySqlOptions>()!.ConnectionString);

builder.Services.AddControllers();
builder.Services.ConfigureCache();
builder.Services.AddApiExplorerDocumentation();

var app = builder.Build();

app.UseOutputCache();

if (app.Environment.IsDevelopment())
{
    // While we don't have auth, use OpenApi only in dev
    // Then, use this: app.MapOpenApi().RequireAuthorization("ApiTesterPolicy").CacheOutput();;
    app.MapOpenApi().CacheOutput(); ;
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHealthChecks();
app.MapControllers();

app.Run();

public partial class Program { }
