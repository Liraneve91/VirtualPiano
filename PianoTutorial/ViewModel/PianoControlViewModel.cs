using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using CSCore.Streams;
using CSCore.Streams.Effects;
using Button = System.Windows.Controls.Button;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using System.Threading;
using System.IO.Ports;
using System.IO;

namespace PianoTutorial.ViewModel
{
    public class PianoControlViewModel : INotifyPropertyChanged
    {
        #region Fields

        public event PropertyChangedEventHandler PropertyChanged;
        public List<Button> m_myKeys = new List<Button>();
        private List<int> whiteKeys = new List<int>() { 0, 2, 4, 5, 7, 9, 11, 12, 14, 16, 17, 19, 21, 23, 24, 26, 28, 29, 31, 33, 35, 36, 38, 40, 41, 43, 45, 47, 48, 50, 52, 53, 55, 57, 59, 60 };
        private List<int> blackKeys = new List<int>();
        private List<int> space = new List<int>() { 2, 5, 7, 10, 12, 15, 17, 20, 22 };
        private List<int> chordC = new List<int>() { 7, 9, 11 };
        private List<int> chordCm = new List<int>() { 7, 42, 11 };
        private List<int> chordC7 = new List<int>() { 7, 9, 11, 45 };
        private List<int> chordC6 = new List<int>() { 7, 9, 11, 12 };
        private List<int> chordCsharp = new List<int>() { 41, 10, 44 };
        private List<int> chordCsharpm = new List<int>() { 41, 9, 44 };
        Dictionary<int, int> m_pianoKeysDictionary = new Dictionary<int, int>();
        Dictionary<int, char> m_arduinoKeysDictionary = new Dictionary<int, char>();
        //private SerialPort m_arduinoPort;
        private ICommand m_testClickCommand;
        private ICommand m_loadFileCommand;
        private ICommand m_stopSongCommand;
        private ICommand m_playSongCommand;
        private ICommand m_pauseSongCommand;
        private ICommand m_volumeUpCommand;
        private ICommand m_volumeDownCommand;
        private bool m_loadFileCanExecute=true;
        private bool m_stopSongCanExecute=true;
        private bool m_playSongCanExecute=true;
        private bool m_pauseSongCanExecute = true;
        private bool m_volumeUpCanExecute = true;
        private bool m_volumeDownCanExecute = true;
        private bool m_testCanExecute;
        private int m_sliderValue=0;
        private static List<ISoundOut> pianoWavSounds = new List<ISoundOut>();
        private const double MaxDB = 20;
        private Equalizer m_equalizer;
        private ISoundOut m_soundOut;
        private System.IO.StreamWriter file;


        #endregion

        #region Constructor

        public PianoControlViewModel()
        {
            init();
            creteKeys();
            createDictionary();
    }

        #endregion

        #region Properties

        public ICommand LoadFile
        {
            get
            {
                return m_loadFileCommand ?? (m_loadFileCommand = new CommandHandler(() => loadFileDialog(), m_loadFileCanExecute));
            }
        }

        public ICommand StopSong
        {
            get
            {
                return m_stopSongCommand ?? (m_stopSongCommand = new CommandHandler(() => Stop(), m_stopSongCanExecute));
            }
        }

        public ICommand PlaySong
        {
            get
            {
                return m_playSongCommand ?? (m_playSongCommand = new CommandHandler(() => Play(), m_playSongCanExecute));
            }
        }

        public ICommand PauseSong
        {
            get
            {
                return m_pauseSongCommand ?? (m_pauseSongCommand = new CommandHandler(() => Pause(), m_pauseSongCanExecute));
            }
        }

        public ICommand VolumeUp
        {
            get
            {
                return m_volumeUpCommand ?? (m_volumeUpCommand = new CommandHandler(() => volumeUp(), m_volumeUpCanExecute));
            }
        }

        public ICommand VolumeDown
        {
            get
            {
                return m_volumeDownCommand ?? (m_volumeDownCommand = new CommandHandler(() => volumeDown(), m_volumeDownCanExecute));
            }
        }

        private void volumeUp()
        {
            if(m_soundOut.Volume>(float)0.95)
            {
                m_soundOut.Volume = (float)1;
            }
            else if(m_soundOut.Volume<1)
                m_soundOut.Volume += (float)0.05;
        }

