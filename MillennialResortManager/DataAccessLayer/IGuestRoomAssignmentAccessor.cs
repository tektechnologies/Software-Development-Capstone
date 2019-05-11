﻿using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
     /// Author: Jared Greenfield
     /// Created On: 2019-04-25
     /// Class for interaction with database for GuestRoomAssignment operations
     /// </summary>
    public interface IGuestRoomAssignmentAccessor
    {
        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Retrieves a list of guest Room Assignment View Models
        /// 
        /// </summary>
        /// <param name="reservationID">ID of Reservation you want Guests of</param>
        /// <returns>List of Room Assignment View Models</returns>
        List<GuestRoomAssignmentVM> SelectGuestRoomAssignmentVMSByReservationID(int reservationID);

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Updates a guest room assignment to checkedout and sets the date
        /// 
        /// </summary>
        /// <param name="roomReservationID">ID of Room Reservation </param>
        /// <param name="guestID">ID of Guest</param>
        /// <returns>0 if failed, greater if successful</returns>
        int UpdateGuestRoomAssignmentToCheckedOut(int guestID, int roomReservationID);
    }
}