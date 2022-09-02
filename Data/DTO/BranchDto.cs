using Booking_Hotel.Models;
using System.ComponentModel.DataAnnotations;

namespace BookingHotel.DTO
{
    public class BranchDto
    {
        public int Id { get; set; }
        [Display(Name = "Branch Name")]
        [MaxLength(70)]
        public string Name { get; set; }
        [Display(Name = "Branch Location")]
        [MaxLength(70)]
        public string Location { get; set; }
        [Display(Name = "Branch City")]
        [MaxLength(70)]
        public string City { get; set; }

        //Relation

        public virtual ICollection<Room>? Rooms { get; set; }
    }
}
