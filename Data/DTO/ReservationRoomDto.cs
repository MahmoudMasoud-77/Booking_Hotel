﻿using System.ComponentModel.DataAnnotations;

namespace BookingHotel.DTO
{
    public class ReservationRoomDto
    {
        public int RoomId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateIn { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOut { get; set; }
        public int NumberOfDays { get; set; }
    }
}