        private void volumeDown()
        {
            if(m_soundOut.Volume<(float)0.05)
            {
                m_soundOut.Volume = (float)0;
            }
            else if (m_soundOut.Volume > 0)
                m_soundOut.Volume -= (float)0.05;
        }

        public ICommand TestButton
        {
            get
            {
                return m_testClickCommand ?? (m_testClickCommand = new CommandHandler(() => testApp(), m_testCanExecute));
            }
        }

        /*public SerialPort ArduinoPort
        {
            get { return m_arduinoPort; }
        }*/

        #region Sliders Property

        public int Slider1
        {
            get { return m_sliderValue; }
            set
            {
                m_sliderValue = value;
                equalizer(0, m_sliderValue);
            }
        }

        public int Slider2
        {
            get { return m_sliderValue; }
            set
            {
                m_sliderValue = value;
                equalizer(1, m_sliderValue);
            }
        }

        public int Slider3
        {
            get { return m_sliderValue; }
            set
            {
                m_sliderValue = value;
                equalizer(2, m_sliderValue);
            }
        }

        public int Slider4
        {
            get { return m_sliderValue; }
            set
            {
                m_sliderValue = value;
                equalizer(3, m_sliderValue);
            }
        }

        public int Slider5
        {
            get { return m_sliderValue; }
            set
            {
                m_sliderValue = value;
                equalizer(4, m_sliderValue);
            }
        }

        public int Slider6
        {
            get { return m_sliderValue; }
            set
            {
                m_sliderValue = value;
                equalizer(5, m_sliderValue);
            }
        }

        public int Slider7
        {
            get { return m_sliderValue; }
            set
            {
                m_sliderValue = value;
                equalizer(6, m_sliderValue);
            }
        }

        public int Slider8
        {
            get { return m_sliderValue; }
            set
            {
                m_sliderValue = value;
                equalizer(7, m_sliderValue);
            }
        }

        public int Slider9
        {
            get { return m_sliderValue; }
            set
            {
                m_sliderValue = value;
                equalizer(8, m_sliderValue);
            }
        }

        public int Slider10
        {
            get { return m_sliderValue; }
            set
            {
                m_sliderValue = value;
                equalizer(9, m_sliderValue);
            }
        }
        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// initialize the arduino 
        /// </summary>
        private void init()
        {
            /*m_arduinoPort = new SerialPort();
            m_arduinoPort.BaudRate = 9600;
            m_arduinoPort.PortName = "COM1";
            m_arduinoPort.Open();*/
        }

