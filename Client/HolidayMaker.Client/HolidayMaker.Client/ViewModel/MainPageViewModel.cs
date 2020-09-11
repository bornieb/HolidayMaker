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
        public ObservableCollection<Accommodation> SearchResult = new ObservableCollection<Accommodation>();
        public void MockData()
        {
            ListOfAccommodations.Add(new Accommodation("Erics Lya", "Malmö", 1.7m));
            ListOfAccommodations.Add(new Accommodation("Rays Lya", "Eslöv", 0.2m));
            ListOfAccommodations.Add(new Accommodation("Mickes hak", "Hjärup", 2.5m));
            ListOfAccommodations.Add(new Accommodation("Jennys Etage", "Los Angeles", 4.9m));
            ListOfAccommodations.Add(new Accommodation("Glenns koja", "Vardagsrummet", 5m));
        }

        public void SearchFunction(string search)
        {
            SearchResult.Clear();

            if (search == "")
            {
                SearchResult.Clear();
            }
            else
            {
                foreach (var s in ListOfAccommodations)
                {
                    if (s.AccommodationName.ToLower().Contains(search.ToLower())
                        || s.City.ToLower().Contains(search.ToLower()))
                    {
                        SearchResult.Add(new Accommodation(s.AccommodationName, s.City, s.Rating));
                    }
                }
            }
        }

        public void SortingFunction()
        {
            var sorted = SearchResult.OrderByDescending(x => x.Rating);
        }
    }
}
