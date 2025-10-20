using PokeCore.DTO;

public class TreinadorRepository
{
    // Define o nome do "banco de dados" desse repositório
    private const string FilePath = "data/users.json";

    // Construtor: Garante que o diretório "data" exista
    public TreinadorRepository()
    {
        Directory.CreateDirectory("data");
    }

    public List<TreinadorDTO> GetAll()
    {
        // Usa o helper para carregar
        return JsonPersistenceHelper.Load<TreinadorDTO>(FilePath);
    }

    public TreinadorDTO GetById(int id)
    {
        return GetAll().FirstOrDefault(u => u.Id == id);
    }

    public TreinadorDTO GetByUsername(string username)
    {
        return GetAll().FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    public TreinadorDTO FindByLoginIdentifier(string identifier)
    {
        List<TreinadorDTO> users = GetAll();

        return users.FirstOrDefault(u =>
                                    u.Username.Equals(identifier, StringComparison.OrdinalIgnoreCase) ||
                                    u.Email.Equals(identifier, StringComparison.OrdinalIgnoreCase) ||
                                    u.DisplayName.Equals(identifier, StringComparison.OrdinalIgnoreCase)
        );
    }

    public void Add(TreinadorDTO user)
    {
        List<TreinadorDTO > users = GetAll();

        // Lógica simples de auto-incremento do ID
        user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;

        users.Add(user);

        // Usa o helper para salvar
        JsonPersistenceHelper.Save(FilePath, users);
    }

    public void Update(TreinadorDTO user)
    {
        List<TreinadorDTO > users = GetAll();
        TreinadorDTO existingUser = users.FirstOrDefault(u => u.Id == user.Id);

        if (existingUser != null)
        {
            // Atualiza os dados do usuário existente
            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.DisplayName = user.DisplayName;
            existingUser.Password = user.Password;
            existingUser.IsAdmin = user.IsAdmin;

            JsonPersistenceHelper.Save(FilePath, users);
        }
    }

    public void Delete(int id)
    {
        List<TreinadorDTO> todosOsUsuarios = GetAll();

        TreinadorDTO usuarioParaRemover = todosOsUsuarios.FirstOrDefault(u => u.Id == id);

        if (usuarioParaRemover == null)
        {
            throw new Exception("Usuário não encontrado para deleção, tente novamente!");
        }

        todosOsUsuarios.Remove(usuarioParaRemover);
        JsonPersistenceHelper.Save(FilePath, todosOsUsuarios);
    }
}