        private void createDictionary()
        {
            m_pianoKeysDictionary.Add(24, 1); m_pianoKeysDictionary.Add(26, 2); m_pianoKeysDictionary.Add(28, 3); m_pianoKeysDictionary.Add(29, 4); m_pianoKeysDictionary.Add(31, 5);
            m_pianoKeysDictionary.Add(33, 6); m_pianoKeysDictionary.Add(35, 7); m_pianoKeysDictionary.Add(36, 8); m_pianoKeysDictionary.Add(38, 9); m_pianoKeysDictionary.Add(40, 10);
            m_pianoKeysDictionary.Add(41, 11); m_pianoKeysDictionary.Add(43, 12); m_pianoKeysDictionary.Add(45, 13); m_pianoKeysDictionary.Add(47, 14); m_pianoKeysDictionary.Add(48, 15);
            m_pianoKeysDictionary.Add(50, 16); m_pianoKeysDictionary.Add(52, 17); m_pianoKeysDictionary.Add(53, 18); m_pianoKeysDictionary.Add(55, 19); m_pianoKeysDictionary.Add(57, 20);
            m_pianoKeysDictionary.Add(59, 21); m_pianoKeysDictionary.Add(60, 22); m_pianoKeysDictionary.Add(62, 23); m_pianoKeysDictionary.Add(64, 24); m_pianoKeysDictionary.Add(65, 25);
            m_pianoKeysDictionary.Add(67, 26); m_pianoKeysDictionary.Add(69, 27); m_pianoKeysDictionary.Add(71, 28); m_pianoKeysDictionary.Add(72, 29);

            m_pianoKeysDictionary.Add(25, 37); m_pianoKeysDictionary.Add(27, 38); m_pianoKeysDictionary.Add(30, 39); m_pianoKeysDictionary.Add(32, 40); m_pianoKeysDictionary.Add(34, 41);
            m_pianoKeysDictionary.Add(37, 42); m_pianoKeysDictionary.Add(39, 43); m_pianoKeysDictionary.Add(42, 44); m_pianoKeysDictionary.Add(44, 45); m_pianoKeysDictionary.Add(46, 46);
            m_pianoKeysDictionary.Add(49, 47); m_pianoKeysDictionary.Add(51, 48); m_pianoKeysDictionary.Add(54, 49); m_pianoKeysDictionary.Add(56, 50); m_pianoKeysDictionary.Add(58, 51);
            m_pianoKeysDictionary.Add(61, 52); m_pianoKeysDictionary.Add(63, 53); m_pianoKeysDictionary.Add(66, 54); m_pianoKeysDictionary.Add(68, 55); m_pianoKeysDictionary.Add(70, 56);

            m_arduinoKeysDictionary.Add(1, 'a'); m_arduinoKeysDictionary.Add(2, 'b'); m_arduinoKeysDictionary.Add(3, 'c');
            m_arduinoKeysDictionary.Add(4, 'd'); m_arduinoKeysDictionary.Add(5, 'e'); m_arduinoKeysDictionary.Add(6, 'f');
            m_arduinoKeysDictionary.Add(7, 'g'); m_arduinoKeysDictionary.Add(8, 'h'); m_arduinoKeysDictionary.Add(9, 'i');
            m_arduinoKeysDictionary.Add(10, 'j'); m_arduinoKeysDictionary.Add(11, 'k'); m_arduinoKeysDictionary.Add(12, 'l');
            m_arduinoKeysDictionary.Add(13, 'm'); m_arduinoKeysDictionary.Add(14, 'n'); m_arduinoKeysDictionary.Add(15, 'o');
            m_arduinoKeysDictionary.Add(16, 'p'); m_arduinoKeysDictionary.Add(17, 'q'); m_arduinoKeysDictionary.Add(18, 'r');
            m_arduinoKeysDictionary.Add(19, 's'); m_arduinoKeysDictionary.Add(20, 't');

        }

        private void loadFileDialog()
        {
            var ofn = new OpenFileDialog();
            ofn.Filter = CodecFactory.SupportedFilesFilterEn;
            if (ofn.ShowDialog() == DialogResult.OK)
            {
                Stop();

                if (WasapiOut.IsSupportedOnCurrentPlatform)
                    m_soundOut = new WasapiOut();
                else
                    m_soundOut = new DirectSoundOut();

                var source = CodecFactory.Instance.GetCodec(ofn.FileName)
                    .Loop()
                    .ChangeSampleRate(32000)
                    .ToSampleSource()
                    .AppendSource(Equalizer.Create10BandEqualizer, out m_equalizer)
                    .ToWaveSource();

                m_soundOut.Initialize(source);
                m_soundOut.Volume = (float) 0.5;
                m_soundOut.Play();
            }
        }

        private void Stop()
        {
            if (m_soundOut != null)
            {
                m_soundOut.Stop();
                m_soundOut.Dispose();
                m_equalizer.Dispose();
                m_soundOut = null;
            }
        }

        private void Play()
        {
            if (m_soundOut != null)
            {
                m_soundOut.Resume();
            }
        }

        private void Pause()
        {
            if (m_soundOut != null)
            {
                m_soundOut.Pause();
            }
        }

        private void equalizer(int p_sliderNum, double p_sliderValue)
        {
            if (m_equalizer != null)
            {
                double perc = (p_sliderValue / (double)100);
                var value = (float)(perc * MaxDB);
                //the tag of the trackbar contains the index of the filter
                EqualizerFilter filter = m_equalizer.SampleFilters[p_sliderNum];
                filter.AverageGainDB = value;
            }
        }

        private void testApp()
        {
            m_myKeys[2].Background = Brushes.Blue;
            UpdateKeys();
        }

