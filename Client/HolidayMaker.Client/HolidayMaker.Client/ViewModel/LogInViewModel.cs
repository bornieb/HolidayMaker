using HolidayMaker.Client.Model;
using HolidayMaker.Client.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMaker.Client.ViewModel
{
    public class LogInViewModel : INotifyPropertyChanged
    {
        private User user = new User(); 
        public event PropertyChangedEventHandler PropertyChanged;

        public User User
        {
            get
            {
                return this.user;
            }
            set
            {
                this.user = value;
                NotifyPropertyChanged(nameof(User));
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
