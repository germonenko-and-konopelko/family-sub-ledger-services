using FastEndpoints;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddFastEndpoints();

var app = builder.Build();

app.UseFastEndpoints(options =>
{
    options.Versioning.Prefix = "v";
});
app.Run();
