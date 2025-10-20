using PokeCore.DTO;

namespace PokeCore.DAL
{
    public class PokemonRepository
    {
        private const string FilePath = "data/pokemons.json";

        public PokemonRepository()
        {
            Directory.CreateDirectory("data");
        }

        public List<PokemonDTO> getAll()
        {
            return JsonPersistenceHelper.Load<PokemonDTO>(FilePath);
        }

        public PokemonDTO GetByID(int id)
        {
            return getAll().FirstOrDefault(p => p.Id == id);
        }

        public List<PokemonDTO> GetByOwnerId(int ownerId)
        {
            return getAll().Where(p => p.OwnerId == ownerId).ToList();
        }

        public PokemonDTO GetByNome(string Nome)
        {
            return getAll().FirstOrDefault(p => p.Nome.Equals(Nome, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(PokemonDTO pokemon)
        {
            List<PokemonDTO> pokemons = getAll();
            pokemon.Id = pokemons.Any() ? pokemons.Max(p => p.Id) + 1 : 1;
            pokemons.Add(pokemon);
            JsonPersistenceHelper.Save(FilePath, pokemons);
        }

        public void Update(PokemonDTO pokemon)
        {
            List<PokemonDTO> pokemons = getAll();
            PokemonDTO existingPokemon = pokemons.FirstOrDefault(p => p.Id == pokemon.Id);

            if (existingPokemon != null)
            {
                // Atualiza os dados do pokemon existente
                existingPokemon.Nome = pokemon.Nome;
                existingPokemon.Tipo = pokemon.Tipo;
                existingPokemon.Nivel = pokemon.Nivel;
                existingPokemon.OwnerId = pokemon.OwnerId;
                existingPokemon.CapturedAt = pokemon.CapturedAt;
                existingPokemon.LocalDeCaptura = pokemon.LocalDeCaptura;
                existingPokemon.Nickname = pokemon.Nickname;
                existingPokemon.isInActiveTeam = pokemon.isInActiveTeam;

                JsonPersistenceHelper.Save(FilePath, pokemons);
            }
        }

        public void Delete(int id)
        {
            List<PokemonDTO> pokemons = getAll();

            PokemonDTO pokemonParaRemover = pokemons.FirstOrDefault(p => p.Id == id);

            if (pokemonParaRemover == null)
            {
                throw new Exception("Pokémon não encontrado para deleção, tente novamente!");
            }

            pokemons.Remove(pokemonParaRemover);
            JsonPersistenceHelper.Save(FilePath, pokemons);
        }
    }
}
