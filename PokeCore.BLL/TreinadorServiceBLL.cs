using PokeCore.DAL;
using PokeCore.DTO;
using PokeCore.Utils;

namespace PokeCore.BLL
{
    public class TreinadorServiceBLL
    {
        private readonly TreinadorRepository _treinadorRepository;
        private readonly PokemonRepository _pokemonRepository;

        public TreinadorServiceBLL(TreinadorRepository treinadorRepository)
        {
            _treinadorRepository = treinadorRepository;
        }

        public TreinadorServiceBLL()
        {
            _treinadorRepository = new TreinadorRepository();
            _pokemonRepository = new PokemonRepository();
        }

        // ### Autenticação e Registro de Usuários ###

        public TreinadorDTO Login(string identifier, string password)
        {

            string cleanedIdentifier = identifier.Trim();
            string cleanedPassword = password.Trim();

            var user = _treinadorRepository.FindByLoginIdentifier(identifier);
            if (user == null)
            {
                throw new Exception("Usuário ou senha inválidos.");
            }

            bool isPasswordValid = (user.Password == password);

            if (!isPasswordValid)
            {
                throw new Exception("Usuário ou senha inválidos.");
            }

            return user;
        }

        public void Register(string username, string password, string email, string displayName, string fotoPath = null)
        {

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("O nome do usuário não pode estar vazio.");
            }
            if (string.IsNullOrWhiteSpace(displayName))
            {
                throw new Exception("O nome de exibição não pode estar vazio.");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("A senha não pode estar vazia.");
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("O e-mail não pode estar vazio");
            }

            PasswordHelper.ValidatePasswordStrength(password);

            var existingUser = _treinadorRepository.GetByUsername(username);
            if (existingUser != null)
            {
                throw new Exception("Nome de usuário já existe. Por favor, escolha outro.");
            }


            var novoTreinador = new TreinadorDTO
            {
                Username = username,
                Email = email,
                DisplayName = displayName,
                FotoPath = fotoPath,
                Password = password,
                IsAdmin = false
            };

            _treinadorRepository.Add(novoTreinador);
        }


        public void UpdateTreinador(TreinadorDTO treinadorAtualizado)
        {
            if (string.IsNullOrWhiteSpace(treinadorAtualizado.Username))
            {
                throw new Exception("O nome do usuàrio não pode ficar em branco");
            }
            if (string.IsNullOrWhiteSpace(treinadorAtualizado.Email))
            {
                throw new Exception("O campo de email não pode ficar em branco");
            }

            TreinadorDTO treinadorComMesmoNome = _treinadorRepository.GetByUsername(treinadorAtualizado.Username);

            if (treinadorComMesmoNome != null && treinadorComMesmoNome.Id != treinadorAtualizado.Id)
            {
                throw new Exception("Esse nome de usuàrio que você deseja já está em uso");
            }

            TreinadorDTO treinadorOriginal = _treinadorRepository.GetById(treinadorAtualizado.Id);

            if (treinadorOriginal == null)
            {
                throw new Exception("Não é possivel atualizar, pois o usuário não foi encontrado");
            }


            treinadorOriginal.Username = treinadorAtualizado.Username;
            treinadorOriginal.Email = treinadorAtualizado.Email;
            treinadorOriginal.DisplayName = treinadorAtualizado.DisplayName;

            _treinadorRepository.Update(treinadorOriginal);
        }

        public void Delete(int idParaDeletar)
        {
            List<PokemonDTO> pokemonsDoTreinador = _pokemonRepository.GetByOwnerId(idParaDeletar);

            if (pokemonsDoTreinador.Any())
            {
                throw new Exception("Não é possivél deletar um treinador que possui pokemon");
            }

            var treinador = _treinadorRepository.GetById(idParaDeletar);

            if (treinador == null)
            {
                throw new Exception("Treinador não encontrado");
            }

            if (treinador.IsAdmin)
            {
                throw new Exception("Não é possivel remover um admin");
            }

            _treinadorRepository.Delete(idParaDeletar);
        }

