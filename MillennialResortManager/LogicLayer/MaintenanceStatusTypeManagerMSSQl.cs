﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Dalton Cleveland" created="2019/03/05">
	/// MaintenanceStatusTypeManagerMSSQL Is an implementation of the IMaintenanceStatusTypeManager Interface meant to interact with the MSSQL MaintenanceStatusType
	/// </summary>
	public class MaintenanceStatusTypeManagerMSSQL : IMaintenanceStatusTypeManager
    {
        private MaintenanceStatusTypeAccessorMSSQL _maintenanceStatusTypeAccessor;

		/// <summary author="Dalton Cleveland" created="2019/03/05">
		/// Constructor allowing for non-static method calls
		/// </summary>
		public MaintenanceStatusTypeManagerMSSQL()
        {
            _maintenanceStatusTypeAccessor = new MaintenanceStatusTypeAccessorMSSQL();
        }

        public void AddMaintenanceStatusType(MaintenanceStatusType newMaintenanceStatusType)
        {
            throw new NotImplementedException();
        }

        public void DeleteMaintenanceStatusType()
        {
            throw new NotImplementedException();
        }

        public List<MaintenanceStatusType> RetrieveAllMaintenanceStatusTypes()
        {
            List<MaintenanceStatusType> maintenanceStatusTypes;
            try
            {
                maintenanceStatusTypes = _maintenanceStatusTypeAccessor.RetrieveAllMaintenanceStatusTypes();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return maintenanceStatusTypes;
        }

        MaintenanceStatusType IMaintenanceStatusTypeManager.RetrieveMaintenanceStatusType()
        {
            throw new NotImplementedException();
        }
    }
}