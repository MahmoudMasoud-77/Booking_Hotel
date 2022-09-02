namespace Booking_Hotel.DTO
{
    public class ReservationDto
    {
        public ReservationDto()
        {
            ReservationRoomInfo = new List<ReservationRoomDto>();
        }
        public List<ReservationRoomDto> ReservationRoomInfo { get; set; } 
        public string Guest_Id { get; set; }
    }
}
