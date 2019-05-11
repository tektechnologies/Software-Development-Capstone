﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace Presentation
{
    /// <summary>
    /// Kevin Broskow
    /// Interaction logic for OrderRecieving.xaml
    /// </summary>
    public partial class OrderRecieving : Window
    {

        SupplierOrderManager _supplierManager = new SupplierOrderManager();
        ReceivingTicketManager _receivingManager = new ReceivingTicketManager();
        SpecialOrderManagerMSSQL _specialManager = new SpecialOrderManagerMSSQL();

        List<SpecialOrderLine> originalSpecialOrderLine;
        List<SpecialOrderLine> specialOrderLines;
        List<SupplierOrderLine> supplierOrderLine;
        private SupplierOrderLine _line = new SupplierOrderLine();
        private SupplierOrder order = new SupplierOrder();
        private CompleteSpecialOrder _specialOrder = new CompleteSpecialOrder();
        private SpecialOrderLine _specialLine = new SpecialOrderLine();
        ReceivingTicket ticket = new ReceivingTicket();
        ReceivingTicket originalTicket = new ReceivingTicket();
        bool orderComplete = true;

        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 3/25/2019
        /// Handles the event of the cancel button being clicked
        /// 
        /// </summary>
        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 3/25/2019
        /// Handles the event of the submit button being clicked
        /// 
        /// </summary>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            if (this.btnSubmit.Content.Equals("Submit"))
            {
                ticket.SupplierOrderID = order.SupplierOrderID;
                ticket.ReceivingTicketExceptions = this.txtException.Text;
                ticket.ReceivingTicketCreationDate = DateTime.Now;
                for (int i = 0; i < dgOrderRecieving.Items.Count - 1; i++)
                {
                    dgOrderRecieving.SelectedIndex = i;
                    SupplierOrderLine temp = (SupplierOrderLine)dgOrderRecieving.SelectedItem;
                    var _tempLine = supplierOrderLine.Find(x => x.ItemID == temp.ItemID);
                    _tempLine.QtyReceived = temp.QtyReceived;
                    if (_tempLine.OrderQty != temp.QtyReceived)
                    {
                        orderComplete = false;
                    }
                    supplierOrderLine.Find(x => x.ItemID == temp.ItemID).QtyReceived = temp.QtyReceived;
                }

                try
                {
                    order.OrderComplete = orderComplete;

                    _supplierManager.UpdateSupplierOrder(order, supplierOrderLine);

                    _receivingManager.createReceivingTicket(ticket);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (this.btnSubmit.Content.Equals("Save"))
            {
                ticket.ReceivingTicketExceptions = this.txtException.Text;
                ticket.ReceivingTicketCreationDate = DateTime.Now;
                for (int i = 0; i < dgOrderRecieving.Items.Count - 1; i++)
                {
                    dgOrderRecieving.SelectedIndex = i;
                    SupplierOrderLine temp = (SupplierOrderLine)dgOrderRecieving.SelectedItem;
                    var _tempLine = supplierOrderLine.Find(x => x.ItemID == temp.ItemID);
                    _tempLine.QtyReceived = temp.QtyReceived;
                    if (_tempLine.OrderQty != temp.QtyReceived)
                    {
                        orderComplete = false;
                    }
                    supplierOrderLine.Find(x => x.ItemID == temp.ItemID).QtyReceived = temp.QtyReceived;
                }

                try
                {
                    order.OrderComplete = orderComplete;

                    _supplierManager.UpdateSupplierOrder(order, supplierOrderLine);

                    _receivingManager.updateReceivingTicket(originalTicket, ticket);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (this.btnSubmit.Content.Equals("Complete"))
            {
                ticket.SupplierOrderID = _specialOrder.SpecialOrderID;
                ticket.ReceivingTicketExceptions = this.txtException.Text;
                ticket.ReceivingTicketCreationDate = DateTime.Now;
                for (int i = 0; i < dgOrderRecieving.Items.Count - 1; i++)
                {
                    dgOrderRecieving.SelectedIndex = i;
                    SpecialOrderLine temp = (SpecialOrderLine)dgOrderRecieving.SelectedItem;
                    var _tempLine = specialOrderLines.Find(x => x.NameID == temp.NameID);
                    _tempLine.QtyReceived = temp.QtyReceived;
                    if (_tempLine.OrderQty != temp.QtyReceived)
                    {
                        orderComplete = false;
                    }
                    specialOrderLines.Find(x => x.NameID == temp.NameID).QtyReceived = temp.QtyReceived;
                }

                try
                {
                    if (orderComplete)
                    {

                        _specialManager.EditSpecialOrder(_specialOrder, _specialOrder);
                    }
                    int i = 0;
                    foreach (var item in specialOrderLines)
                    {
                        _specialManager.EditSpecialOrderLine(originalSpecialOrderLine[i], item);
                        i++;
                    }

                    //_receivingManager.createReceivingTicket(ticket);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            this.Close();

        }
        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 3/25/2019
        /// Real constructor for the window being opened for a supplier order being received
        /// 
        /// </summary>
        public OrderRecieving(SupplierOrder supplierOrder)
        {
            order = supplierOrder;
            try
            {
                supplierOrderLine = _supplierManager.RetrieveAllSupplierOrderLinesBySupplierOrderID(supplierOrder.SupplierOrderID);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            InitializeComponent();
            this.lblRecieving.Content += supplierOrder.SupplierOrderID.ToString();
            this.btnSubmit.Content = "Submit";
            dgOrderRecieving.ItemsSource = supplierOrderLine;

        }
        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 3/25/2019
        /// Real constructor for the window being opened on a special order being recieved
        /// 
        /// </summary>
        public OrderRecieving(CompleteSpecialOrder specialOrder)
        {
            _specialOrder = specialOrder;
            try
            {
                specialOrderLines = _specialManager.RetrieveOrderLinesByID(_specialOrder.SpecialOrderID);
                originalSpecialOrderLine = _specialManager.RetrieveOrderLinesByID(_specialOrder.SpecialOrderID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            InitializeComponent();
            this.lblRecieving.Content += _specialOrder.SpecialOrderID.ToString();
            this.btnSubmit.Content = "Complete";
            dgOrderRecieving.ItemsSource = specialOrderLines;
        }
        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 3/25/2019
        /// Real constructor for the window being opened
        /// 
        /// </summary>
        public OrderRecieving(ReceivingTicket ticket)
        {
            originalTicket = ticket;
            try
            {
                supplierOrderLine = _supplierManager.RetrieveAllSupplierOrderLinesBySupplierOrderID(ticket.SupplierOrderID);
                order = _supplierManager.RetrieveSupplierOrderByID(ticket.SupplierOrderID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            InitializeComponent();
            this.lblRecieving.Content += ticket.SupplierOrderID.ToString();

            this.btnSubmit.Content = "Save";
            dgOrderRecieving.ItemsSource = supplierOrderLine;
        }

        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 3/25/2019
        /// Method to handle the column headings
        /// 
        /// </summary>
        private void dgOrderRecieving_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "MM/dd/yy";
            }

            string headerName = e.Column.Header.ToString();

            if (headerName == "")
            {
                e.Cancel = true;
            }
            if (headerName == "QtyReceived")
            {
                e.Column.IsReadOnly = false;
            }
            if (headerName == "SupplierOrderID")
            {
                e.Cancel = true;
            }
            if (headerName == "ItemID")
            {
                e.Column.IsReadOnly = true;
            }
            if (headerName == "Description")
            {
                e.Column.IsReadOnly = true;
            }
            if (headerName == "OrderQty")
            {
                e.Column.IsReadOnly = true;
            }
            if (headerName == "UnitPrice")
            {
                e.Cancel = true;
            }
        }
    }
}
