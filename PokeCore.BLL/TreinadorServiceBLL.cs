using PokeCore.DTO;
using PokeCore.DAL;
using PokeCore.Utils;
using System.Net.Http.Headers;

namespace PokeCore.BLL
{
    public class TreinadorServiceBLL
    {
        private readonly TreinadorRepository _treinadorRepository;

        public TreinadorServiceBLL(TreinadorRepository treinadorRepository)
        {
            _treinadorRepository = treinadorRepository;
        }

        public TreinadorServiceBLL()
        {
            _treinadorRepository = new TreinadorRepository();
        }

        public TreinadorDTO Login(string username, string password)
        {
            // 1. Busca o usuário 
            TreinadorDTO user = _treinadorRepository.GetByUsername(username);
            if (user == null)
            {
                throw new Exception("Usuário ou senha inválidos.");
            }

            // 2. Verifica a senha (Responsabilidade do Helper)
            bool isPasswordValid = PasswordHelper.VerifyPassword(password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new Exception("Usuário ou senha inválidos.");
            }

            return user;
        }

        public void Register(string username, string password, string email)
        {
            // Validação dos campos vazios

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("O nome do usuário não pode estar vazio.");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("A senha não pode estar vazia.");
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("O e-mail não pode estar vazio");
            }

            // Verifica se o usuário já existe
            var existingUser = _treinadorRepository.GetByUsername(username);
            if (existingUser != null)
            {
                throw new Exception("Nome de usuário já existe. Por favor, escolha outro.");
            }

            // Adiciona o novo usuário
            var novoTreinador = new TreinadorDTO
            {
                Username = username,
                Email = email,
                DisplayName = username, 
                PasswordHash = PasswordHelper.HashPassword(password),
                IsAdmin = false // Novo usuário não é admin por padrão
            };

            _treinadorRepository.Add(novoTreinador); // Manda a DAL adicionar o novo treinador ao JSON
        }
    }
}

