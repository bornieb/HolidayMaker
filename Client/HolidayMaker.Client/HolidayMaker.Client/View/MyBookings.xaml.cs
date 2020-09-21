using HolidayMaker.Client.Model;
using HolidayMaker.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HolidayMaker.Client.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyBookings : Page
    {
        MyBookingsViewModel bookingsViewModel;
        public ObservableCollection<BookedRoom> ListOfUserRooms = new ObservableCollection<BookedRoom>();
        public MyBookings()
        {
            this.InitializeComponent();
            bookingsViewModel = new MyBookingsViewModel();
            //bookingsViewModel.Mockdata();
            bookingsViewModel.GetBookings();
        }

        private void bookingsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListOfUserRooms.Clear();

            var ac = (Booking)bookingsListview.SelectedItem;

            foreach (var item in ac.BookedRooms)
            {
                ListOfUserRooms.Add(item);
            }
        }

        private void SaveBookingButton_Click(object sender, RoutedEventArgs e)
        {
            var booking = (Booking)bookingsListview.SelectedItem;
            bookingsViewModel.UpdateBooking(booking);
            
        }
    }
}
