using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagment.ViewModel
{
    public class RoomBookingViewModel
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime? BookingFrom { get; set; }
        public DateTime? BookingTo { get; set; }
        public decimal RoomPrice { get; set; }
        public int NoOfMembers { get; set; }
        public string RoomNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public int NoOfDays { get; set; }

    }
}