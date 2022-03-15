using System.Collections.Generic;
using System.Threading.Tasks;
using TicketCinema.Data.Base;
using TicketCinema.Models;

namespace TicketCinema.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        public MoviesService(AppDbContext context):base(context)
        {

        }
    }
}
