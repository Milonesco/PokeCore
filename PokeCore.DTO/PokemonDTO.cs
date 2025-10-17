namespace PokeCore.DTO
{
    public class PokemonDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public int Nivel { get; set; }
        public int OwnerId { get; set; }
        public DateTime CapturedAt { get; set; }
        public string Nickname { get; set; }
    }
}
