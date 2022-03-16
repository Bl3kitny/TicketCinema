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
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n=> n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }
            return View("Index",allMovies);
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

        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if (movieDetails == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList()
            };


            var movieDropdowns = await _service.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdowns.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdowns.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdowns.Actors, "Id", "FullName");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewMovieVM movie)
        {

            if (id != movie.Id) return View("NotFound");

            var movieDropdowns = await _service.GetNewMovieDropdownsValues();
            if (!ModelState.IsValid)
            {
                ViewBag.Cinemas = new SelectList(movieDropdowns.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdowns.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdowns.Actors, "Id", "FullName");
                return View(movie);
            }

            await _service.UpdateMovieAsync(movie);

            return RedirectToAction("Index");
        }






    }
}
