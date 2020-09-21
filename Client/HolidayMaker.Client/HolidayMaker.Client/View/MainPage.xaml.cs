using System;
using System.Collections.Generic;
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
using HolidayMaker.Client.ViewModel;
using HolidayMaker.Client.Model;
using System.Security.Cryptography.X509Certificates;
using System.Collections.ObjectModel;
using HolidayMaker.Client.View;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HolidayMaker.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MainPageViewModel mainPageViewModel;
        public ObservableCollection<Room> ListOfRooms = new ObservableCollection<Room>();

        public MainPage()
        {
            this.InitializeComponent();
            mainPageViewModel = new MainPageViewModel();
            //mainPageViewModel.GetAccommodations();
            mainPageViewModel.MockData();
        }

        private void CollapseButton_Click(object sender, RoutedEventArgs e)
        {
            if (SplitviewMenu.IsPaneOpen)
            {
                SplitviewMenu.IsPaneOpen = false;
                CollapseButton.Width = 54;
            }
            else
            {
                SplitviewMenu.IsPaneOpen = true;
                CollapseButton.Width = 130;
            }
        }
        private void accListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Accommodation ac = accListView.SelectedItem as Accommodation;
            ListOfRooms.Clear();

            var ac = (Accommodation)accListView.SelectedItem;

            foreach (var item in ac.Rooms)
            {
                if (item.IsAvailable)
                {
                    ListOfRooms.Add(item);
                }
            }
        }

        private void AddRoom_Clicked(object sender, RoutedEventArgs e)
        {
            Room clickedRoom = roomListView.SelectedItem as Room;
            Accommodation clickedAccommodation = accListView.SelectedItem as Accommodation;

            //string roomtype = bookedRoom.RoomType.ToString();
            //string price = bookedRoom.Price.ToString();
            //bookingTextBlock.Text = roomtype + " " + price;
            //var foo = 0;

            //Booking booking = mainPageViewModel.AddToBooking(clickedRoom, clickedAccommodation);
            //string bookingNumber = booking.BookingNumber.ToString();
            //BookingNumberTextBlock.Text = $"Booking Number:\n";
            clickedRoom.IsAvailable = false;
            ListOfRooms.Remove(clickedRoom);
            mainPageViewModel.AddToBooking(clickedRoom, clickedAccommodation);
            BookingListview.ItemsSource = mainPageViewModel.AddedRooms;

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            accListView.ItemsSource = mainPageViewModel.SearchResult;
            mainPageViewModel.SearchFunction(SearchTextBox.Text);
        }

        //private void SortingButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var sorted = mainPageViewModel.SearchResult.OrderBy(x => x.Rating);
        //    accListView.ItemsSource = sorted;

        //}

        private void SplitViewMenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CreateBooking_Click(object sender, RoutedEventArgs e)
        {
            mainPageViewModel.CreateBooking();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LogInView));
        }

        private void MenuFlyoutItem_Click_Rating(object sender, RoutedEventArgs e)
        {
            
                var sorted = mainPageViewModel.SearchResult.OrderByDescending(x => x.Rating);
                accListView.ItemsSource = sorted;
            
        }

        private void MenuFlyoutItem_Click_Name(object sender, RoutedEventArgs e)
        {
                var sorted = mainPageViewModel.SearchResult.OrderBy(x => x.AccommodationName);
                accListView.ItemsSource = sorted;
        }

       
    }
}
