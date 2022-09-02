using Booking_Hotel.Data.Base;
using Booking_Hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking_Hotel.Data.Services
{
    public class BranchService : EntityBaseRepository<Branch>,IBranchService
    {
        private readonly AppDbContext context;

        public BranchService(AppDbContext _context) : base(_context)
        {
            context= _context;
        }

        public async Task<Branch> GetOne(int id)
        {
            Branch branch = await context.Branches.Include(b => b.Rooms).AsSplitQuery().FirstOrDefaultAsync(b => b.Id == id);
            return branch;
        }
        
    }
}
