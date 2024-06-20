using Microsoft.AspNetCore.Mvc;
using PokedexWeb.Helpers;
using PokedexWeb.Models;
using PokedexWeb.Services;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokedexWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyApiClient _apiClient;

        public HomeController(ILogger<HomeController> logger, MyApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Index(int? id, int pageIndex = 1, int pageSize = 5)
        {
            try
            {
                if (id.HasValue)
                {
                    var pokemon = await _apiClient.GetPokemonById(id.Value);
                    return View(new PaginatedList<Pokemon>(new List<Pokemon> { pokemon }, 1, 1, 1)); // Mostrar un solo elemento en una sola página para un solo pokemon
                }
                else
                {
                    var pokemonList = await _apiClient.GetPokemonAsync();
                    var paginatedList = PaginatedList<Pokemon>.Create(pokemonList, pageIndex, pageSize);
                    return View(paginatedList);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Se produjo un error al cargar la lista de Pokémon.";
                _logger.LogError(ex, "Error al cargar la lista de Pokémon");
                return View(new List<Pokemon>());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GetPokemon()
        {
            var pokemonList = await _apiClient.GetPokemonAsync();
            if (pokemonList == null)
            {
                return View(new List<Pokemon>());
            }
            return View(pokemonList);
        }
    }
}