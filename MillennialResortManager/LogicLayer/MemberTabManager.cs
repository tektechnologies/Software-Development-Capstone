﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	public class MemberTabManager : IMemberTabManager
	{

		private IMemberTabAccessor _memberTabAccessor;

		/// <summary author="James Heim" created="2019/04/18">
		/// The default constructor. Generates the default MemberTabAccessor (MSSQL.)
		/// </summary>
		public MemberTabManager()
		{
			_memberTabAccessor = new MemberTabAccessor();
		}

		/// <summary author="James Heim" created="2019/04/18">
		/// The constructor that takes an object inheriting from IMemberTabAccessor.
		/// Allows the user to specify an accessor other than MSSQL or a Mock Accessor.
		/// </summary>
		/// <param name="accessor">The accessor.</param>
		public MemberTabManager(IMemberTabAccessor accessor)
		{
			_memberTabAccessor = accessor;
		}

		/// <summary author="James Heim" created="2019/04/18">
		/// Create a new MemberTab for the specified Member.
		/// </summary>
		/// <remarks>
		/// Modified by James Heim
		/// Modified 2019-05-01
		/// Stored Procedure does not return rowcount, and database is locked down,
		/// so set result to true if it ran without exception. 
		/// (Procedure throws error if there exists an already active member tab.)
		/// </remarks>
		/// <param name="memberID"></param>
		/// <returns>Whether the tab was created.</returns>
		public bool CreateMemberTab(int memberID)
		{
			bool result = false;

			try
			{
				_memberTabAccessor.InsertMemberTab(memberID);
				result = true;
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="James Heim" created="2019/04/18">
		/// Retrieve the only active MemberTab by the specified Member's ID.
		/// </summary>
		/// <param name="memberID"></param>
		/// <returns>The only active MemberTab</returns>
		public MemberTab RetrieveActiveMemberTabByMemberID(int memberID)
		{
			MemberTab memberTab = null;

			try
			{
				memberTab = _memberTabAccessor.SelectActiveMemberTabByMemberID(memberID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return memberTab;
		}

		/// <summary author="James Heim" created="2019/04/18">
		/// Retrieve the MemberTab that matches the specified MemberTabID.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>The MemberTab.</returns>
		public MemberTab RetrieveMemberTabByID(int id)
		{
			MemberTab memberTab = null;

			try
			{
				memberTab = _memberTabAccessor.SelectMemberTabByID(id);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return memberTab;
		}

		/// <summary author="James Heim" created="2019/04/18">
		/// Set the MemberTab to inactive.
		/// Should not work if any guests are still checked in.
		/// </summary>
		/// <param name="memberTabID"></param>
		/// <returns>If the deactivation was successful.</returns>
		public bool DeactivateMemberTab(int memberTabID)
		{
			bool result = false;

			try
			{
				result = (1 == _memberTabAccessor.DeactivateMemberTab(memberTabID));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="James Heim" created="2019/04/25">
		/// Reactivate the specified member tab.
		/// </summary>
		/// <param name="memberTabID"></param>
		/// <returns>If the reactivation was successful.</returns>
		public bool ReactivateMemberTab(int memberTabID)
		{
			bool result = false;

			try
			{
				result = (1 == _memberTabAccessor.ReactivateMemberTab(memberTabID));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="James Heim" created="2019/04/25">
		/// Create a new line on the member's tab and return the ID of that line.
		/// Throws an exception if the ID is not updated by the database.
		/// </summary>
		/// <param name="memberTabLine"></param>
		/// <returns>The ID of the newly created MemberTab.</returns>
		public int CreateMemberTabLine(MemberTabLine memberTabLine)
		{
			int memberTabLineID = -1;

			try
			{
				memberTabLineID = _memberTabAccessor.InsertMemberTabLine(memberTabLine);

				if (memberTabLineID == -1)
				{
					throw new ApplicationException("TabLine ID was not set by the database.");
				}
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return memberTabLineID;
		}

		/// <summary author="James Heim" created="2019/04/25">
		/// Retrieve all lines for the specified Tab.
		/// </summary>
		/// <param name="memberTabID"></param>
		/// <returns>A list of the tab's tablines.</returns>
		public IEnumerable<MemberTabLine> RetrieveMemberTabLinesByMemberTabID(int memberTabID)
		{
			List<MemberTabLine> tabLines = null;

			try
			{
				tabLines = _memberTabAccessor.SelectMemberTabLinesByMemberTabID(memberTabID).ToList();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return tabLines;
		}

		/// <summary author="James Heim" created="2019/04/25">
		/// Retrieve the line on a tab by its unique ID.
		/// </summary>
		/// <param name="memberTabLineID"></param>
		/// <returns>The TabLine specific to the suppied ID.</returns>
		public MemberTabLine RetrieveMemberTabLineByID(int memberTabLineID)
		{
			MemberTabLine tabLine = null;

			try
			{
				tabLine = _memberTabAccessor.SelectMemberTabLineByID(memberTabLineID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return tabLine;
		}

		/// <summary author="James Heim" created="2019/04/25">
		/// Delete the specified tabline.
		/// </summary>
		/// <param name="memberTabLineID"></param>
		/// <returns>Whether the Delete was successful.</returns>
		public bool DeleteMemberTabLine(int memberTabLineID)
		{
			bool result = false;

			try
			{
				result = (1 == _memberTabAccessor.DeleteMemberTabLine(memberTabLineID));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="James Heim" created="2019/04/25">
		/// Retrieve all member tabs.
		/// </summary>
		/// <returns>A list of all member tabs.</returns>
		public IEnumerable<MemberTab> RetrieveAllMemberTabs()
		{
			List<MemberTab> allTabs = null;

			try
			{
				allTabs = _memberTabAccessor.SelectMemberTabs().ToList();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return allTabs;
		}

		/// <summary author="James Heim" created="2019/04/25">
		/// Delete the specified Member Tab.
		/// </summary>
		/// <param name="memberTabID"></param>
		/// <returns>Whether the delete was successful.</returns>
		public bool DeleteMemberTab(int memberTabID)
		{
			bool result = false;

			try
			{
				result = (1 == _memberTabAccessor.DeleteMemberTab(memberTabID));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}

		/// <summary author="James Heim" created="2019/04/26">
		/// Retrieve all tabs a Member has ever had.
		/// </summary>
		/// <param name="memberID"></param>
		/// <returns></returns>
		public IEnumerable<MemberTab> RetrieveMemberTabsByMemberID(int memberID)
		{
			List<MemberTab> memberTabs = new List<MemberTab>();

			try
			{
				memberTabs = _memberTabAccessor.SelectMemberTabsByMemberID(memberID).ToList();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return memberTabs;
		}

		/// <summary author="Jared Greenfield" created="2019/04/30">
		/// Select last tab member had.
		/// </summary>
		/// <param name="memberID"></param>
		/// <returns></returns>
		public MemberTab RetrieveLastMemberTabByMemberID(int memberID)
		{
			MemberTab tab = null;
			try
			{
				tab = _memberTabAccessor.SelectLastMemberTabByMemberID(memberID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return tab;
		}

		/// <summary name="Carlos Arzu" created="2019/04/26">
		/// Retrieve list of Shops.
		/// </summary>
		public List<string> retrieveShops()
		{
			List<string> dataString;
			try
			{

				dataString = _memberTabAccessor.SelectShop();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw;
			}
			return dataString;
		}

		/// <summary name="Carlos Arzu" created="2019/04/26">
		/// Retrieve ShopID.
		/// </summary>
		public int retrieveShopID(string name)
		{
			int ID = 0;

			try
			{

				ID = _memberTabAccessor.SelectShopID(name);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw;
			}
			return ID;
		}

		/// <summary name="Carlos Arzu" created="2019/04/26">
		/// Retrieve list of members.
		/// </summary>
		public DataTable retrieveOfferings(int shopID)
		{
			DataTable memberTable;

			try
			{

				memberTable = _memberTabAccessor.selectOfferings(shopID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw;
			}
			return memberTable;
		}

		/// <summary name="Carlos Arzu" created="2019/04/26">
		/// Retrieve list of members.
		/// </summary>
		public DataTable retrieveSearchMembers(string data)
		{
			DataTable memberTable;

			try
			{

				memberTable = _memberTabAccessor.SelectSearchMember(data);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw;
			}
			return memberTable;
		}
	}
}