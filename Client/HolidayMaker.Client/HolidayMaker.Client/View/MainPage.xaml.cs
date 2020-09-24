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
using HolidayMaker.Client.Service;
using System.Threading.Tasks;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HolidayMaker.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MainPageViewModel mainPageViewModel;
        UserService userService;
        public ObservableCollection<Room> ListOfRooms = new ObservableCollection<Room>();
        public ObservableCollection<BookedRoom> ListOfUserRooms = new ObservableCollection<BookedRoom>();

        public bool IsLoggedIn = false;
        User user;

        public MainPage()
        {
            this.InitializeComponent();
            mainPageViewModel = new MainPageViewModel();
            userService = new UserService();
            mainPageViewModel.GetAccommodations();
            mainPageViewModel.GetBookings();
            ShowBookingButton();
        }

        private void accListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (accListView.SelectedItem == null)
            {
                return;
            }

            //Accommodation ac = accListView.SelectedItem as Accommodation;
            ListOfRooms.Clear();

            var ac = (Accommodation)accListView.SelectedItem;

            mainPageViewModel.GetAvailableRooms(ac, CheckInDate.Date.DateTime, CheckOutDate.Date.DateTime.Date);

        }

        private void AddRoom_Clicked(object sender, RoutedEventArgs e)
        {
            Room clickedRoom = roomListView.SelectedItem as Room;
            Accommodation clickedAccommodation = accListView.SelectedItem as Accommodation;
            clickedRoom.IsAvailable = false;
            ListOfRooms.Remove(clickedRoom);
            mainPageViewModel.AddToBooking(clickedRoom, clickedAccommodation);
            BookingListview.ItemsSource = mainPageViewModel.AddedRooms;
            ShowBookingButton();
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

        private async void CreateBooking_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoggedIn == true)
            {
                mainPageViewModel.CreateBooking(CheckInDate.Date.DateTime, CheckOutDate.Date.DateTime);
            }
            else
            {
                await new MessageDialog("Du måste logga in").ShowAsync();
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await RegisterContent.ShowAsync();
            //this.Frame.Navigate(typeof(BlankPage1));
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

        private void MenuFlyoutItem_Click_Name_Descend(object sender, RoutedEventArgs e)
        {
            var sorted = mainPageViewModel.SearchResult.OrderByDescending(x => x.AccommodationName);
            accListView.ItemsSource = sorted;
        }

       

        private async void RegisterContent_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string userEmail = EmailTextbox.Text;

            string password = PasswordTextbox.Password;

            string confirmPassword = ConfirmPasswordTextbox.Password;

            if (string.IsNullOrEmpty(EmailTextbox.Text))
            {
                args.Cancel = true;
                ErrorTextBlock.Text = "Email is a required field";
            }
            else if (string.IsNullOrEmpty(PasswordTextbox.Password))
            {
                args.Cancel = true;
                PasswordErrorTextBlock.Text = "Password is a required field";
            } 

            if (password == confirmPassword)
            {
                PasswordTextBlock.Text = "";
                ConfirmPasswordTextBlock.Text = "";
                User user = new User(userEmail, password);
                await userService.PostRegisterUser(user);
                PasswordTextbox.Password = "";
                ConfirmPasswordTextbox.Password = "";
                EmailTextbox.Text = "";
            }
            else
            {
                args.Cancel = true;
                PasswordTextBlock.Text = "Passwords don't match";
                ConfirmPasswordTextBlock.Text = "Passwords don't match";
            }

          
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            await LoginContent.ShowAsync();
        }

        

        private async void Register_Hyperbutton_Click(object sender, RoutedEventArgs e)
        {
            LoginContent.Hide();
            await RegisterContent.ShowAsync();
        }

        private async void LoginContent_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string userName = UserEmailTextbox.Text;
            string password = UserPasswordTextbox.Password;

            var response = await userService.LogIn(userName, password);

            if (response == true)
            {
                IsLoggedIn = true;
                TitleTextBlock.Text = "Logged in as:";
                mainPageViewModel.User = new User(userName);
                LoggedInUser.Text = $"{userName}";
            }
            if (IsLoggedIn == true)
            {
                LoggedInVisibility();
            }
            
          
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            IsLoggedIn = false;
            mainPageViewModel.User = null;
            LoggedOutVisibility();
        }

          
        private void MyBookingsButton_Click(object sender, RoutedEventArgs e)
        {
            MyBookingsText.Visibility = Visibility.Visible;
            //this.Frame.Navigate(typeof(MyBookings));
        }

        private void LoggedInVisibility()
        {
            LoginButton.Visibility = Visibility.Collapsed;
            FirstAppBar.Visibility = Visibility.Collapsed;
            SecAppBar.Visibility = Visibility.Collapsed;
            ThirdAppBar.Visibility = Visibility.Visible;
            FourthAppBar.Visibility = Visibility.Visible;
            RegisterButton.Visibility = Visibility.Collapsed;
            MyBookingsButton.Visibility = Visibility.Visible;
            LogoutButton.Visibility = Visibility.Visible;
        }

        private void LoggedOutVisibility()
        {
            TitleTextBlock.Text = "Welcome";
            LoggedInUser.Text = "";
            LoginButton.Visibility = Visibility.Visible;
            FirstAppBar.Visibility = Visibility.Visible;
            SecAppBar.Visibility = Visibility.Visible;
            ThirdAppBar.Visibility = Visibility.Collapsed;
            FourthAppBar.Visibility = Visibility.Collapsed;
            RegisterButton.Visibility = Visibility.Visible;
            MyBookingsButton.Visibility = Visibility.Collapsed;
            LogoutButton.Visibility = Visibility.Collapsed;
            MyBookingsText.Visibility = Visibility.Collapsed;
        }

        public void ShowBookingButton()
        {
            if(mainPageViewModel.AddedRooms.Count == 0)
            {
                CreateBooking.Visibility = Visibility.Collapsed;
            }
            else
            {
                CreateBooking.Visibility = Visibility.Visible;
            }
        }

        #region MyBookings
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
        #endregion
    }
}
