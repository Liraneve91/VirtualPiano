using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoTutorial.Model
{
    public class RadioModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string header = string.Empty;
        public string Header
        {
            get { return header; }
            set
            {
                if (value != this.header)
                    header = value;
                NotifyPropertyChanged("Header");
            }
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
