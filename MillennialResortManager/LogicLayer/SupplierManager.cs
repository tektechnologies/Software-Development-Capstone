﻿using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	/// <summary author="James Heim" created="2019/01/24">
	/// Concrete implementation of the ISupplierManager Interface.
	/// </summary>
	public class SupplierManager : ISupplierManager
    {

        private ISupplierAccessor _supplierAccessor;

		/// <summary author="James Heim" created="2019/01/24">s
		/// Default Constructor.
		/// </summary>
		public SupplierManager()
        {
            _supplierAccessor = new SupplierAccessor();
        }

		/// <summary author="James Heim" created="2019/02/12">
		/// Constructor for supplying the mock accessor.
		/// </summary>
		/// <param name="supplierAccessor1"></param>
		public SupplierManager(SupplierAccessorMock mockSupplierAccessor)
        {
            _supplierAccessor = mockSupplierAccessor;
        }

		/// <summary author="James Heim" created="2019/03/08">
		/// Attempts to call ActivateSupplier from the accessor.
		/// </summary>
		/// <param name="supplier"></param>
		public bool ActivateSupplier(Supplier supplier)
        {
            bool result = false;

            try
            {
                result = (1 == _supplierAccessor.ActivateSupplier(supplier));
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
        }

		/// <summary author="James Heim" created="2019/01/24">
		/// Attempts to call InsertSupplier from the SupplierAccessor.
		/// Returns the ID of the newly created Supplier or -1 if something
		/// goes wrong when an exception is not thrown.
		/// </summary>
		/// <updates>
		/// <update author="James Heim" date="2019/02/15">
		/// Added proper validation.
		/// </update>
		/// </updates>
		/// <param name="supplierName">Human friendly name of the Supplier.</param>
		/// <param name="address">Address of the Supplier.</param>
		/// <param name="city">City the Supplier is based in.</param>
		/// <param name="state">State the Supplier is based in.</param>
		/// <param name="zip">Zip code of the Supplier.</param>
		/// <param name="country">Country the Supplier is based in.</param>
		/// <param name="contactFirstName">The first name of our contact for the Supplier.</param>
		/// <param name="contactLastName">The last name of our contact for the Supplier.</param>
		/// <param name="phoneNumber">The phone number of our contact for the Supplier.</param>
		/// <param name="email">The email of our contact for the Supplier.</param>
		/// <returns>The number of rows affected. -1 if something went wrong but an exception is not thrown.</returns>
		public void CreateSupplier(Supplier newSupplier)
        {
            newSupplier.Validate();

            try
            {
                _supplierAccessor.InsertSupplier(newSupplier);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Caitlin Abelson" created="2019/01/23">
		/// RetrieveAllSuppliers takes the list that was returned from the DataAccess Layer
		/// and pushes it to the presentation layer so that it can be displayed in the data grid
		/// </summary>
		/// <returns></returns>
		public List<Supplier> RetrieveAllSuppliers()
        {
            List<Supplier> suppliers = null;

            try
            {
                suppliers = _supplierAccessor.SelectAllSuppliers();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return suppliers;
        }

		/// <summary author="James Heim" created="2019/02/15">
		/// Retrieve a Supplier by the Unique ID.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Supplier RetrieveSupplier(int id)
        {
            Supplier supplier = null;

            try
            {
                supplier = _supplierAccessor.SelectSupplier(id);

                if (supplier == null)
                {
                    throw new NullReferenceException("Supplier does not exist.");
                }
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return supplier;
        }

		/// <summary author="James Heim" created="2019/01/31">
		/// Pass the old supplier and new old supplier to the SupplierAccessor in order
		/// to update the supplier in the database.
		/// </summary>
		/// <param name="newSupplier"></param>
		/// <param name="oldSupplier"></param>
		public void UpdateSupplier(Supplier newSupplier, Supplier oldSupplier)
        {

            newSupplier.Validate();
            oldSupplier.Validate();

            try
            {
                _supplierAccessor.UpdateSupplier(newSupplier, oldSupplier);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="James Heim" created="2019/02/21">
		/// Disable a Supplier.
		/// </summary>
		/// <param name="supplier"></param>
		public bool DeactivateSupplier(Supplier supplier)
        {
            bool result = false;

            try
            {
                result = (1 == _supplierAccessor.DeactivateSupplier(supplier));
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
        }

		/// <summary author="James Heim" created="2019/02/15">
		/// Deactivates an active Supplier or removes an inactive Supplier
		/// from the database.
		/// </summary>
		/// <param name="supplier"></param>
		public bool DeleteSupplier(Supplier supplier)
        {
            bool result = false;

            try
            {
                result = (1 == _supplierAccessor.DeleteSupplier(supplier));
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