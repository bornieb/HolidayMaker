using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMaker.Client.Model
{
    public class Booking : INotifyPropertyChanged
    {
        public string BookingNumber { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalPrice { get; set; }
        public string Email { get; set; }
        private decimal displayTotal = 0;
        public decimal TotalPriceBooking 
        {
            get { return TotalPriceBookingMethod(); }
            set
            {
                displayTotal = value;
                NotifyPropertyChanged("TotalPriceBooking");
            }
        }

        public decimal TotalPriceBookingMethod()
        {
            decimal totalPriceBooking = 0;
            int diff = (CheckOut - CheckIn).Days;
            foreach (var item in BookedRooms)
            {
                totalPriceBooking+=item.TotalPriceRoom*diff;
            }
            

            return totalPriceBooking;
        }


        public ObservableCollection<BookedRoom> BookedRooms { get; set; }

        public Booking()
        {
            BookedRooms = new ObservableCollection<BookedRoom>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}