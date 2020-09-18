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
        public event PropertyChangedEventHandler PropertyChanged;

        public LogInView LogInView
        {
            get
            {
                return LogInView;
            }
            set
            {
                LogInView = value;
                NotifyPropertyChanged(nameof(LogInView));
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
