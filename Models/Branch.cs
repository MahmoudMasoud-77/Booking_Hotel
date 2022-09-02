using Booking_Hotel.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace Booking_Hotel.Models
{
    public class Branch : IEntityBase
    {
        public int Id { get; set; }
        [Display(Name ="Branch Name")]
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
