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
        MainPageViewModel mainPageViewModel;
        public ObservableCollection<BookedRoom> ListOfUserRooms = new ObservableCollection<BookedRoom>();
        public MyBookings()
        {
            this.InitializeComponent();
            mainPageViewModel = new MainPageViewModel();
            //bookingsViewModel.Mockdata();
            mainPageViewModel.GetBookings();
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
                MessageDialog msg = new MessageDialog("Updates the booking information", "Update information?");
                msg.Commands.Clear();
                msg.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                msg.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });

                var result = await msg.ShowAsync(); 

                if ((int)result.Id == 0)
                {
                    string bookingInformation = MainPageViewModel.PrintUpdatedInfo((Booking)bookingsListview.SelectedItem);
                    var booking = (Booking)bookingsListview.SelectedItem;

                    await mainPageViewModel.UpdateBooking(booking);

                    MessageDialog msg2 = new MessageDialog(bookingInformation, "Updated booking");

                    await msg2.ShowAsync();

                }
            }

            //bookingsRoomListview.Items.Clear();
            //bookingsViewModel.ListOfUserBookings.Clear();
            
            mainPageViewModel.GetBookings();
        }

        private async void DeleteBookingButton_Click(object sender, RoutedEventArgs e)
        {

            if (bookingsListview.SelectedItem != null)
            {
                MessageDialog msg = new MessageDialog("Removes this booking permanently", "Remove booking?");
                msg.Commands.Clear();
                msg.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                msg.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });

                var result = await msg.ShowAsync();

                if ((int)result.Id == 0)
                {
                    string bookingInformation = MainPageViewModel.PrintBookingInfo((Booking)bookingsListview.SelectedItem);
                    var booking = (Booking)bookingsListview.SelectedItem;

                    await mainPageViewModel.DeleteBooking(booking);

                    MessageDialog msg2 = new MessageDialog(bookingInformation, "Removed booking information");
                    await msg2.ShowAsync();

                }
            }
            
            //bookingsViewModel.ListOfUserBookings.Clear();
            mainPageViewModel.GetBookings();

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

        private async void DeleteRoomButton_Click(object sender, RoutedEventArgs e)
        {

            if (bookingsListview.SelectedItem != null)
            {
                MessageDialog msg = new MessageDialog("Removes the room from this booking permanently", "Remove room?");
                msg.Commands.Clear();
                msg.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                msg.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });

                var result = await msg.ShowAsync();

                if ((int)result.Id == 0)
                {

                    var deleteRoom = (BookedRoom)bookingsRoomListview.SelectedItem;
                    var ac = (Booking)bookingsListview.SelectedItem;
                    ac.BookedRooms.Remove(deleteRoom);
                    ListOfUserRooms.Remove(deleteRoom);

                    MessageDialog msg2 = new MessageDialog("The room was successfully removed", "Removed");

                    await msg2.ShowAsync();

                }
            }
           // var deleteRoom = (BookedRoom)bookingsRoomListview.SelectedItem;
           // var ac = (Booking)bookingsListview.SelectedItem;
           // ac.BookedRooms.Remove(deleteRoom);
           // ListOfUserRooms.Remove(deleteRoom);
        }
    }
}
