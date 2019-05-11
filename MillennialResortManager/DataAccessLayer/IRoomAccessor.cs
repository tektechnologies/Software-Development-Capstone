﻿/// <summary>
/// Wes Richardson
/// Created: 2019/02/12
/// 
/// A Interface for Creating, Reading, Updating, and Deleteing 
/// Data related to Resort Rooms
/// </summary>
/// <remarks>
/// Wes Richardson
/// Updated: 2019/02/20
/// 
/// Added SelectRoomStatusList
/// Update to reflect changes to Room Object and Needed Look up properties
/// </remarks>
using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IRoomAccessor
    {
        int InsertNewRoom(Room room, int employeeID);
        List<string> SelectBuildings();
        Room SelectRoomByID(int roomID);
        List<Room> SelectRoomList();
        List<string> SelectRoomTypes();
        /// <summary>
        /// Danielle Russo
        /// Updated: 2019/04/15
        /// 
        /// Updated to add the newRoom info
        /// </summary>
        int UpdateRoom(Room selectedRoom, Room newRoom);
        int DeleteRoom(Room room);
        int DeleteRoomByID(int roomID);
        List<string> SelectRoomStatusList();

        /// <remarks>
        /// Danielle Russo
        /// Created: 2019/04/04
        /// </remarks>
        List<Room> SelectRoomsByBuildingID(string buildingId);
    }
}