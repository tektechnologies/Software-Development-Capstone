﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Phillip Hansen" created="2019/01/23">
	/// This class is for the Event objects in the logic layer, to be a building block between
	/// the Presentation Layer and the Data Access layer
	/// </summary>
	public class EventManager : IEventManager
	{
		private IEventAccessor _eventAccessor;
		public int _createdEventID = 0;

		/// <summary author="Phillip Hansen" created="2019/01/23">
		/// Constructor for calling non-static methods
		/// </summary>
		public EventManager()
		{
			_eventAccessor = new EventAccessor();
		}

		public EventManager(MockEventAccessor _mockEventAccessor)
		{
			_eventAccessor = _mockEventAccessor;
		}

		/// <summary author="Phillip Hansen" created="2019/01/23">
		/// Method for creating an event calling to the accessor for events
		/// </summary>
		/// <param name="newEvent"></param> creates a new Event object called newEvent
		public int CreateEvent(Event newEvent)
		{
			try
			{
				if (!IsValid(newEvent))
				{
					throw new ArgumentException("Input for the new event was invalid!");
				}
				_createdEventID = _eventAccessor.insertEvent(newEvent);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return _createdEventID;
		}

		/// <summary author="Phillip Hansen" created="2019/01/23">
		/// Method for retrieving all events as a list
		/// </summary>
		/// <returns></returns>
		public List<Event> RetrieveAllEvents()
		{
			List<Event> events;

			try
			{
				events = _eventAccessor.selectAllEvents();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return events;
		}

		/// <summary author="Phillip Hansen" created="2019/01/23">
		/// Method for retrieving all events as a list
		/// </summary>
		/// <returns></returns>
		public List<Event> RetrieveAllCancelledEvents()
		{
			List<Event> events;

			try
			{
				events = _eventAccessor.selectAllCancelledEvents();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return events;
		}

		public Event RetrieveEventByID(int eventId)
		{
			Event _event;

			try
			{
				_event = _eventAccessor.selectEventById(eventId);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return _event;
		}

		/// <summary author="Phillip Hansen" created="2019/01/23">
		/// Updates the event
		/// </summary>
		/// <param name="oldEvent"></param> the old event
		/// <param name="newEvent"></param> the new event after updating
		public void UpdateEvent(Event oldEvent, Event newEvent)
		{
			try
			{
				if (!IsValid(newEvent))
				{
					throw new ArgumentException("Input for the new event was invalid!");
				}
				_eventAccessor.updateEvent(oldEvent, newEvent);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		public void UpdatEventToUncancel(Event uncancelEvent)
		{
			try
			{
				_eventAccessor.updateEventToUncancelled(uncancelEvent);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Phillip Hansen" created="2019/04/03">
		/// Method for cancelling a chosen event
		/// </summary>
		/// <param name="selectedEvent"></param> the specific event passed through
		public void UpdateEventToCancel(Event cancelEvent)
		{
			try
			{
				_eventAccessor.updateEventToCancelled(cancelEvent);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Phillip Hansen" created="2019/01/23">
		/// Deletes the event by taking the object as a whole, and passes only the ID
		/// </summary>
		/// <param name="purgeEvent"></param> the event to be purged
		public void DeleteEvent(Event purgeEvent)
		{

			try
			{
				_eventAccessor.deleteEventByID(purgeEvent.EventID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		public bool IsValid(Event _event)
		{
			if (ValidateStrings(_event) && ValidateDates(_event))
			{
				return true;
			}

			return false;
		}

		public bool ValidateStrings(Event _event)
		{
			if (_event.EventTitle == null || _event.EventTitle == "")
			{
				return false;
			}
			else if (_event.EventTitle.Length > 50)
			{
				return false;
			}
			else if (_event.EventTypeID == null || _event.EventTypeID == "")
			{
				return false;
			}
			else if (_event.EventTypeID.Length > 15)
			{
				return false;
			}
			else if (_event.Description.Length > 1000)
			{
				return false;
			}
			else if (_event.Location == null || _event.Location == "")
			{
				return false;
			}
			else if (_event.Location.Length > 50)
			{
				return false;
			}
			else if (_event.SeatsRemaining > _event.NumGuests)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool ValidateDates(Event _event)
		{
			if (_event.EventStartDate.Date == null || _event.EventStartDate.Date <= DateTime.Now)
			{
				return false;
			}
			else if (_event.EventEndDate.Date == null || _event.EventEndDate.Date < _event.EventStartDate.Date)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}