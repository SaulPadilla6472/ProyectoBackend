using ProyectoBackend;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/brewery", () => new Repository().GetBreweries());

app.Run();
