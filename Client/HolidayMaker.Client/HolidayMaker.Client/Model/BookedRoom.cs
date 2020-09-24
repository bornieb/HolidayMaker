using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMaker.Client.Model
{
    public class BookedRoom : INotifyPropertyChanged
    {
        public int RoomId { get; set; }
        public string AccommodationName { get; set; }
        public string City { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public bool ExtraBedBooked { get; set; }
        public bool FullBoard { get; set; }
        public bool HalfBoard { get; set; }
        public bool AllInclusive { get; set; }
        private decimal displayTotal = 0;

        public decimal TotalPriceRoom 
        {
            get { return TotalPriceRoomMethod(); }
            set {
                displayTotal = value;
                NotifyPropertyChanged("TotalPriceRoom");
                }
        }

        public decimal TotalPriceRoomMethod()
        {
            decimal totalPriceRoom = Price;
            if (ExtraBedBooked)
            {
                totalPriceRoom +=100;
            }
            if (FullBoard)
            {
                totalPriceRoom +=100;
            }
            else if (HalfBoard)
            {
                totalPriceRoom +=50;
            }
            else if(AllInclusive)
            {
                totalPriceRoom +=150;
            }

            return totalPriceRoom;
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
