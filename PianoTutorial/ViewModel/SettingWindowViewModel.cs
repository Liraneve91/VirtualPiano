using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PianoTutorial.ViewModel
{
    public class SettingWindowViewModel : INotifyPropertyChanged
    {
        private bool m_isVisible;
        public event PropertyChangedEventHandler PropertyChanged;
        private ICommand m_closeCommand;
        private bool m_closeExecute;

        public SettingWindowViewModel()
        {
            m_isVisible = false;
            m_closeExecute = true;
        }

        /// <summary>
        /// Close Button
        /// </summary>
        public ICommand Close
        {
            get
            {
                return m_closeCommand ?? (m_closeCommand = new CommandHandler(() => CloseApp(), m_closeExecute));
            }
        }

        public bool IsVisible
        {
            get { return m_isVisible; }
            set
            {
                m_isVisible = value;
                if (m_isVisible)
                {
                    MainWindowViewModel.m_chordListViewModel.IsVisible = false;
                    MainWindowViewModel.m_songListViewModel.IsVisible = false;
                }
                NotifyPropertyChanged("IsVisible");
            }
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        /// <summary>
        /// CloseApp
        /// </summary>
        private void CloseApp()
        {
            //System.Windows.Application.Current.Shutdown();
            //MainWindowViewModel.m_arduinoLedsManager.AllLedsOff();
            //MainWindowViewModel.m_arduinoLedsManager.CloseArduinoPort();
            Environment.Exit(0);
        }
    }
}
