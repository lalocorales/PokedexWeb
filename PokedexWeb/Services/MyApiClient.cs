using PokedexWeb.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokedexWeb.Services
{
    public class MyApiClient
    {
        private readonly HttpClient _httpClient;

        public MyApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Pokemon>> GetPokemonAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7036/Pokemon");
            response.EnsureSuccessStatusCode();
            var pokemonList = await response.Content.ReadFromJsonAsync<List<Pokemon>>();
            if (pokemonList == null)
            {
                throw new Exception("No data received from API.");
            }
            return pokemonList.OrderBy(pokemon => pokemon.Id).ToList();
        }

        public async Task<Pokemon> GetPokemonById(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7036/Pokemon/{id}");
            response.EnsureSuccessStatusCode();
            var pokemon = await response.Content.ReadFromJsonAsync<Pokemon>();
            if (pokemon == null)
            {
                throw new Exception($"No se encontró ningún Pokémon con el ID {id}.");
            }
            return pokemon;
        }

    }
}