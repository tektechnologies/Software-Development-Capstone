﻿using DataAccessLayer;
using DataObjects;
using LogicLayer;
using MillennialResortWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillennialResortWebSite.Controllers
{
    public class RoomsController : Controller
    {
        IRoomManager roomManager = new RoomManager();

        IReservationManager reservationManager = new ReservationManagerMSSQL();

        IGuestManager _guestManager = new GuestManager();

        IRoomType _roomType = new RoomTypeManager();

        IEnumerable<String> _types = null;


        // GET: Rooms
        public ActionResult Index(ReservationSearchModel model)
        {
            _types = _roomType.RetrieveAllRoomTypes();
            roomManager = new RoomManager();

            model.Rooms = roomManager.RetrieveRoomList();

            int hour = DateTime.Now.Hour;

            ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";
            ViewBag.Types = _types;
            return View(model);

        }





        [Authorize]
        public ActionResult Create(int id, DateTime start, DateTime end, int numGuest)
        {
            _types = _roomType.RetrieveAllRoomTypes();
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            Room room = null;
            Guest guest = new Guest();
            try
            {
                room = roomManager.RetreieveRoomByID(id);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            try
            {
                string email = User.Identity.Name;
                guest = _guestManager.RetrieveGuestByEmail(email);
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }


            NewReservation res = new NewReservation()
            {
                ArrivalDate = start,
                DepartureDate = end,
                NumberOfGuests = numGuest,
                numberOfPets = 0,
                RoomType = room.RoomType,
                Notes = ""
            };

            ViewBag.Types = _types;
            return View(res);
        }


        // POST: Reservaion/Create
        [HttpPost]
        public ActionResult Create(NewReservation reservation)
        {
            _types = _roomType.RetrieveAllRoomTypes();
            if (ModelState.IsValid)
            {
                try
                {

                    Guest guest = new Guest();
                    string email = User.Identity.Name;
                    guest = _guestManager.RetrieveGuestByEmail(email);
                    Reservation res = new Reservation()
                    {
                        MemberID = guest.MemberID,
                        NumberOfGuests = reservation.NumberOfGuests,
                        ArrivalDate = reservation.ArrivalDate.Value,
                        DepartureDate = reservation.DepartureDate.Value,
                        Notes = reservation.Notes
                    };
                    if (reservationManager.AddReservation(res))
                    {
                        return RedirectToAction("Index", "MyAccount");
                    }
                }
                catch
                {
                    throw;
                }
            }

            ViewBag.Types = _types;
            return View(reservation);
        }

    }
}