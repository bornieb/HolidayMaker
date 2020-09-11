﻿using System;
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
using System.Linq;

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
            mainPageViewModel.MockData();
        }

        private void accListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Accommodation ac = accListView.SelectedItem as Accommodation;

            foreach (var item in ac.Rooms)
            {
                ListOfRooms.Add(item);
            }
        }

        private void BookRoom_Clicked(object sender, RoutedEventArgs e)
        {
            Room bookedRoom = roomListView.SelectedItem as Room;

            string roomtype = bookedRoom.RoomType.ToString();
            string price = bookedRoom.Price.ToString();
            bookingTextBlock.Text = roomtype + " " + price;
            var foo = 0;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            accListView.ItemsSource = mainPageViewModel.SearchResult;
            mainPageViewModel.SearchFunction(SearchTextBox.Text);
        }

        private void SortingButton_Click(object sender, RoutedEventArgs e)
        {
            var sorted = mainPageViewModel.SearchResult.OrderBy(x => x.Rating);
            accListView.ItemsSource = sorted;
        }
    }
}
