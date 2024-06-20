namespace PokedexWeb.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public List<Type> Types { get; set; } = new List<Type>();
    }

    public class Type
    {
        public string Name { get; set; } = "";
    }
}