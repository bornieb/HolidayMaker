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
using Windows.UI.Popups;
using HolidayMaker.Client.Service;
using HolidayMaker.Client.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HolidayMaker.Client.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterView : Page
    {
        UserService registerUserService = new UserService();
        public RegisterView()
        {
            this.InitializeComponent();
            
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string userFirstName = FirstNameTextbox.Text;

            string userLastName = LastNameTextbox.Text;

            string userEmail = EmailTextbox.Text;

            string password = PasswordTextbox.Password;

            string confirmPassword = ConfirmPasswordTextbox.Password;

            if (password == confirmPassword)
            {
                PasswordTextBlock.Text = "";
                ConfirmPasswordTextBlock.Text = "";
            }
            else
            {
                PasswordTextBlock.Text = "Passwords don't match";
                ConfirmPasswordTextBlock.Text = "Passwords don't match";
            }

            User user = new User(userEmail, password);


            await registerUserService.PostRegisterUserAsync(user);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
