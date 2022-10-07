using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using PianoTutorial.View;

namespace PianoTutorial.ViewModel
{
    public class TopToolbarViewModel
    {
        #region Members

        public event PropertyChangedEventHandler PropertyChanged;
        private ICommand m_clickCommand;
        private ICommand m_settingsButtonCommand;
        private bool m_closeExecute;
        private ICommand m_chordListButtonCommand;
        private ICommand m_songListButtonCommand;
        private bool m_chordListCanExecute;
        private bool m_songListCanExecute;
        private bool m_settingsCanExecute;
        private EventLoopScheduler m_eventLoopScheduler = new EventLoopScheduler();
        private SongListViewModel m_songListViewModel;
        private ChordListViewModel m_chordListViewModel;
        private SettingWindowViewModel m_settingsWindowViewModel;

        #endregion

        #region Constructor's
        public TopToolbarViewModel()
        {
            m_closeExecute = true;
            m_chordListCanExecute = true;
            m_songListCanExecute = true;
            m_settingsCanExecute = true;
            //m_chordListViewModel.IsVisible = false;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Close Button
        /// </summary>
        public ICommand Close
        {
            get
            {
                return m_clickCommand ?? (m_clickCommand = new CommandHandler(() => CloseApp(), m_closeExecute));
            }
        }

        public ICommand Settings
        {
            get
            {
                return m_settingsButtonCommand ?? (m_settingsButtonCommand = new CommandHandler(() => SettingsApp(), m_settingsCanExecute));
            }
        }

        public ICommand ChordListButton
        {
            get
            {
                return m_chordListButtonCommand ?? (m_chordListButtonCommand = new CommandHandler(() => chordListButton(), m_chordListCanExecute));
            }
        }

        public ICommand SongListButton
        {
            get
            {
                return m_songListButtonCommand ?? (m_songListButtonCommand = new CommandHandler(() => songListButton(), m_songListCanExecute));
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// CloseApp
        /// </summary>
        private void CloseApp()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void SettingsApp()
        {
            if (m_settingsWindowViewModel == null)
                m_settingsWindowViewModel = MainWindowViewModel.m_settingsWindowViewModel;
            if (MainWindowViewModel.m_settingsWindowViewModel.IsVisible == true)
            {
                MainWindowViewModel.m_settingsWindowViewModel.IsVisible = false;
            }
            else
            {
                MainWindowViewModel.m_settingsWindowViewModel.IsVisible = true;
            }
        }

        private void chordListButton()
        {
            if (m_chordListViewModel == null)
                m_chordListViewModel = MainWindowViewModel.m_chordListViewModel;
            if (m_chordListViewModel.IsVisible == true)
            {
                m_chordListViewModel.IsVisible = false;
            }
            else
            {
                m_chordListViewModel.IsVisible = true;
            }
        }

        private void songListButton()
        {
            if (m_songListViewModel == null)
                m_songListViewModel = MainWindowViewModel.m_songListViewModel;
            if (m_songListViewModel.IsVisible == true)
            {
                m_songListViewModel.IsVisible = false;
            }
            else
            {
                m_songListViewModel.IsVisible = true;
            }
        }

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        //private void NotifyPropertyChanged(String info)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(info));
        //    }
        //}
        #endregion
    }

}
