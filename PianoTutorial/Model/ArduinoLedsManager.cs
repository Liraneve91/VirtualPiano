using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoTutorial.Model
{
    public class ArduinoLedsManager
    {
        private const int START_KEY = 42;
        private const int END_KEY = 63;
        private SerialPort m_arduinoPort;
        private bool[] m_ledLists = new bool[20];
        Dictionary<int, char> m_arduinoKeysDictionary = new Dictionary<int, char>();
        private bool m_isSongPlay;
        private List<int> m_songNotesList = new List<int>();
        private bool nextNote = false;

        public bool IsSongPlay
        {
            get { return m_isSongPlay; }
            set { m_isSongPlay = value; }
        }

        public ArduinoLedsManager(SerialPort p_arduinoPort)
        {
            m_arduinoPort = p_arduinoPort;
            createDictionary();
            for(int i=0;i<20;i++)
            {
                m_ledLists[i] = false;
            }
        }

        public void TurnLedOn(int ledNum)
        {
            if ((ledNum > START_KEY && ledNum < END_KEY) && !m_ledLists[ledNum - START_KEY-1])
            {
                int temp = (ledNum - START_KEY-1);
                m_arduinoPort.Write(m_arduinoKeysDictionary[temp+1].ToString());
                m_ledLists[temp] = true;
            }
        }

        public void TurnLedOff(int ledNum)
        {
            if ( (ledNum > START_KEY && ledNum < END_KEY) && m_ledLists[ledNum - START_KEY-1])
            {
                int temp = (ledNum - START_KEY-1);
                m_arduinoPort.Write(m_arduinoKeysDictionary[temp+1].ToString());
                m_ledLists[temp] = false;
                nextNote = true;
            }
            if(m_isSongPlay && nextNote)
            {
                TurnLedOn(m_songNotesList[0]);
                m_songNotesList.RemoveAt(0);
                if (m_songNotesList.Count == 0)
                    m_isSongPlay = false;
            }
            nextNote = false;
        }

        private void createDictionary()
        {
            m_arduinoKeysDictionary.Add(1, 'a'); m_arduinoKeysDictionary.Add(2, 'b'); m_arduinoKeysDictionary.Add(3, 'c');
            m_arduinoKeysDictionary.Add(4, 'd'); m_arduinoKeysDictionary.Add(5, 'e'); m_arduinoKeysDictionary.Add(6, 'f');
            m_arduinoKeysDictionary.Add(7, 'g'); m_arduinoKeysDictionary.Add(8, 'h'); m_arduinoKeysDictionary.Add(9, 'i');
            m_arduinoKeysDictionary.Add(10, 'j'); m_arduinoKeysDictionary.Add(11, 'k'); m_arduinoKeysDictionary.Add(12, 'l');
            m_arduinoKeysDictionary.Add(13, 'm'); m_arduinoKeysDictionary.Add(14, 'n'); m_arduinoKeysDictionary.Add(15, 'o');
            m_arduinoKeysDictionary.Add(16, 'p'); m_arduinoKeysDictionary.Add(17, 'q'); m_arduinoKeysDictionary.Add(18, 'r');
            m_arduinoKeysDictionary.Add(19, 's'); m_arduinoKeysDictionary.Add(20, 't');

        }

        public void StartSong(string p_song)
        {
            switch(p_song)
            {
                case "האחת שלי":
                    if (m_songNotesList.Count != 0)
                    {
                        m_songNotesList.Clear();
                        AllLedsOff();
                    }
                    CreateFirstSong();
                    m_isSongPlay = true;
                    TurnLedOn(m_songNotesList[0]);
                    m_songNotesList.RemoveAt(0);
                    break;
                case "יום הולדת שמח":
                    if (m_songNotesList.Count != 0)
                    {
                        m_songNotesList.Clear();
                        AllLedsOff();
                    }
                    CreateSecondSong();
                    m_isSongPlay = true;
                    TurnLedOn(m_songNotesList[0]);
                    m_songNotesList.RemoveAt(0);
                    break;
            }
            /*
            C6 B5 C6 E6 D6 C6D6 E6 D6 C6 B5 A5 B5 C6 E6 E6 D6 C6 B5 A5 E6 G6 Eb6
            */
        }

        public void CreateFirstSong()
        {
            m_songNotesList.Add(50); m_songNotesList.Add(52); m_songNotesList.Add(54); m_songNotesList.Add(50);
            m_songNotesList.Add(47); m_songNotesList.Add(54); m_songNotesList.Add(57); m_songNotesList.Add(55);
            m_songNotesList.Add(57); m_songNotesList.Add(55); m_songNotesList.Add(54); m_songNotesList.Add(52);
            m_songNotesList.Add(54); m_songNotesList.Add(52); m_songNotesList.Add(50); m_songNotesList.Add(52);
            m_songNotesList.Add(49); m_songNotesList.Add(45); m_songNotesList.Add(52); m_songNotesList.Add(55);
            m_songNotesList.Add(54); m_songNotesList.Add(52); m_songNotesList.Add(50); m_songNotesList.Add(52);
            m_songNotesList.Add(50); m_songNotesList.Add(49); m_songNotesList.Add(50); m_songNotesList.Add(47);
            m_songNotesList.Add(43); m_songNotesList.Add(47); m_songNotesList.Add(50); m_songNotesList.Add(49);
            m_songNotesList.Add(47); m_songNotesList.Add(54); m_songNotesList.Add(52); m_songNotesList.Add(50);
            m_songNotesList.Add(49); m_songNotesList.Add(50); m_songNotesList.Add(49); m_songNotesList.Add(49);
            m_songNotesList.Add(49); m_songNotesList.Add(49); m_songNotesList.Add(50); m_songNotesList.Add(52);
        }

        public void CreateSecondSong()
        {
            m_songNotesList.Add(48); m_songNotesList.Add(48); m_songNotesList.Add(50); m_songNotesList.Add(48);
            m_songNotesList.Add(53); m_songNotesList.Add(52); m_songNotesList.Add(48); m_songNotesList.Add(48);
            m_songNotesList.Add(50); m_songNotesList.Add(48); m_songNotesList.Add(55); m_songNotesList.Add(53);
            m_songNotesList.Add(48); m_songNotesList.Add(48); m_songNotesList.Add(60); m_songNotesList.Add(57);
            m_songNotesList.Add(53); m_songNotesList.Add(52); m_songNotesList.Add(50); m_songNotesList.Add(58);
            m_songNotesList.Add(58); m_songNotesList.Add(57); m_songNotesList.Add(53); m_songNotesList.Add(55);
            m_songNotesList.Add(53); m_songNotesList.Add(48); m_songNotesList.Add(48); m_songNotesList.Add(50);
            m_songNotesList.Add(48); m_songNotesList.Add(53); m_songNotesList.Add(52); m_songNotesList.Add(48);
            m_songNotesList.Add(48); m_songNotesList.Add(50); m_songNotesList.Add(48); m_songNotesList.Add(55);
            m_songNotesList.Add(53); m_songNotesList.Add(48); m_songNotesList.Add(48); m_songNotesList.Add(60);
            m_songNotesList.Add(57); m_songNotesList.Add(53); m_songNotesList.Add(52); m_songNotesList.Add(50);
            m_songNotesList.Add(58); m_songNotesList.Add(58); m_songNotesList.Add(57); m_songNotesList.Add(53);
            m_songNotesList.Add(55); m_songNotesList.Add(53); 
        }

        public void AllLedsOff()
        {
            for(int i=1;i<21;i++)
            {
                if (m_ledLists[i - 1])
                {
                    m_arduinoPort.Write(m_arduinoKeysDictionary[i].ToString());
                    m_ledLists[i - 1] = false;
                }
            }
        }

        public void CloseArduinoPort()
        {
            m_arduinoPort.Close();
        }
    }
}
