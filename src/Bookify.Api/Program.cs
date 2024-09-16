using Bookify.Api.Extensions;
using Bookify.Application;
using Bookify.Infrastructure;
using Serilog;



var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configurations) =>
{
    configurations.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();

    // app.SeedData();

}

await app.SeedAdmin();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseCustomExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
