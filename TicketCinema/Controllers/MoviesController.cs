using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TicketCinema.Data;
using System.Linq;
using TicketCinema.Data.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketCinema.Models;

namespace TicketCinema.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service) 
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync(n=>n.Cinema);
            return View(allMovies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            return View(movie);
        }

        public async Task<IActionResult> Create()
        {
            var movieDropdowns = await _service.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdowns.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdowns.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdowns.Actors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            var movieDropdowns = await _service.GetNewMovieDropdownsValues();
            if (!ModelState.IsValid)
            {
                ViewBag.Cinemas = new SelectList(movieDropdowns.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdowns.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdowns.Actors, "Id", "FullName");
                return View (movie);
            }

            await _service.AddNewMovieAsync(movie);

            return RedirectToAction("Index");
        }




    }
}
