using System.Threading.Tasks;
using TicketCinema.Data.Base;
using TicketCinema.Models;

namespace TicketCinema.Data.Services
{
    public interface IMoviesService: IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
    }
}
