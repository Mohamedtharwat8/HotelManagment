using HotelManagement.ViewModel;
using HotelManagment.Models;
using HotelManagment.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HotelManagment.Controllers
{

    public class RoomController : Controller
    {
        private HotelDBEntities objHotelDbEntities;
        public RoomController()
        {
            objHotelDbEntities = new HotelDBEntities();
        }
        public ActionResult Index()
        {
            RoomViewModel objRoomViewModel = new RoomViewModel();
            objRoomViewModel.ListOfBookingStatus = (from obj in objHotelDbEntities.BookingStatus
                                                    select new SelectListItem()
                                                    {
                                                        Text = obj.BookingStatus,
                                                        Value = obj.BookingStatusId.ToString()
                                                    }).ToList();
            objRoomViewModel.ListOfRoomType = (from obj in objHotelDbEntities.RoomTypes
                                               select new SelectListItem()
                                               {
                                                   Text = obj.RoomTypeName,
                                                   Value = obj.RoomTypeId.ToString()
                                               }).ToList();

            return View(objRoomViewModel);
        }
        [HttpPost]
        public ActionResult Index(RoomViewModel objRoomViewModel)
        {
            string message = String.Empty;
            string ImageUniqueName = String.Empty;
            string ActualImageName = String.Empty;
            if (objRoomViewModel.RoomId == 0)
            {
                ImageUniqueName = Guid.NewGuid().ToString();
                ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);
                objRoomViewModel.Image.SaveAs(filename: Server.MapPath("~/RoomImages/" + ActualImageName));
                //objHotelDbEntities
                Room objRooms = new Room()
                {
                    RoomNuber = objRoomViewModel.RoomNumber,
                    RoomDescription = objRoomViewModel.RoomDescription,
                    RoomPrice = objRoomViewModel.RoomPrice,
                    BookingStatusId = objRoomViewModel.BookingStatusId,
                    IsActive = true,
                    RoomImage = ActualImageName,
                    RoomCapacity = objRoomViewModel.RoomCapacity,
                    RoomTypeId = objRoomViewModel.RoomTypeId
                };
                objHotelDbEntities.Rooms.Add(objRooms);
                message = "Added.";
            }
            else
            {
                Room objRooms = objHotelDbEntities.Rooms.Single(model => model.RoomId == objRoomViewModel.RoomId);
                if (objRoomViewModel.Image != null)
                {
                    ImageUniqueName = Guid.NewGuid().ToString();
                    ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);
                    objRoomViewModel.Image.SaveAs(filename: Server.MapPath("~/RoomImages/" + ActualImageName));
                    objRooms.RoomImage = ActualImageName;
                }
                objRooms.RoomNuber = objRoomViewModel.RoomNumber;
                objRooms.RoomDescription = objRoomViewModel.RoomDescription;
                objRooms.RoomPrice = objRoomViewModel.RoomPrice;
                objRooms.BookingStatusId = objRoomViewModel.BookingStatusId;
                objRooms.IsActive = true;
                objRooms.RoomCapacity = objRoomViewModel.RoomCapacity;
                objRooms.RoomTypeId = objRoomViewModel.RoomTypeId;
                message = "Updated.";
            }
            objHotelDbEntities.SaveChanges();
            return Json(data: new { message = "Room Successfully " + message, success = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAllRooms()
        {
            IEnumerable<RoomDetailsViewModel> listOfRoomDetailsViewModels =
                  (from objRooms in objHotelDbEntities.Rooms
                   join objBooking in objHotelDbEntities.BookingStatus on objRooms.BookingStatusId equals objBooking.BookingStatusId
                   join objRoomType in objHotelDbEntities.RoomTypes on objRooms.RoomTypeId equals objRoomType.RoomTypeId
                   where objRooms.IsActive == true
                   select new RoomDetailsViewModel()
                   {
                       RoomNumber = objRooms.RoomNuber,
                       RoomDescription = objRooms.RoomDescription,
                       RoomCapacity = objRooms.RoomCapacity,
                       RoomPrice = objRooms.RoomPrice,
                       BookingStatus = objBooking.BookingStatus,
                       RoomType = objRoomType.RoomTypeName,
                       RoomImage = objRooms.RoomImage,
                       RoomId = objRooms.RoomId
                   }).ToList();
            return PartialView("_RoomDetailsPartial", listOfRoomDetailsViewModels);
        }
        [HttpGet]
        public JsonResult EditRoomDetails(int roomId)
        {
            var result = objHotelDbEntities.Rooms.Single(model => model.RoomId == roomId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteRoomDetails(int roomId)
        {
            Room objRooms = objHotelDbEntities.Rooms.Single(model => model.RoomId == roomId);
            objRooms.IsActive = false;
            objHotelDbEntities.SaveChanges();
            return Json(data: new { message = "Record successfully deleted.", success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}