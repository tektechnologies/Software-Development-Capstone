﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class EventAccessor : IEventAccessor
    {
        /// <summary>
        ///  @Author: Phillip Hansen
        ///  @Created 1/23/2019
        ///  
        /// Class for the stored procedure data for Event Requests
        /// </summary>


        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Constructor for calling non-static methods
        /// </summary>
        public EventAccessor()
        {

        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Updated: 3/1/2019 by Phillip Hansen
        /// Update fields to match new definition in Data Dictionary
        /// 
        /// Updated: 3/29/2019 by Phillip Hansen
        /// Updated fields to match new definitions in Data Dictionary
        /// 
        /// Method for inserting a new event
        /// </summary>
        /// <param name="newEvent"></param> allows a new Event object to be created, called 'newEvent'
        public int insertEvent(Event newEvent)
        {
            int eventID = 0;

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_insert_event", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //Parameters for new Event Request
            cmd.Parameters.AddWithValue("@EventTitle", newEvent.EventTitle);
            cmd.Parameters.AddWithValue("@Price", newEvent.Price);
            cmd.Parameters.AddWithValue("@EmployeeID", newEvent.EmployeeID);
            cmd.Parameters.AddWithValue("@EventTypeID", newEvent.EventTypeID);
            cmd.Parameters.AddWithValue("@Description", newEvent.Description);
            cmd.Parameters.AddWithValue("@EventStartDate", newEvent.EventStartDate);
            cmd.Parameters.AddWithValue("@EventEndDate", newEvent.EventEndDate);
            cmd.Parameters.AddWithValue("@KidsAllowed", newEvent.KidsAllowed);
            cmd.Parameters.AddWithValue("@SeatsRemaining", newEvent.SeatsRemaining);
            cmd.Parameters.AddWithValue("@NumGuests", newEvent.NumGuests);
            cmd.Parameters.AddWithValue("@Location", newEvent.Location);
            cmd.Parameters.AddWithValue("@PublicEvent", newEvent.PublicEvent);
            cmd.Parameters.AddWithValue("@Sponsored", newEvent.Sponsored);
            cmd.Parameters.AddWithValue("@Approved", newEvent.Approved);

            //Add a new parameter without messing with the stored procedure code
            SqlParameter _value = cmd.Parameters.Add("ScopeID", SqlDbType.Int);
            //Direct the parameter to the @return_value
            //In this case, it is the SCOPE_IDENTITY
            _value.Direction = ParameterDirection.ReturnValue;



            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

                //Retrieve the parameter's value
                //Must happen after the query is executed
                eventID = (int)_value.Value;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return eventID;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Updated: 3/1/2019 by Phillip Hansen
        /// Update fields to match new definition in Data Dictionary
        /// 
        /// Updated: 3/29/2019 by Phillip Hansen
        /// Updated fields to match new definitions in Data Dictionary
        /// 
        /// Method for updating an event object from old to new
        /// </summary>
        /// <param name="oldEvent"></param> Needs the old event object
        /// <param name="newEvent"></param> The new event object is the updated version of the old one
        public void updateEvent(Event oldEvent, Event newEvent)
        {

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_update_event", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //Parameters for new Event Request
            cmd.Parameters.AddWithValue("@EventID", oldEvent.EventID);
            cmd.Parameters.AddWithValue("@EventTitle", newEvent.EventTitle);
            cmd.Parameters.AddWithValue("@EmployeeID", newEvent.EmployeeID);
            cmd.Parameters.AddWithValue("@EventTypeID", newEvent.EventTypeID);
            cmd.Parameters.AddWithValue("@Description", newEvent.Description);
            cmd.Parameters.AddWithValue("@EventStartDate", newEvent.EventStartDate);
            cmd.Parameters.AddWithValue("@EventEndDate", newEvent.EventEndDate);
            cmd.Parameters.AddWithValue("@KidsAllowed", newEvent.KidsAllowed);
            cmd.Parameters.AddWithValue("@NumGuests", newEvent.NumGuests);
            cmd.Parameters.AddWithValue("@SeatsRemaining", newEvent.SeatsRemaining);
            cmd.Parameters.AddWithValue("@Location", newEvent.Location);
            cmd.Parameters.AddWithValue("@Sponsored", newEvent.Sponsored);
            cmd.Parameters.AddWithValue("@Approved", newEvent.Approved);
            cmd.Parameters.AddWithValue("@PublicEvent", newEvent.PublicEvent);
            //Parameters for old Event Request
            //The PK ID should not change
            cmd.Parameters.AddWithValue("@OldEventTitle", oldEvent.EventTitle);
            cmd.Parameters.AddWithValue("@OldOfferingID", oldEvent.OfferingID);
            cmd.Parameters.AddWithValue("@OldEmployeeID", oldEvent.EmployeeID);
            cmd.Parameters.AddWithValue("@OldEventTypeID", oldEvent.EventTypeID);
            cmd.Parameters.AddWithValue("@OldDescription", oldEvent.Description);
            cmd.Parameters.AddWithValue("@OldEventStartDate", oldEvent.EventStartDate);
            cmd.Parameters.AddWithValue("@OldEventEndDate", oldEvent.EventEndDate);
            cmd.Parameters.AddWithValue("@OldKidsAllowed", oldEvent.KidsAllowed);
            cmd.Parameters.AddWithValue("@OldNumGuests", oldEvent.NumGuests);
            cmd.Parameters.AddWithValue("@OldSeatsRemaining", oldEvent.SeatsRemaining);
            cmd.Parameters.AddWithValue("@OldLocation", oldEvent.Location);
            cmd.Parameters.AddWithValue("@OldSponsored", oldEvent.Sponsored);
            cmd.Parameters.AddWithValue("@OldApproved", oldEvent.Approved);
            cmd.Parameters.AddWithValue("@OldPublicEvent", oldEvent.PublicEvent);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            
        }

        public void updateEventToCancelled(Event cancelledEvent)
        {
            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_update_event_to_cancelled", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventID", cancelledEvent.EventID);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void updateEventToUncancelled(Event uncancelEvent)
        {
            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_update_event_to_uncancelled", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventID", uncancelEvent.EventID);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Updated: 3/1/2019 by Phillip Hansen
        /// Update fields to match new definition in Data Dictionary
        /// 
        /// Method for retrieving all Events
        /// </summary>
        /// <returns></returns> returns all events
        public List<Event> selectAllEvents()
        {
            List<Event> Events = new List<Event>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_retrieve_all_events_uncancelled";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                
                var r = cmd.ExecuteReader();
                if(r.HasRows)
                {
                    while (r.Read())
                    {
                        Events.Add(new Event()
                        {
                            EventID = r.GetInt32(0),
                            OfferingID = r.GetInt32(1),
                            EventTitle = r.GetString(2),
                            EmployeeID = r.GetInt32(3),
                            EmployeeName = r.GetString(4),
                            EventTypeID = r.GetString(5),
                            Description = r.GetString(6),
                            EventStartDate = r.GetDateTime(7),
                            EventEndDate = r.GetDateTime(8),
                            KidsAllowed = r.GetBoolean(9),
                            NumGuests = r.GetInt32(10),
                            SeatsRemaining = r.GetInt32(11),
                            Location = r.GetString(12),
                            Sponsored = r.GetBoolean(13),
                            Approved = r.GetBoolean(14),
                            Cancelled = r.GetBoolean(15),
                            PublicEvent = r.GetBoolean(16),
                            Price = (decimal)r.GetSqlMoney(17)
                        });
                              
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return Events;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Updated: 3/1/2019 by Phillip Hansen
        /// Update fields to match new definition in Data Dictionary
        /// 
        /// Method for retrieving all Events
        /// </summary>
        /// <returns></returns> returns all events
        public List<Event> selectAllCancelledEvents()
        {
            List<Event> Events = new List<Event>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_retrieve_all_events_cancelled";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        Events.Add(new Event()
                        {
                            EventID = r.GetInt32(0),
                            OfferingID = r.GetInt32(1),
                            EventTitle = r.GetString(2),
                            EmployeeID = r.GetInt32(3),
                            EmployeeName = r.GetString(4),
                            EventTypeID = r.GetString(5),
                            Description = r.GetString(6),
                            EventStartDate = r.GetDateTime(7),
                            EventEndDate = r.GetDateTime(8),
                            KidsAllowed = r.GetBoolean(9),
                            NumGuests = r.GetInt32(10),
                            SeatsRemaining = r.GetInt32(11),
                            Location = r.GetString(12),
                            Sponsored = r.GetBoolean(13),
                            Approved = r.GetBoolean(14),
                            Cancelled = r.GetBoolean(15),
                            PublicEvent = r.GetBoolean(16),
                            Price = (decimal)r.GetSqlMoney(17)
                        });

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return Events;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Updated: 3/1/2019 by Phillip Hansen
        /// Update fields to match new definition in Data Dictionary
        /// 
        /// Method for retrieving a specific Event
        /// </summary>
        /// <remarks>
        /// James Heim
        /// Modified 4/26/2019
        /// Added the CommandType to fix the command from throwing an exception.
        /// </remarks>
        /// <param name="eventReqID"></param> needs the unique ID of the event to be retrieved
        /// <returns></returns>
        public Event selectEventById(int eventReqID)
        {
           Event _event = new Event();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_retrieve_event";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventID", eventReqID);

            try
            {
                conn.Open();
                


                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        _event = new Event()
                        {
                            EventID = r.GetInt32(0),
                            OfferingID = r.GetInt32(1),
                            EventTitle = r.GetString(2),
                            EmployeeID = r.GetInt32(3),
                            EmployeeName = r.GetString(4),
                            EventTypeID = r.GetString(5),
                            Description = r.GetString(6),
                            EventStartDate = r.GetDateTime(7),
                            EventEndDate = r.GetDateTime(8),
                            KidsAllowed = r.GetBoolean(9),
                            NumGuests = r.GetInt32(10),
                            SeatsRemaining = r.GetInt32(11),
                            Location = r.GetString(12),
                            Sponsored = r.GetBoolean(13),
                            Approved = r.GetBoolean(14),
                            PublicEvent = r.GetBoolean(15),
                            Price = (decimal)r.GetSqlMoney(16)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return _event;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Method for purging an event
        /// NOTE: The event must be approved as 'false' or the default value '0' in SQL to be purged
        /// </summary>
        /// <param name="EventID"></param> //needs the unique ID of the event to be deleted
        public void deleteEventByID(int EventID)
        {

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_delete_event", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventID", EventID);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            
        }
    }
}