        private void creteKeys()
        {
            double n = 0;
            Thickness margin = new Thickness(-9, 0, 0, 0);
            for (int i = 0; i < 36; i++)
            {
                Button border = new Button();
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 1, 1, 1);
                border.Background = Brushes.White;
                border.Margin = margin;
                border.Width = 41;
                border.Height = 290;
                m_myKeys.Add(border);
            }

            int y = 1497;
            for (int i = 0; i < 25; i++)
            {
                Button border = new Button();
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 1, 1, 1);
                border.Background = Brushes.Black;
                border.HorizontalAlignment = HorizontalAlignment.Left;
                margin.Left = -y;
                margin.Top = -90;
                border.Margin = margin;
                border.Width = 35;
                border.Height = 200;
                m_myKeys.Add(border);
                if (space.Contains(i + 1))
                {
                    y = y - 71;
                }
                else
                {
                    y = y - 34;
                }
            }

            MyKeys = m_myKeys;
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

        #region Public Methods

        public void ShowChord(string p_chord)
        {
            switch(p_chord)
            {
                case "A":
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(45);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(49);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(52);
                    //PressKey(45); PressKey(49); PressKey(52);
                    break;
                case "Ab":
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(44);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(48);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(51);
                    //PressKey(44); PressKey(48); PressKey(51);
                    break;
                case "B":
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(47);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(51);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(54);
                    //PressKey(47); PressKey(51); PressKey(54);
                    break;
                case "Bb":
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(46);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(50);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(53);
                    //PressKey(46); PressKey(50); PressKey(53);
                    break;
                case "C":
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(48);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(52);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(55);
                    //PressKey(48); PressKey(52); PressKey(55);
                    break;
                case "C#":
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(49);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(53);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(56);
                    //PressKey(49); PressKey(53); PressKey(56);
                    break;
                case "D":
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(50);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(54);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(57);
                    //PressKey(50); PressKey(54); PressKey(57);
                    break;
                case "E":
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(52);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(56);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(59);
                    //PressKey(52); PressKey(56); PressKey(59);
                    break;
                case "Eb":
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(51);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(55);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(58);
                    //PressKey(51); PressKey(55); PressKey(58);
                    break;
                case "F":
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(53);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(57);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(60);
                    //PressKey(53); PressKey(57); PressKey(60);
                    break;
                case "F#":
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(54);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(58);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(61);
                    //PressKey(54); PressKey(58); PressKey(61);
                    break;
                case "G":
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(55);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(59);
                    //MainWindowViewModel.m_arduinoLedsManager.TurnLedOn(62);
                    //PressKey(55); PressKey(59); PressKey(62);
                    break;
            }
            
        }

        public void HideChordC()
        {
            RealseKey(39);
            RealseKey(43);
            RealseKey(46);
            RealseKey(51);
        }

        public void PressKey(int key)
        {

            Console.WriteLine(key.ToString() + " ");

            if (key >= 43 && key <= 62)
            {
                //MainWindowViewModel.m_arduinoLedsManager.TurnLedOff(key);
                MainWindowViewModel.m_notesViewModel.DrawNote(key);
            }

            try
            {
                m_myKeys[m_pianoKeysDictionary[key] - 1].Background = Brushes.Blue;
                UpdateKeys();
                //int temp = (key - 23);
                //if (key >= 24 && key <= 43)
                //    m_arduinoPort.Write(m_arduinoKeysDictionary[temp].ToString());
                //m_arduinoPort.Write(temp.ToString());

            }
            catch (Exception e)
            {
                // TODO: print to log
            }
        }

        public void TurnOnLed(int key)
        {

        }

        public void TurnOffLed(int key)
        {

        }

        public void RealseKey(int key)
        {
            try {
                if (m_pianoKeysDictionary[key] <= 29)
                    m_myKeys[m_pianoKeysDictionary[key] - 1].Background = Brushes.White;
                else
                {
                    m_myKeys[m_pianoKeysDictionary[key] - 1].Background = Brushes.Black;
                }
                UpdateKeys();
            }
            catch(Exception e)
            {
                // TODO: print to log
            }
        }

        public List<Button> MyKeys
        {
            get { return m_myKeys; }
            set
            {
                m_myKeys = value;
                NotifyPropertyChanged("MyKeys");
            }

        }

        public void UpdateKeys()
        {
            NotifyPropertyChanged("MyKeys");
        }
        #endregion
    }
}
