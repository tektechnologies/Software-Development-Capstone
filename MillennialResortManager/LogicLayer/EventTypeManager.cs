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
	/// <summary author="Phillip Hansen" created="2019/01/24">
	/// Manages Event Types through the Logic Layer
	/// </summary>
	public class EventTypeManager : IEventTypeManager
	{
		public List<string> RetrieveEventTypes()
		{
			List<string> eventTypes = null;

			try
			{
				eventTypes = EventTypeAccessor.RetrieveAllEventTypes();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return eventTypes;
		}


		/// <summary author="Craig Barkley" created="2019/01/23">
		/// This class is for the Event Types in the logic layer, to be a connector to the 
		/// the Presentation Layer via the data.
		/// </summary>
		/// <param name="newEventType"></param>
		/// <returns></returns>
		/// 
		private IEventTypeAccessor _eventAccessor;

		public EventTypeManager()
		{
			_eventAccessor = new EventTypeAccessor();
		}
		public EventTypeManager(IEventTypeAccessor eventAccessor)
		{
			_eventAccessor = eventAccessor;
		}

		/// <summary>
		/// Method for creating a new Event Request
		/// </summary>
		/// <param name="EventType newEventType">The Create Event Type is passed the newEventType.</param>
		/// <returns>Results</returns>
		public bool AddEventType(EventType newEventType)
		{
			ValidationExtensionMethods.ValidateID(newEventType.EventTypeID);
			ValidationExtensionMethods.ValidateDescription(newEventType.Description);

			bool result = false;

			try
			{
				result = (1 == _eventAccessor.CreateEventType(newEventType));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary>
		/// Method for retrieveing an Event Type
		/// </summary>
		/// <param name="()">The Event Type is called with the SelectAllEventTypeId().</param>
		/// <returns>eventTypes</returns>
		public List<string> RetrieveAllEventTypes()
		{
			List<string> eventTypes = null;

			try
			{
				eventTypes = _eventAccessor.SelectAllEventTypeID();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return eventTypes;
		}

		/// <summary>
		/// Method for retrieveing an Event Type by status
		/// </summary>
		/// <param name="()">The Event Type is called with the RetrievetAllEventTypes(status).</param>
		/// <returns>eventTypes</returns>
		public List<EventType> RetrieveAllEventTypes(string status)
		{
			List<EventType> eventTypes = null;

			if (status != "")
			{
				try
				{
					eventTypes = _eventAccessor.RetrievetAllEventTypes(status);
				}
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}

			return eventTypes;
		}

		/// <summary>
		/// Method for deleting an Event Type.
		/// </summary>
		/// <param name="(string eventType)">The Delete Event Type is called with the DeleteEventType(eventType).</param>
		/// <returns>results</returns>
		public bool DeleteEventType(string eventType)
		{
			bool result = false;

			try
			{
				result = (1 == _eventAccessor.DeleteEventType(eventType));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}
	}
}