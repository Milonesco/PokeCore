namespace PokeCore.DTO
{
    public class TreinadorDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FotoPath { get; set; }
    }
}
