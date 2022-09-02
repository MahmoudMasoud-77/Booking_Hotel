using AutoMapper;
using Booking_Hotel.Models;
using BookingHotel.DTO;

namespace BookingHotel.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Branch, BranchDto>();
        }
    }
}
