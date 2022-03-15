using TicketCinema.Data.Base;
using TicketCinema.Models;

namespace TicketCinema.Data.Services
{
    public interface IMoviesService: IEntityBaseRepository<Movie>
    {
    }
}