        public void UpdateUserProfile(TreinadorDTO dadosAtualizados)
        {
            if (string.IsNullOrWhiteSpace(dadosAtualizados.Username))
                throw new ArgumentException("Nome de usuário é obrigatório.");
            if (string.IsNullOrWhiteSpace(dadosAtualizados.Email))
                throw new ArgumentException("Email é obrigatório.");
            if (string.IsNullOrWhiteSpace(dadosAtualizados.DisplayName))
                throw new ArgumentException("Nome de exibição é obrigatório.");

            TreinadorDTO checkUsername = _treinadorRepository.FindByLoginIdentifier(dadosAtualizados.Username); 
            if (checkUsername != null && checkUsername.Id != dadosAtualizados.Id)
            {
                throw new Exception("Este nome de usuário já está em uso por outra conta.");
            }

            TreinadorDTO checkEmail = _treinadorRepository.FindByLoginIdentifier(dadosAtualizados.Email); 
            if (checkEmail != null && checkEmail.Id != dadosAtualizados.Id)
            {
                throw new Exception("Este e-mail já está em uso por outra conta.");
            }

            TreinadorDTO treinadorOriginal = _treinadorRepository.GetById(dadosAtualizados.Id);
            if (treinadorOriginal == null)
            {
                throw new Exception("Usuário original não encontrado para atualização.");
            }

            treinadorOriginal.Username = dadosAtualizados.Username;
            treinadorOriginal.DisplayName = dadosAtualizados.DisplayName;
            treinadorOriginal.Email = dadosAtualizados.Email;
            treinadorOriginal.FotoPath = dadosAtualizados.FotoPath;

            _treinadorRepository.Update(treinadorOriginal);
        }

        public void ChangePassword(int treinadorId, string senhaAntiga, string senhaNova)
        {
            TreinadorDTO treinador = _treinadorRepository.GetById(treinadorId);
            if (treinador == null)
            {
                throw new Exception("Treinador não encontrado");
            }

            bool isPasswordValid = (treinador.Password == senhaAntiga);

            if (!isPasswordValid)
            {
                throw new Exception("Senha antiga incorreta");
            }
            if (string.IsNullOrWhiteSpace(senhaNova))
            {
                throw new Exception("A nova senha não pode estar vazia");
            }

            PasswordHelper.ValidatePasswordStrength(senhaNova);

            treinador.Password = senhaNova;
            _treinadorRepository.Update(treinador);
        }

        public void TreinadorReleasePokemon(int treinadorId, int pokemonId)
        {
            PokemonDTO pokemon = _pokemonRepository.GetByID(pokemonId);
            if (pokemon == null)
            {
                throw new Exception("Pokémon não encontrado");
            }
            if (pokemon.OwnerId != treinadorId)
            {
                throw new Exception("Você só pode liberar seus próprios pokémons");
            }

            pokemon.OwnerId = null;
            _pokemonRepository.Update(pokemon);
        }

        // ### Gerenciamento de Admins ###

        public void AdminCreateTreinador(int adminId, string novoUsername, string novaSenha, string novoEmail, bool NovoAdmin)
        {
            TreinadorDTO admin = _treinadorRepository.GetById(adminId);

            if (admin == null || !admin.IsAdmin)
            {
                throw new Exception("Acesso negado. Apenas administradores podem criar novos treinadores");
            }

            if (string.IsNullOrWhiteSpace(novoUsername))
            {
                throw new Exception("O nome do usuário não pode estar vazio.");
            }
            if (string.IsNullOrWhiteSpace(novaSenha))
            {
                throw new Exception("A senha não pode estar vazia.");
            }
            if (string.IsNullOrWhiteSpace(novoEmail))
            {
                throw new Exception("O e-mail não pode estar vazio");
            }

            PasswordHelper.ValidatePasswordStrength(novaSenha);

            var existingUser = _treinadorRepository.GetByUsername(novoUsername);
            if (existingUser != null)
            {
                throw new Exception("Nome de usuário já existe. Por favor, escolha outro.");
            }

            var novoTreinador = new TreinadorDTO
            {
                Username = novoUsername,
                Email = novoEmail,
                DisplayName = novoUsername,
                Password = novaSenha,
                IsAdmin = NovoAdmin,
                CreatedAt = DateTime.Now
            };

            _treinadorRepository.Add(novoTreinador);
        }

        public void AdminDeleteTreinador(int adminId, int treinadorAlvoId)
        {
            TreinadorDTO admin = _treinadorRepository.GetById(adminId);

            if (admin == null || !admin.IsAdmin)
            {
                throw new Exception("Acesso negado. Apenas administradores podem deletar treinadores");
            }

            TreinadorDTO alvo = _treinadorRepository.GetById(treinadorAlvoId);

            if (alvo == null)
            {
                throw new Exception("Treinador alvo não encontrado");
            }
            if (alvo.IsAdmin)
            {
                throw new Exception("Não é possivel deletar um administrador");
            }

            List<PokemonDTO> pokemonsDoTreinador = _pokemonRepository.GetByOwnerId(treinadorAlvoId);

            if (pokemonsDoTreinador.Any())
            {
                throw new Exception("Não é possivél deletar um treinador que possui pokemon");
            }

            _treinadorRepository.Delete(treinadorAlvoId);
        }

