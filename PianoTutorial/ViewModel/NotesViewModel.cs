using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PianoTutorial.ViewModel
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        private const int START_KEY = 42;
        private const int END_KEY = 63;
        public event PropertyChangedEventHandler PropertyChanged;
        private List<string> m_notesList = new List<string>();
        Dictionary<int, string> m_notesDictionary = new Dictionary<int, string>();
        private ICommand m_clearNotesCommand;
        private bool m_clearNotesCanExecute = true;

        public NotesViewModel()
        {
            createDictionary();
        }

        public ICommand ClearNotes
        {
            get { return m_clearNotesCommand ?? (m_clearNotesCommand = new CommandHandler(() => clearNotesList(), m_clearNotesCanExecute)); }
        }

        private void clearNotesList()
        {
            m_notesList.Clear();
            updateImages();
        }

        public void DrawNote(int key)
        {
            if (key > START_KEY && key < END_KEY)
            {
                int temp = (key - START_KEY - 1);
                if (m_notesList.Count == 12)
                    m_notesList.RemoveAt(0);
                m_notesList.Add(m_notesDictionary[temp+1]);
                updateImages();
            }
        }

        private void createDictionary()
        {
            m_notesDictionary.Add(1, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_1A.png");
            m_notesDictionary.Add(2, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_1Ab.png");
            m_notesDictionary.Add(3, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_1B.png");
            m_notesDictionary.Add(4, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_1Bb.png");
            m_notesDictionary.Add(5, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_1F.png");
            m_notesDictionary.Add(6, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_1F#.png");
            m_notesDictionary.Add(7, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_1G.png");
            m_notesDictionary.Add(8, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_2C#.png");
            m_notesDictionary.Add(9, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_2C.png");
            m_notesDictionary.Add(10, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_2D.png");
            m_notesDictionary.Add(11, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_2E.png");
            m_notesDictionary.Add(12, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_2Eb.png");
            m_notesDictionary.Add(13, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_2F.png");
            m_notesDictionary.Add(14, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_2F#.png");
            m_notesDictionary.Add(15, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_2G.png");
            m_notesDictionary.Add(16, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_3A.png");
            m_notesDictionary.Add(17, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_3Ab.png");
            m_notesDictionary.Add(18, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_3B.png");
            m_notesDictionary.Add(19, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_3Bb.png");
            m_notesDictionary.Add(20, @"C:\Users\liran\Documents\Visual Studio 2015\Projects\PianoTutorial\PianoTutorial\Images\Note_3C.png");
        }

        public string DisplayedImage1Path
        {
            get
            {
                if (m_notesList.Count > 0)
                    return m_notesList[0];
                else
                    return "";
            }
        }

        public string DisplayedImage2Path
        {
            get
            {
                if (m_notesList.Count > 1)
                    return m_notesList[1];
                else
                    return "";
            }
        }

        public string DisplayedImage3Path
        {
            get
            {
                if (m_notesList.Count > 2)
                    return m_notesList[2];
                else
                    return "";
            }
        }

        public string DisplayedImage4Path
        {
            get
            {
                if (m_notesList.Count > 3)
                    return m_notesList[3];
                else
                    return "";
            }
        }

        public string DisplayedImage5Path
        {
            get
            {
                if (m_notesList.Count > 4)
                    return m_notesList[4];
                else
                    return "";
            }
        }

        public string DisplayedImage6Path
        {
            get
            {
                if (m_notesList.Count > 5)
                    return m_notesList[5];
                else
                    return "";
            }
        }

        public string DisplayedImage7Path
        {
            get {
                if (m_notesList.Count > 6)
                    return m_notesList[6];
                else
                    return "";
            }
        }

        public string DisplayedImage8Path
        {
            get
            {
                if (m_notesList.Count > 7)
                    return m_notesList[7];
                else
                    return "";
            }
        }

        public string DisplayedImage9Path
        {
            get
            {
                if (m_notesList.Count > 8)
                    return m_notesList[8];
                else
                    return "";
            }
        }

        public string DisplayedImage10Path
        {
            get
            {
                if (m_notesList.Count > 9)
                    return m_notesList[9];
                else
                    return "";
            }
        }

        public string DisplayedImage11Path
        {
            get
            {
                if (m_notesList.Count > 10)
                    return m_notesList[10];
                else
                    return "";
            }
        }

        public string DisplayedImage12Path
        {
            get
            {
                if (m_notesList.Count > 11)
                    return m_notesList[11];
                else
                    return "";
            }
        }


        private void updateImages()
        {
            NotifyPropertyChanged("DisplayedImage1Path");
            NotifyPropertyChanged("DisplayedImage2Path");
            NotifyPropertyChanged("DisplayedImage3Path");
            NotifyPropertyChanged("DisplayedImage4Path");
            NotifyPropertyChanged("DisplayedImage5Path");
            NotifyPropertyChanged("DisplayedImage6Path");
            NotifyPropertyChanged("DisplayedImage7Path");
            NotifyPropertyChanged("DisplayedImage8Path");
            NotifyPropertyChanged("DisplayedImage9Path");
            NotifyPropertyChanged("DisplayedImage10Path");
            NotifyPropertyChanged("DisplayedImage11Path");
            NotifyPropertyChanged("DisplayedImage12Path");
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
