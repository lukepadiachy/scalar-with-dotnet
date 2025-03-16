using SampleApiScalar.Endpoints;
using SampleApiScalar.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddDependencies();

var app = builder.Build();

app.UseOpenApi();

app.UseHttpsRedirection();

app.AddRootEndpoints();
app.AddRapEndpoints();

app.Run();