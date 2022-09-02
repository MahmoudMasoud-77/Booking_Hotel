using Booking_Hotel.Data.Base;
using Booking_Hotel.Models;

namespace Booking_Hotel.Data.Services
{
    public class BranchService : EntityBaseRepository<Branch>,IBranchService
    {
        public BranchService(AppDbContext _context) : base(_context)
        {
        }
    }
}
