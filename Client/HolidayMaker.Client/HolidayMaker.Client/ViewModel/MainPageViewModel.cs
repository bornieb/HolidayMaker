using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HolidayMaker.Client.Model;

namespace HolidayMaker.Client.ViewModel
{
    public class MainPageViewModel
    {
        public ObservableCollection<Accommodation> ListOfAccommodations = new ObservableCollection<Accommodation>();

        public void MockData()
        {
            ListOfAccommodations.Add(new Accommodation("Eriks Lya", "Malmö"));
            ListOfAccommodations.Add(new Accommodation("Rays Lya", "Eslöv"));
        }

    }
}
