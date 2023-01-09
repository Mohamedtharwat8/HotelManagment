using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HotelManagment.ViewModel
{
    public class BookingViewModel
    {
        [Display(Name = "Customer Name")]
        [Required]
        public string CustomerName { get; set; }
        [Display(Name = "Customer Address")]
        [Required]
        public string CustomerAddress { get; set; }
        [Display(Name = "Customer Phone")]
        [Required]
        public string CustomerPhone { get; set; }
        [Display(Name = "Booking From")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingFrom { get; set; }
        [Display(Name = "Booking To")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingTo { get; set; }
        [Display(Name = "Assign Room")]
        [Required]
        public int AssignRoomId { get; set; }
        [Display(Name = "No Of Members")]
        [Required]
        public int NoOfMembers { get; set; }
        public IEnumerable<SelectListItem> ListOfRooms { get; set; }

    }
}