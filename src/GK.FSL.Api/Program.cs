using FastEndpoints;
using GK.FSL.Api.Services;
using GK.FSL.Api.Services.Contracts;
using GK.FSL.Core;
using Microsoft.EntityFrameworkCore;
using Sqids;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddDbContext<CoreDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Core");
    options.UseNpgsql(connectionString, postgres =>
    {
        postgres.MigrationsAssembly("GK.FSL.Migrations");
        postgres.MigrationsHistoryTable("migrations", "system");
    });
    options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
    options.EnableDetailedErrors(builder.Environment.IsDevelopment());
});

builder.Services.AddSingleton<IIdEncoder, SqidsIdEncoder>();
builder.Services.AddSingleton<SqidsEncoder<long>>(_ =>
{
    var options = new SqidsOptions
    {

    };
    return new SqidsEncoder<long>(options);
});

var app = builder.Build();

app.UseFastEndpoints(options =>
{
    options.Versioning.Prefix = "v";
});
app.Run();
