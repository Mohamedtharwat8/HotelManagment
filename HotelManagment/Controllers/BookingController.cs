using HotelManagment.Models;
using HotelManagment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagment.Controllers
{
    public class BookingController : Controller
    {
        private HotelDBEntities objHotelDbEntities;
        public BookingController()
        {
            objHotelDbEntities = new HotelDBEntities();
        }
        public ActionResult Index()
        {
            BookingViewModel objBookingViewModel = new BookingViewModel();
            objBookingViewModel.ListOfRooms = (from objRooms in objHotelDbEntities.Rooms
                                               where objRooms.BookingStatusId == 1
                                               select new SelectListItem()
                                               {
                                                   Text = objRooms.RoomNuber,
                                                   Value = objRooms.RoomId.ToString()
                                               }
                                               ).ToList();
            objBookingViewModel.BookingFrom = DateTime.Now;
            objBookingViewModel.BookingTo = DateTime.Now.AddDays(1);
            return View(objBookingViewModel);
        }
        [HttpPost]
        public ActionResult Index(BookingViewModel objBookingViewModel)
        {
            int numberOfDays = Convert.ToInt32((objBookingViewModel.BookingTo - objBookingViewModel.BookingFrom).TotalDays);
            Room objRooms = objHotelDbEntities.Rooms.Single(model => model.RoomId == objBookingViewModel.AssignRoomId);
            decimal RoomPrice = objRooms.RoomPrice;
            decimal TotalAmount = RoomPrice * numberOfDays;

            RoomBooking roomBookings = new RoomBooking()
            {
                BookingFrom = objBookingViewModel.BookingFrom,
                BookingTo = objBookingViewModel.BookingTo,
                AssignRoomId = objBookingViewModel.AssignRoomId, 
                CustomerName = objBookingViewModel.CustomerName,
                NoOfMembers = objBookingViewModel.NoOfMembers,
                TotalAmount = TotalAmount
            };
            objHotelDbEntities.RoomBookings.Add(roomBookings);
            objHotelDbEntities.SaveChanges();
            objRooms.BookingStatusId = 2;
            objHotelDbEntities.SaveChanges();
            return Json(data: new { message = "Hotel Booking had Successfuly been Created.", success = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public PartialViewResult GetAllBookingHistory()
        {
            List<RoomBookingViewModel> listOfBookingViewModels = new List<RoomBookingViewModel>();
            listOfBookingViewModels = (from objHotelBooking in objHotelDbEntities.RoomBookings
                                       join objRooms in objHotelDbEntities.Rooms on objHotelBooking.AssignRoomId equals objRooms.RoomId
                                       select new RoomBookingViewModel()
                                       {
                                           BookingFrom = objHotelBooking.BookingFrom,
                                           BookingTo = objHotelBooking.BookingTo,
                                           CustomerAddress = objHotelBooking.CutomerAddress,
                                           CustomerName = objHotelBooking.CustomerName,
                                           TotalAmount = objHotelBooking.TotalAmount,
                                           NoOfMembers = objHotelBooking.NoOfMembers,
                                           BookingId = objHotelBooking.BookingId,
                                           RoomNumber = objRooms.RoomNuber,
                                           RoomPrice = objRooms.RoomPrice,}).ToList();
            return PartialView("_BookingHistoryPartial", model: listOfBookingViewModels);


        }
    }


}