        public void AdminUpdateTreinadorProfile(int adminExecutandoId, TreinadorDTO dadosAtualizados)
        {
            TreinadorDTO admin = _treinadorRepository.GetById(adminExecutandoId);
            if (admin == null || !admin.IsAdmin)
            {
                throw new Exception("Acesso negado. Apenas administradores podem atualizar perfis.");
            }

            if (admin.Id == dadosAtualizados.Id && !dadosAtualizados.IsAdmin)
            {
                throw new Exception("Um administrador não pode remover o próprio status de admin através desta tela.");
            }


            if (string.IsNullOrWhiteSpace(dadosAtualizados.Username))
                throw new ArgumentException("Nome de usuário é obrigatório.");
            if (string.IsNullOrWhiteSpace(dadosAtualizados.Email))
                throw new ArgumentException("Email é obrigatório.");
            if (string.IsNullOrWhiteSpace(dadosAtualizados.DisplayName))
                throw new ArgumentException("Nome de exibição é obrigatório.");


            TreinadorDTO checkUsername = _treinadorRepository.GetByUsername(dadosAtualizados.Username);
            if (checkUsername != null && checkUsername.Id != dadosAtualizados.Id)
            {
                throw new Exception("Este nome de usuário já está em uso por outra conta.");
            }

            TreinadorDTO checkEmail = _treinadorRepository.GetByEmail(dadosAtualizados.Email);
            if (checkEmail != null && checkEmail.Id != dadosAtualizados.Id)
            {
                throw new Exception("Este e-mail já está em uso por outra conta.");
            }

            TreinadorDTO treinadorOriginal = _treinadorRepository.GetById(dadosAtualizados.Id);
            if (treinadorOriginal == null)
            {
                throw new Exception("Treinador alvo não encontrado.");
            }

            treinadorOriginal.Username = dadosAtualizados.Username;
            treinadorOriginal.DisplayName = dadosAtualizados.DisplayName;
            treinadorOriginal.Email = dadosAtualizados.Email;
            treinadorOriginal.IsAdmin = dadosAtualizados.IsAdmin;
            treinadorOriginal.FotoPath = dadosAtualizados.FotoPath; 


            _treinadorRepository.Update(treinadorOriginal);
        }

        public void AdminReleasePokemon(int adminId, int pokemonId)
        {
            TreinadorDTO admin = _treinadorRepository.GetById(adminId);
            if (admin == null || !admin.IsAdmin)
            {
                throw new Exception("Acesso negado. Apenas administradores podem liberar pokémons");
            }

            PokemonDTO pokemon = _pokemonRepository.GetByID(pokemonId);
            if (pokemon == null)
            {
                throw new Exception("Pokémon não encontrado");
            }

            pokemon.OwnerId = null;
            _pokemonRepository.Update(pokemon);
        }

        public void PromoverParaAdmin(int adminId, int treinadorAlvoId, bool promover)
        {
            TreinadorDTO admin = _treinadorRepository.GetById(adminId);

            if (admin == null || !admin.IsAdmin)
            {
                throw new Exception("Apenas administradores podem promover ou rebaixar outros treinadores");
            }

            if (adminId == treinadorAlvoId)
            {
                throw new Exception("Administradores não podem alterar seu próprio status de administrador");
            }

            TreinadorDTO alvo = _treinadorRepository.GetById(treinadorAlvoId);

            if (alvo == null)
            {
                throw new Exception("Treinador alvo não encontrado");
            }

            alvo.IsAdmin = promover;
            _treinadorRepository.Update(alvo);
        }

        public int AdminGetTotalTrainerCount(int adminId)
        {
            TreinadorDTO admin = _treinadorRepository.GetById(adminId);
            if (admin == null || !admin.IsAdmin)
            {
                throw new Exception("Acesso negado.");
            }

            return _treinadorRepository.GetAll().Count;
        }

        public int AdminGetTotalPokemonInSystem(int adminId)
        {
            TreinadorDTO admin = _treinadorRepository.GetById(adminId);
            if (admin == null || !admin.IsAdmin)
            {
                throw new Exception("Acesso negado.");
            }

            return _pokemonRepository.getAll().Count;
        }

        public int AdminGetNewTrainersLast24h(int adminId)
        {
            TreinadorDTO admin = _treinadorRepository.GetById(adminId);
            if (admin == null || !admin.IsAdmin)
            {
                throw new Exception("Acesso negado.");
            }

            return _treinadorRepository.GetAll()
                       .Where(t => t.CreatedAt > DateTime.Now.AddDays(-1))
                       .Count();
        }

        public int AdminGetNewPokemonLast24h(int adminId)
        {
            TreinadorDTO admin = _treinadorRepository.GetById(adminId);
            if (admin == null || !admin.IsAdmin)
            {
                throw new Exception("Acesso negado.");
            }

            return _pokemonRepository.getAll()
                       .Where(p => p.CapturedAt > DateTime.Now.AddDays(-1))
                       .Count();
        }

