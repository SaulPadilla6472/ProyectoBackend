using ProyectoBackend;//Importa el namespace de la clase Repository
using ProyectoBackend.Models;

var builder = WebApplication.CreateBuilder(args);//Crea la aplicacion
builder.Services.AddDbContext<PubContext>();//Agrega el contexto de la base de datos    
var app = builder.Build();//Crea la aplicacion

app.MapGet("/", () => "Hello World!");
app.MapGet("/brewery", () => new Repository().GetBreweries()); //Llama al metodo GetBreweries de la clase Repository para obtener la lista de cervecerias
app.MapGet("/brewery/{id}", (int id) =>   //Devuelve la cerveceria con el ID que se le pasa por parametro, verifica que el dato no sea nulo
{

    var brewery = new Repository().GetBrewery(id); //obtiene la cerveceria con el ID que se le pasa por parametro
    return brewery is not null ? Results.Ok(brewery) : Results.NotFound();//Si la cerveceria no es nula devuelve la cerveceria, si es nula devuelve un error 404
});

app.MapGet("/beers", (PubContext db) => db.Beers.ToList()); //Devuelve la lista de cervezas

app.MapPost("/beer", (PubContext db, Beer beer) => //Agrega una cerveza a la base de datos
{
    db.Beers.Add(beer); //Agrega la cerveza
    db.SaveChanges();//Guarda los cambios en la bd
    return Results.Created($"/beer/{beer.BeerId}", beer); //Para acceder a la informacion del registro
});

app.MapPut("/beers/{id}", async (int id, PubContext db, Beer beerRequest) =>
{
    var beer = await db.Beers.FindAsync(id); //Obtiene la cerveza de forma asincrona

    if (beer is null) return Results.NotFound(); //Retorna not found si es null

    beer.Name = beerRequest.Name;   //Actualiza los datos
    beer.BrandId = beerRequest.BrandId;

    await db.SaveChangesAsync(); //Guarda los datos

    return Results.NoContent(); //No regresa nada
});

app.MapDelete("/beers/{id}", async (int id, PubContext db) =>
{
    var beer = await db.Beers.FindAsync(id);
    if (beer is null) return Results.NotFound();

    db.Beers.Remove(beer);
    await db.SaveChangesAsync();
    return Results.Ok(beer);
});

app.Run(); //Ejecuta la aplicacion
