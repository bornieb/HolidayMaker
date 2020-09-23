using HolidayMaker.Client.Model;
using HolidayMaker.Client.Service;
using HolidayMaker.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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

        private async void SaveBookingButton_Click(object sender, RoutedEventArgs e)
        {
            //var booking = (Booking)bookingsListview.SelectedItem;

            if (bookingsListview.SelectedItem != null)
            {
                MessageDialog msg = new MessageDialog("Update information?");
                msg.Commands.Clear();
                msg.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                msg.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });

                var result = await msg.ShowAsync(); 

                if ((int)result.Id == 0)
                {
                    string bookingInformation = MyBookingsViewModel.PrintBookingInfo((Booking)bookingsListview.SelectedItem);
                    var booking = (Booking)bookingsListview.SelectedItem;

                    await bookingsViewModel.UpdateBooking(booking);

                    MessageDialog msg2 = new MessageDialog(bookingInformation, "Updated booking");

                    await msg2.ShowAsync();

                }
            }

            //bookingsRoomListview.Items.Clear();
            bookingsViewModel.GetBookings();
        }

        private async void DeleteBookingButton_Click(object sender, RoutedEventArgs e)
        {

            if (bookingsListview.SelectedItem != null)
            {
                MessageDialog msg = new MessageDialog("Remove booking permanently?", "Remove booking");
                msg.Commands.Clear();
                msg.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                msg.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });

                var result = await msg.ShowAsync();

                if ((int)result.Id == 0)
                {
                    string bookingInformation = MyBookingsViewModel.PrintBookingInfo((Booking)bookingsListview.SelectedItem);
                    var booking = (Booking)bookingsListview.SelectedItem;

                    await bookingsViewModel.DeleteBooking(booking);

                    MessageDialog msg2 = new MessageDialog(bookingInformation, "Deleted booking information");
                    await msg2.ShowAsync();

                }
            }
            
            //bookingsViewModel.ListOfUserBookings.Clear();
            bookingsViewModel.GetBookings();

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            On_BackRequested();
        }

        private bool On_BackRequested()
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                return true;
            }
            return false;
        }
    }
}
