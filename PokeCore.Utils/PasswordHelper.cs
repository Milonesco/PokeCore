using BCrypt.Net;
using BCryptLib = BCrypt.Net.BCrypt;

namespace PokeCore.Utils
{
    public class PasswordHelper
    {
        //// Método para criar o hash
        //public static string HashPassword(string password)
        //{
        //    // O "trabalho sujo" do BCrypt fica isolado aqui
        //    return BCryptLib.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
        //}

        //// Método para verificar o hash
        //public static bool VerifyPassword(string password, string passwordHash)
        //{
        //    return BCryptLib.Verify(password, passwordHash);
        //}

        public static void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("A senha não pode ser vazia.");
            }
        }

        public static void ValidatePasswordStrength(string password)
        {
            if (password.Length < 8)
            {
                throw new ArgumentException("A senha deve ter pelo menos 8 caracteres.");
            }
            if (!password.Any(char.IsUpper))
            {
                throw new ArgumentException("A senha deve conter pelo menos uma letra maiúscula.");
            }
            if (!password.Any(char.IsLower))
            {
                throw new ArgumentException("A senha deve conter pelo menos uma letra minúscula.");
            }
            if (!password.Any(char.IsDigit))
            {
                throw new ArgumentException("A senha deve conter pelo menos um número.");
            }
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                throw new ArgumentException("A senha deve conter pelo menos um caractere especial.");
            }
        }
    }
}