        public List<TreinadorDTO> GetAllTreinadores()
        {
            return _treinadorRepository.GetAll();
        }

        // ### Métodos de Gerenciamento de Treinadores e Pokemon###


        public TreinadorDTO GetTreinadorById(int id)
        {
            TreinadorDTO treinador = _treinadorRepository.GetById(id);

            if (treinador == null)
            {
                throw new Exception($"Treinador com ID {id} não encontrado.");
            }

            return treinador;
        }
        public List<PokemonDTO> GetTreinadorActiveTeam(int treinadorId)
        {
            List<PokemonDTO> todosOsPokemon = _pokemonRepository.GetByOwnerId(treinadorId);

            return todosOsPokemon.Where(p => p.isInActiveTeam).ToList();
        }

        public List<PokemonDTO> GetTreinadorPcBox(int treinadorId)
        {
            List<PokemonDTO> todosOsPokemon = _pokemonRepository.GetByOwnerId(treinadorId);
            return todosOsPokemon.Where(p => !p.isInActiveTeam).ToList();
        }

        public PokemonDTO GetLastCapturedPokemon(int treinadorId)
        {
            return _pokemonRepository.GetByOwnerId(treinadorId)
                                     .OrderByDescending(p => p.CapturedAt)
                                     .FirstOrDefault();
        }

        public int GetTotalCapturedCount(int treinadorId)
        {
            return _pokemonRepository.GetByOwnerId(treinadorId).Count;
        }

        public void MudarApelidoPokemon(int pokemonID, string novoApelido, int treinadorEditando)
        {
            PokemonDTO pokemon = _pokemonRepository.GetByID(pokemonID);

            if (pokemon == null)
            {
                throw new Exception("Pokémon não encontrado");
            }

            if (pokemon.OwnerId != treinadorEditando)
            {
                throw new Exception("Você não pode editar um pokemon que não é seu");
            }

            if (string.IsNullOrWhiteSpace(novoApelido))
            {
                pokemon.Nickname = null;
            }
            else if (novoApelido.Length > 20)
            {
                throw new Exception("O apelido não pode ter mais que 20 caracteres");
            }
            else
            {
                pokemon.Nickname = novoApelido;
            }

            _pokemonRepository.Update(pokemon);
        }

        public void MudarLocalPokemon(int pokemonId, string novoLocal, int treinadorEditando)
        {
            PokemonDTO pokemon = _pokemonRepository.GetByID(pokemonId);

            if (pokemon == null)
            {
                throw new Exception("Pokémon não encontrado");
            }

            if (pokemon.OwnerId != treinadorEditando)
            {
                throw new Exception("Você não pode editar um pokemon que não é seu");
            }

            if (string.IsNullOrWhiteSpace(novoLocal))
            {
                pokemon.LocalDeCaptura = null;
            }
            else if (novoLocal.Length > 50)
            {
                throw new Exception("O local de captura não pode ter mais que 50 caracteres");
            }

            pokemon.LocalDeCaptura = novoLocal;
            _pokemonRepository.Update(pokemon);
        }

        public void MovePokemonToActiveTeam(int treinadorId, int pokemonId)
        {
            PokemonDTO pokemon = _pokemonRepository.GetByID(pokemonId);
            if (pokemon == null)
            {
                throw new Exception("Pokemon não encontrado");
            }
            if (pokemon.OwnerId != treinadorId)
            {
                throw new Exception("Você só pode mover seus próprios pokémons");
            }

            if (pokemon.isInActiveTeam)
            {
                throw new Exception("Esse pokémon já está na equipe ativa");
            }

            List<PokemonDTO> timeAtivo = GetTreinadorActiveTeam(treinadorId);

            if (timeAtivo.Count >= 6)
            {
                throw new Exception("Seu time ativo já possui 6 pokémons.");
            }

            pokemon.isInActiveTeam = true;
            _pokemonRepository.Update(pokemon);
        }

        public void MovePokemonToPcBox(int treinadorId, int pokemonId)
        {
            PokemonDTO pokemon = _pokemonRepository.GetByID(pokemonId);
            if (pokemon == null)
            {
                throw new Exception("Pokemon não encontrado");
            }
            if (pokemon.OwnerId != treinadorId)
            {
                throw new Exception("Você só pode mover seus próprios pokémons");
            }
            if (!pokemon.isInActiveTeam)
            {
                throw new Exception("Esse pokémon já está na caixa de PC");
            }
            pokemon.isInActiveTeam = false;
            _pokemonRepository.Update(pokemon);
        }
    }
}

