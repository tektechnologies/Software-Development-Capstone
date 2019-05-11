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
	/// <summary author="Matthew Hill" created="2019/04/26">
	/// Manager Class that calls the MemberTabLineAccessor to populate a
	/// MemberTabLine data object with data.
	/// Implements the IMemberTabLine Interface.
	/// </summary>
	public class MemberTabLineManager : IMemberTabLineManager
	{
		private IMemberTabLineAccessor _memberTabLineAccessor;
		public List<MemberTabLine> RetrieveMemberTabLineByMemberID(int id)
		{
			List<MemberTabLine> memberTabLines = new List<MemberTabLine>();
			_memberTabLineAccessor = new MemberTabLineAccessor();

			try
			{
				memberTabLines = _memberTabLineAccessor.SelectMemberTabLineByMemberTabID(id);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return memberTabLines;
		}
	}
}
