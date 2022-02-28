using Microsoft.EntityFrameworkCore;

namespace TicketCinema.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {

        }
    }
}
