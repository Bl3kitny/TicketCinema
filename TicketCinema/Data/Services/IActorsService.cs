using System.Collections.Generic;
using System.Threading.Tasks;
using TicketCinema.Data.Base;
using TicketCinema.Models;

namespace TicketCinema.Data.Services
{
    public interface IActorsService : IEntityBaseRepository<Actor>
    {
    }
}
