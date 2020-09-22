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
        public bool IsLoggedIn = false;
        User user;

        public MainPage()
        {
            this.InitializeComponent();
            mainPageViewModel = new MainPageViewModel();
            userService = new UserService();
            mainPageViewModel.GetAccommodations();
            
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

        private async void CreateBooking_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoggedIn == true)
            {
                mainPageViewModel.CreateBooking();
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

        private async void Login_Button_Click(object sender, RoutedEventArgs e)
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
                mainPageViewModel.User = new User(userName);
            }
        }
        private void MyBookings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MyBookings));
        }
    }
}
