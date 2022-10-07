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
    public class ChordListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Chord { get; set; }
        List<Label> items = new List<Label>();
        private bool m_isVisible;
        private ISubject<string> m_chordSelected = new Subject<string>();
        private PianoControlViewModel m_pianoControlViewModel;

        public ChordListViewModel()
        {
            Label label1 = new Label();

            Label label4 = new Label();
            label1.Foreground = Brushes.Chocolate;
            label1.FontSize = 30;
            label1.Content = "A";
            items.Add(label1);
            MyChords = items;

            Label label5 = new Label();
            label1.Foreground = Brushes.Chocolate;
            label1.FontSize = 30;
            label1.Content = "Ab";
            items.Add(label1);
            MyChords = items;

            Label label6 = new Label();
            label1.Foreground = Brushes.Chocolate;
            label1.FontSize = 30;
            label1.Content = "B";
            items.Add(label1);
            MyChords = items;

            Label label7 = new Label();
            label1.Foreground = Brushes.Chocolate;
            label1.FontSize = 30;
            label1.Content = "Bb";
            items.Add(label1);
            MyChords = items;

            label1.Content = "C";
            label1.Foreground = Brushes.Chocolate;
            label1.FontSize = 30;
            items.Add(label1);

            Label label2 = new Label();
            label2.Foreground = Brushes.Chocolate;
            label2.FontSize = 30;
            label2.Content = "C#";
            items.Add(label2);

            //Label label3 = new Label();
            //label3.Foreground = Brushes.Chocolate;
            //label3.FontSize = 30;
            //label3.Content = "C7";
            //items.Add(label3);
            //
            //MyChords = items;

            Label label8 = new Label();
            label1.Foreground = Brushes.Chocolate;
            label1.FontSize = 30;
            label1.Content = "D";
            items.Add(label1);
            MyChords = items;

            Label labe9= new Label();
            label1.Foreground = Brushes.Chocolate;
            label1.FontSize = 30;
            label1.Content = "E";
            items.Add(label1);
            MyChords = items;

            Label label10 = new Label();
            label1.Foreground = Brushes.Chocolate;
            label1.FontSize = 30;
            label1.Content = "Eb";
            items.Add(label1);
            MyChords = items;

            Label label11 = new Label();
            label1.Foreground = Brushes.Chocolate;
            label1.FontSize = 30;
            label1.Content = "F";
            items.Add(label1);
            MyChords = items;

            Label label12 = new Label();
            label1.Foreground = Brushes.Chocolate;
            label1.FontSize = 30;
            label1.Content = "F#";
            items.Add(label1);
            MyChords = items;

            Label label13 = new Label();
            label1.Foreground = Brushes.Chocolate;
            label1.FontSize = 30;
            label1.Content = "G";
            items.Add(label1);
            MyChords = items;

            this.radioCollection = this.setRadio();

        }

        public IObservable<string> ChordSelected
        {
            get { return m_chordSelected; }
        }

        public bool IsVisible
        {
            get { return m_isVisible; }
            set
            {
                m_isVisible = value;
                if (m_isVisible)
                {
                    MainWindowViewModel.m_songListViewModel.IsVisible = false;
                    MainWindowViewModel.m_settingsWindowViewModel.IsVisible = false;
                }
                NotifyPropertyChanged("IsVisible");
            }
        }

        public Label SelectedChord
        {
            set
            {
                if (m_pianoControlViewModel == null)
                    m_pianoControlViewModel = MainWindowViewModel.m_pianoControlViewMode;
                m_pianoControlViewModel.ShowChord(value.Content.ToString());
            }
        }

        public List<Label> MyChords
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
                if (m_pianoControlViewModel == null)
                    m_pianoControlViewModel = MainWindowViewModel.m_pianoControlViewMode;
                m_pianoControlViewModel.ShowChord(value.Header.ToString());
                setSelectedStrings();
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
                this.radioCollectionSetter.Add(new RadioModel { Header = "A" });
                this.radioCollectionSetter.Add(new RadioModel { Header = "Ab" });
                this.radioCollectionSetter.Add(new RadioModel { Header = "B" });
                this.radioCollectionSetter.Add(new RadioModel { Header = "Bb" });
                this.radioCollectionSetter.Add(new RadioModel { Header = "C" });
                this.radioCollectionSetter.Add(new RadioModel { Header = "C#" });
                //this.radioCollectionSetter.Add(new RadioModel { Header = "C7" });
                this.radioCollectionSetter.Add(new RadioModel { Header = "D" });
                this.radioCollectionSetter.Add(new RadioModel { Header = "E" });
                this.radioCollectionSetter.Add(new RadioModel { Header = "Eb" });
                this.radioCollectionSetter.Add(new RadioModel { Header = "F" });
                this.radioCollectionSetter.Add(new RadioModel { Header = "F#" });
                this.radioCollectionSetter.Add(new RadioModel { Header = "G" });
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
                m_chordSelected.OnNext(selectedHeader);
                NotifyPropertyChanged("SelectedHeader");
            }
        }
        #endregion void setSelectedStrings()
        #endregion Methods
    }
}
