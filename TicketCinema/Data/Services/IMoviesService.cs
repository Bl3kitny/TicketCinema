using System.Threading.Tasks;
using TicketCinema.Data.Base;
using TicketCinema.Data.ViewModels;
using TicketCinema.Models;

namespace TicketCinema.Data.Services
{
    public interface IMoviesService: IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);
    }
}
