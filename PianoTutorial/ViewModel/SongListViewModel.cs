using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Reactive.Subjects;
using System.Threading;
using PianoTutorial.Model;

namespace PianoTutorial.ViewModel
{
    public class SongListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Song { get; set; }
        List<Label> items = new List<Label>();
        private bool m_isVisible;
        private ISubject<string> m_songSelected = new Subject<string>();
        private PianoControlViewModel m_pianoControlViewModel;

        public SongListViewModel()
        {
            Label label1 = new Label();
            label1.Content = "Song1";
            label1.Foreground = Brushes.Chocolate;
            label1.FontSize = 30;
            items.Add(label1);

            Label label2 = new Label();
            label2.Foreground = Brushes.Chocolate;
            label2.FontSize = 30;
            label2.Content = "Song2";
            items.Add(label2);

            Label label3 = new Label();
            label3.Foreground = Brushes.Chocolate;
            label3.FontSize = 30;
            label3.Content = "Song3";
            items.Add(label3);
            MySongs = items;

            this.radioCollection = this.setRadio();

        }

        public IObservable<string> SongSelected
        {
            get { return m_songSelected; }
        }

        public bool IsVisible
        {
            get { return m_isVisible; }
            set
            {
                m_isVisible = value;
                if(m_isVisible)
                {
                    //if(MainWindowViewModel.m_chordListViewModel.IsVisible)
                        MainWindowViewModel.m_chordListViewModel.IsVisible = false;
                    //if(MainWindowViewModel.m_settingsWindowViewModel.IsVisible)
                        MainWindowViewModel.m_settingsWindowViewModel.IsVisible = false;
                }
                NotifyPropertyChanged("IsVisible");
            }
        }

        public Label SelectedSong
        {
            set
            {
                if (m_pianoControlViewModel == null)
                    m_pianoControlViewModel = MainWindowViewModel.m_pianoControlViewMode;
                //MainWindowViewModel.m_arduinoLedsManager.StartSong(value.Content.ToString());
            }
        }

        public List<Label> MySongs
        {
            get { return items; }
            set
            {
                items = value;
                NotifyPropertyChanged("MyKeys");
            }

        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #region HeaderString
        private string headerString = string.Empty;
        public string HeaderString
        {
            get { return headerString; }
            set
            {
                if (value != this.headerString)
                    headerString = value;
                NotifyPropertyChanged("HeaderString");
            }
        }
        #endregion HeaderString
        #region SelectedHeader
        private string selectedHeader = string.Empty;
        public string SelectedHeader
        {
            get { return selectedHeader; }
            set
            {
                if (value != this.selectedHeader)
                    selectedHeader = value;
                NotifyPropertyChanged("SelectedHeader");
            }
        }
        #endregion SelectedHeader
        #region SelectedRadio
        private RadioModel selectedRadio;
        public RadioModel SelectedRadio
        {
            get { return selectedRadio; }
            set
            {
                if (value != this.selectedRadio)
                    selectedRadio = value;
                setSelectedStrings();
                //MainWindowViewModel.m_arduinoLedsManager.StartSong(value.Header);
                IsVisible = false;
                selectedRadio = null;
                NotifyPropertyChanged("SelectedRadio");
            }
        }
        #endregion SelectedRadio
        #region RadioCollection
        private ObservableCollection<RadioModel> radioCollection;
        public ObservableCollection<RadioModel> RadioCollection
        {
            get { return radioCollection; }
            set
            {
                if (value != this.radioCollection)
                    radioCollection = value;
                NotifyPropertyChanged("RadioCollection");
            }
        }
        #endregion RadioCollection
        #region RadioCollectionSetter
        private List<RadioModel> radioCollectionSetter;
        public List<RadioModel> RadioCollectionSetter
        {
            get { return radioCollectionSetter; }
            set
            {
                if (value != this.radioCollectionSetter)
                    radioCollectionSetter = value;
                NotifyPropertyChanged("RadioCollectionSetter");
            }
        }
        #endregion RadioCollectionSetter

        #region Methods
        #region void addRadioItemsToList()
        private void addRadioItemsToList()
        {
            if (!string.IsNullOrEmpty(this.headerString))
            {
                this.radioCollectionSetter.Add(new RadioModel { Header = this.headerString });

                NotifyPropertyChanged("RadioCollectionSetter");
                this.radioCollection = new ObservableCollection<RadioModel>(this.radioCollectionSetter);
                NotifyPropertyChanged("RadioCollection");
            }
        }
        #endregion void addRadioItemsToList()

        #region void closeAndRelease()
        private void closeAndRelease()
        {
            this.radioCollection = this.setRadio();
            //Very untidy way of handling this but we only have one view so this is fine...
            Environment.Exit(0);
        }
        #endregion void closeAndRelease()

        #region void setRadio()
        private ObservableCollection<RadioModel> setRadio()
        {
            if (this.radioCollectionSetter == null)
            {
                this.radioCollectionSetter = new List<RadioModel>();
                this.radioCollectionSetter.Add(new RadioModel { Header = "Perfect - Ed Sheeran" });
                this.radioCollectionSetter.Add(new RadioModel { Header = "Happy Birthday to You" });
                this.radioCollection = new ObservableCollection<RadioModel>(this.radioCollectionSetter);
            }
            return this.radioCollection;
        }
        #endregion void setRadio()

        #region void setSelectedStrings()
        private void setSelectedStrings()
        {
            if (this.selectedRadio != null)
            {
                this.selectedHeader = this.selectedRadio.Header;
                m_songSelected.OnNext(selectedHeader);
                NotifyPropertyChanged("SelectedHeader");
            }
        }
        #endregion void setSelectedStrings()
        #endregion Methods
    }
}
