using BCrypt.Net;
using BCryptLib = BCrypt.Net.BCrypt;

namespace PokeCore.Utils
{
    public class PasswordHelper
    {
        // Método para criar o hash
        public static string HashPassword(string password)
        {
            // O "trabalho sujo" do BCrypt fica isolado aqui
            return BCryptLib.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }

        // Método para verificar o hash
        public static bool VerifyPassword(string password, string passwordHash)
        {
            return BCryptLib.Verify(password, passwordHash);
        }
    }
}
