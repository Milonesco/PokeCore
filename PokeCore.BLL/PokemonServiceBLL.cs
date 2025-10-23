using PokeCore.DAL;
using PokeCore.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokeCore.BLL
{
    public class PokemonServiceBLL
    {
        private readonly PokemonRepository _pokemonRepository;
        private readonly TreinadorRepository _treinadorRepository;

        public PokemonServiceBLL(PokemonRepository pokemonRepository, TreinadorRepository treinadorRepository)
        {
            _pokemonRepository = pokemonRepository;
            _treinadorRepository = treinadorRepository;
        }

        public PokemonServiceBLL()
        {
            _pokemonRepository = new PokemonRepository();
            _treinadorRepository = new TreinadorRepository();
        }

        public List<PokemonDTO> GetAllPokemon()
        {
            return _pokemonRepository.getAll();
        }

        public PokemonDTO GetPokemonById(int id)
        {
            PokemonDTO pokemon = _pokemonRepository.GetByID(id);
            if (pokemon == null)
            {
                throw new Exception($"Pokémon com ID {id} não encontrado.");
            }
            return pokemon;
        }

        public void AdminAddPokemon(int adminId, PokemonDTO novoPokemon)
        {
            TreinadorDTO admin = _treinadorRepository.GetById(adminId);
            if (admin == null || !admin.IsAdmin)
            {
                throw new Exception("Acesso negado. Apenas administradores podem adicionar Pokémon.");
            }

            if (string.IsNullOrWhiteSpace(novoPokemon.Nome))
                throw new ArgumentException("Nome é obrigatório.");
            if (novoPokemon.Nivel <= 0)
                throw new ArgumentException("Nível inválido.");
            if (string.IsNullOrWhiteSpace(novoPokemon.Tipo))
                throw new ArgumentException("Tipo é obrigatório.");

            if (novoPokemon.CapturedAt == default(DateTime))
                novoPokemon.CapturedAt = DateTime.Now;
            novoPokemon.isInActiveTeam = false;

            _pokemonRepository.Add(novoPokemon);
        }

        public void AdminUpdatePokemon(int adminId, PokemonDTO pokemonAtualizado)
        {
            TreinadorDTO admin = _treinadorRepository.GetById(adminId);
            if (admin == null || !admin.IsAdmin)
            {
                throw new Exception("Acesso negado. Apenas administradores podem editar Pokémon.");
            }

            PokemonDTO original = _pokemonRepository.GetByID(pokemonAtualizado.Id);
            if (original == null)
                throw new Exception("Pokémon não encontrado para atualização.");

            if (string.IsNullOrWhiteSpace(pokemonAtualizado.Nome))
                throw new ArgumentException("Nome é obrigatório.");
            if (pokemonAtualizado.Nivel <= 0)
                throw new ArgumentException("Nível inválido.");
            if (string.IsNullOrWhiteSpace(pokemonAtualizado.Tipo))
                throw new ArgumentException("Tipo é obrigatório.");

            original.Nome = pokemonAtualizado.Nome;
            original.Nivel = pokemonAtualizado.Nivel;
            original.Tipo = pokemonAtualizado.Tipo;
            original.LocalDeCaptura = pokemonAtualizado.LocalDeCaptura;
            original.Nickname = pokemonAtualizado.Nickname;
            original.OwnerId = pokemonAtualizado.OwnerId;

            _pokemonRepository.Update(original);
        }

        public void AdminDeletePokemon(int adminId, int pokemonId)
        {
            TreinadorDTO admin = _treinadorRepository.GetById(adminId);
            if (admin == null || !admin.IsAdmin)
            {
                throw new Exception("Acesso negado. Apenas administradores podem excluir Pokémon.");
            }

            PokemonDTO pokemon = _pokemonRepository.GetByID(pokemonId);
            if (pokemon == null)
            {
                throw new Exception("Pokémon não encontrado para exclusão.");
            }

            _pokemonRepository.Delete(pokemonId);
        }
    }
}
