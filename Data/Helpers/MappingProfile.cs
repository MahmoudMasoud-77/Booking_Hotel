using AutoMapper;
using Booking_Hotel.Models;
using Booking_Hotel.DTO;

namespace Booking_Hotel.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Branch, BranchDto>();
        }
    }
}
