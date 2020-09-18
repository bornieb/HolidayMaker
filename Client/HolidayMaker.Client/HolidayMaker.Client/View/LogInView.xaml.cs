using HolidayMaker.Client.Service;
using HolidayMaker.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Bluetooth.Advertisement;
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
    public sealed partial class LogInView : Page
    {
        UserService userService = new UserService();
        LogInViewModel logInViewModel = new LogInViewModel();

        public bool IsLoggedIn = false;
        public string email = "";

        public LogInView()
        {
            this.InitializeComponent();
        }

        private async void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserNameTextBox.Text;
            string password = PasswordTextBox.Password;

            var response = await userService.LogIn(userName, password);

            if(response == true)
            {
                IsLoggedIn = true;
                email = userName;
            }
        }
    }
}
