namespace ProyectoBackend
{
    public class Repository
    {
        List<Brewery> breweries = new List<Brewery>()
        {
            new Brewery() { Id = 1, Name = "Minerv" },
            new Brewery() { Id = 2, Name = "Beaver" },
            new Brewery() { Id = 3, Name = "Samichlaus" },
        };
        public List<Brewery> GetBreweries() => breweries;
        public Brewery? GetBrewery(int id) => breweries.Find(p=> p.Id == id); //Compara el ID en el metodo con el ID de la lista
     


    }

    public class Brewery
    {
    public int Id { get; set; }
        public string Name { get; set; }
    }
}
