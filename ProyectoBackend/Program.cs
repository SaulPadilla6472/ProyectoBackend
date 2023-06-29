using ProyectoBackend;//Importa el namespace de la clase Repository
using ProyectoBackend.Models;

var builder = WebApplication.CreateBuilder(args);//Crea la aplicacion
var app = builder.Build();//Crea la aplicacion

app.MapGet("/", () => "Hello World!");
app.MapGet("/brewery", () => new Repository().GetBreweries()); //Llama al metodo GetBreweries de la clase Repository para obtener la lista de cervecerias
app.MapGet("/brewery/{id}", (int id) =>   //Devuelve la cerveceria con el ID que se le pasa por parametro, verifica que el dato no sea nulo
{

    var brewery = new Repository().GetBrewery(id); //obtiene la cerveceria con el ID que se le pasa por parametro
    return brewery is not null ? Results.Ok(brewery) : Results.NotFound();//Si la cerveceria no es nula devuelve la cerveceria, si es nula devuelve un error 404
});

app.MapGet("/beers", () =>
{
    List<Beer> beers = null; //Crea una lista de cervezas

    using (var db = new PubContext()) //Crea un contexto de la base de datos
    {
        beers = db.Beers.ToList(); //Obtiene la lista de cervezas de la base de datos
    }

    return beers; //Devuelve la lista de cervezas
});

app.Run(); //Ejecuta la aplicacion
