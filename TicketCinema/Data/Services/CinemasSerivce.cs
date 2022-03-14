using TicketCinema.Data.Base;
using TicketCinema.Models;

namespace TicketCinema.Data.Services
{
    public class CinemasSerivce : EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasSerivce(AppDbContext context): base(context)
        {

        }
    }
}
