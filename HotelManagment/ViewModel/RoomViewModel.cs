using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace HotelManagment.ViewModel
{

     //<add name = "HotelDBModelEntities" connectionString="data source=DESKTOP-35MLEMB;initial catalog=HotelDB;integrated security=True;multipleactiveresultsets=True;application name=EntityFramework" providerName="System.Data.SqlClient" />
    
    public class RoomViewModel
    {
        public int RoomId { get; set; }
        [Display(Name = "Room No.")]

        public string RoomNumber { get; set; }
        [Display(Name = "Room Image")]

        public string RoomImage { get; set; }

        [Display(Name = "Room Price")]

        public decimal RoomPrice { get; set; }

        [Display(Name = "Booking Status")]

        public int BookingStatusId { get; set; }

        [Display(Name = "Room Type")]

        public int RoomTypeId { get; set; }

        [Display(Name = "Room Capacity")]

        public int RoomCapacity { get; set; }

        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Room Description")]


        public string RoomDescription { get; set; }

        public List<SelectListItem> ListOfBookingStatus { get; set; }

        public List<SelectListItem> ListOfRoomType { get; set; }
    }
}