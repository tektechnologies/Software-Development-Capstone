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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for BrowseShuttleReservation.xaml
    /// </summary>
    public partial class BrowseShuttleReservation : Window
    {


        private IShuttleReservationManager _shuttleReservationManager;
        private List<ShuttleReservation> _shuttleReservations;
        private List<ShuttleReservation> _currentShuttleReservations;
        private ShuttleReservation _shuttleReservation = new ShuttleReservation();
        //private List<Guest> _guestList;

       
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// Default constructor:  BrowseshuttleReservation.
        /// </summary>
        public BrowseShuttleReservation()
        {
            _shuttleReservationManager = new ShuttleReservationManager();
        
            InitializeComponent();
            
        }
      
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// constructor: BrowseShuttleReservation with one parameter.
        /// </summary>
        public BrowseShuttleReservation(IShuttleReservationManager shuttleReservationManager = null)
        {
            if(shuttleReservationManager == null)
            {
                _shuttleReservationManager = new ShuttleReservationManager();
            }

            _shuttleReservationManager = shuttleReservationManager;


            InitializeComponent();
        }
       
       

     
       
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method to refresh browse shuttlereservations.
        /// </summary>
        private void refreshShuttleReservation()
        {
            try
            {
                _shuttleReservations = _shuttleReservationManager.RetrieveAllShuttleReservations();

                _currentShuttleReservations = _shuttleReservations;
               
                dgShuttleReservation.ItemsSource = _currentShuttleReservations;
                dgShuttleReservation.Items.Refresh();
                filterShuttleReservations();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

       



        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// //method to call the filter method
        /// </summary>

        private void BtnFilterShuttleReservation_Click(object sender, RoutedEventArgs e)
        {
            filterShuttleReservations();
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// //method to filter the shuttleReservation list
        /// </summary>
        private void filterShuttleReservations()
        {

            IEnumerable<Guest> _currentGuestLists = _shuttleReservations.Select(s => s.Guest);
       IEnumerable<ShuttleReservation> _currentLists = _shuttleReservations;
            try
            {
               
                
                if (txtSearchShuttleReservation.Text.ToString() != "")
                {
                   
                    if (txtSearchShuttleReservation.Text != "" && txtSearchShuttleReservation.Text != null)
                    {
                        _currentLists = _currentLists.Where(b => b.PickupLocation.ToLower().Contains(txtSearchShuttleReservation.Text.ToLower())).ToList();

                        
                    }
                }
              
                if (txtSearchLastNameShuttleReservation.Text.ToString() != "")
                {

                    if (txtSearchLastNameShuttleReservation.Text != "" && txtSearchLastNameShuttleReservation.Text != null)
                    {
                        
                        _currentLists = _currentLists.Where(s => s.Guest.LastName.ToLower().Contains(txtSearchLastNameShuttleReservation.Text.ToLower()));
                       
                    }
                }
                
             
                if (dtpSearchDate.Text != null & dtpSearchDate.Text != "")
                {
                    //  DateTime date = TimeZone.Now;
                    _currentLists = _currentLists.Where(d => d.PickupDateTime.ToString().Contains(dtpSearchDate.Text.ToLower())).ToList();
                }
                    
                 
                 if (cbActiveShuttleReservation.IsChecked == true && cbDeactiveShuttleReservation.IsChecked == false)
                {
                    _currentLists = _currentLists.Where(b => b.Active == true);
                }
                else if (cbActiveShuttleReservation.IsChecked == false && cbDeactiveShuttleReservation.IsChecked == true)
                {
                    _currentLists = _currentLists.Where(b => b.Active == false);
                }
                else if (cbActiveShuttleReservation.IsChecked == false && cbDeactiveShuttleReservation.IsChecked == false)
                {
                    _currentLists = _currentLists.Where(b => b.Active == false && b.Active == true);
                }

                dgShuttleReservation.ItemsSource = null;

                dgShuttleReservation.ItemsSource = _currentLists;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);


            }

        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// //method to clear the filters
        /// </summary>
        private void BtnClearSetupListShuttleReservation_Click(object sender, RoutedEventArgs e)
        {
            cbDeactiveShuttleReservation.IsChecked = true;
            cbActiveShuttleReservation.IsChecked = true;
            txtSearchShuttleReservation.Text = "";
            txtSearchLastNameShuttleReservation.Text = "";
            dtpSearchDate.Text = "";
            _currentShuttleReservations = _shuttleReservations;

            dgShuttleReservation.ItemsSource = _currentShuttleReservations;

        }



    
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// //method to cancel and exit a window
        /// </summary>
        private void BtnCancelShuttleReservation_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to quit?", "Closing Application", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if(result == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

     

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method window loaded to refresh  shuttle reservation lists
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refreshShuttleReservation();
      
            
        }

       


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// //method to open the update  dialog
        /// </summary>
        private void BtnUpdateShuttleReservation_Click(object sender, RoutedEventArgs e)
        {

            if (dgShuttleReservation.SelectedItem != null)
            {
                _shuttleReservation = (ShuttleReservation)dgShuttleReservation.SelectedItem;


               var assign = new ShuttleReservationDetail(_shuttleReservation);
              assign.ShowDialog();
            }
            else
            {

                MessageBox.Show("You must select an item first");

            }
            refreshShuttleReservation();
        }
      
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/10
        /// 
        /// method to open the create shuttlereservation dialog.
        /// </summary>
        private void BtnAddShuttleReservation_Click(object sender, RoutedEventArgs e)
        {


            var detailForm = new ShuttleReservationDetail();

            var result = detailForm.ShowDialog();// need to be added



            if (result == true)
            {

                MessageBox.Show(result.ToString());
            }
        refreshShuttleReservation();

    }
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/10
        /// 
        /// //method to Deactivate shuttleReservation
        /// </summary>
        private void BtnDeactivateShuttleReservation_Click(object sender, RoutedEventArgs e)
        {

            if (dgShuttleReservation.SelectedItem != null)
            {
                ShuttleReservation current = (ShuttleReservation)dgShuttleReservation.SelectedItem;

                try
                {
                    if (current.Active == true)
                    {
                        var result = MessageBox.Show("Are you sure that you want to cancel this shuttle reservation?", "cancel ShuttleReservation", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            _shuttleReservationManager.DeactivateShuttleReservation(current.ShuttleReservationID, current.Active);
                        }
                    }
                    
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }



            }
            else
            {

                MessageBox.Show("You must select an item first");

            }
            refreshShuttleReservation();

        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/10
        /// 
        /// //method to filter active shuttle reservation
        /// </summary>
        private void CbDeactiveShuttleReservation_Click(object sender, RoutedEventArgs e)
        {
            filterShuttleReservations();
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/10
        /// 
        /// //method to filter inactive shuttle reservation
        /// </summary>
        private void CbActiveShuttleReservation_Click(object sender, RoutedEventArgs e)
        {
            filterShuttleReservations();
        }

    }


}







