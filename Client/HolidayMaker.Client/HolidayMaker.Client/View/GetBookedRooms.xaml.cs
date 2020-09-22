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
using System.Collections.ObjectModel;
using HolidayMaker.Client.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HolidayMaker.Client.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GetBookedRooms : Page
    {
        public ObservableCollection<Room> availableRooms = new ObservableCollection<Room>();
        MainPageViewModel mainPageViewModel = new MainPageViewModel();
        public GetBookedRooms()
        {
            this.InitializeComponent();
            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void GetRooms_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
