using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Sanford.Multimedia;
using Sanford.Multimedia.Midi;
using System.Windows.Input;
using Sanford.Multimedia.Midi.UI;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using PianoTutorial.Model;

namespace PianoTutorial.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Fields

        private const int SysExBufferSize = 128;
        public event PropertyChangedEventHandler PropertyChanged;
        private PianoControl m_pianoControl;
        private OutputDevice outDevice;
        private int outDeviceID = 0;
        private Sanford.Multimedia.Midi.InputDevice m_inDevice = null;
        private SynchronizationContext m_context;
        public static PianoControlViewModel m_pianoControlViewMode;
        public static TopToolbarViewModel m_topToolBarViewModel;
        public static ChordListViewModel m_chordListViewModel;
        public static SongListViewModel m_songListViewModel;
        public static SettingWindowViewModel m_settingsWindowViewModel;
        //public static ArduinoLedsManager m_arduinoLedsManager;
        public static NotesViewModel m_notesViewModel;
        private readonly EventLoopScheduler m_eventLoopScheduler = new EventLoopScheduler();

        #endregion

        #region Constructor's

        public MainWindowViewModel()
        {
            m_pianoControl = new PianoControl();
            init();

            if (OutputDevice.DeviceCount == 0)
            {
                MessageBox.Show("No MIDI output devices available.", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                try
                {
                    outDevice = new OutputDevice(outDeviceID);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

            try
            {
                //m_inDevice.StartRecording();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            /*m_arduinoLedsManager = new ArduinoLedsManager(m_pianoControlViewMode.ArduinoPort);*/
            m_chordListViewModel.IsVisible = false;
            m_songListViewModel.IsVisible = false;
            m_settingsWindowViewModel.IsVisible = false;

            m_chordListViewModel.ChordSelected.ObserveOn(m_eventLoopScheduler).Subscribe(onChordSelected);
        }
        #endregion

        #region Methods

        private void onChordSelected(string chord)
        {
            switch(chord)
            {
                case "C": //m_pianoControlViewMode.ShowChordC();
                    break;  
            }
        }

        /// <summary>
        /// initialize piano input device
        /// </summary>
        private void init()
        {
            if (Sanford.Multimedia.Midi.InputDevice.DeviceCount == 0)
            {
                //MessageBox.Show("No MIDI input devices available.", "Error!",
                //    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //Exception exception = new Exception("No MIDI input devices available");
                //throw exception;
            }
            else
            {
                try
                {
                    m_context = SynchronizationContext.Current;

                    m_inDevice = new Sanford.Multimedia.Midi.InputDevice(0);
                    m_inDevice.ChannelMessageReceived += HandleChannelMessageReceived;
                    m_inDevice.SysCommonMessageReceived += HandleSysCommonMessageReceived;
                    m_inDevice.SysExMessageReceived += HandleSysExMessageReceived;
                    m_inDevice.SysRealtimeMessageReceived += HandleSysRealtimeMessageReceived;
                    m_inDevice.Error += new EventHandler<ErrorEventArgs>(inDevice_Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// inDevice_Error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inDevice_Error(object sender, ErrorEventArgs e)
        {
            MessageBox.Show(e.Error.Message, "Error!",
                   MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        /// <summary>
        /// HandleChannelMessageReceived - Read key from piano usb midi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleChannelMessageReceived(object sender, ChannelMessageEventArgs e)
        {
            if(e.Message.Command.ToString() == "NoteOn" && e.Message.Data2.ToString() != "0")
            {
                outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, e.Message.Data1, 127)); //e.Message.Data2
                m_pianoControlViewMode.PressKey(e.Message.Data1);
            }
            if(e.Message.Command.ToString() == "NoteOn" && e.Message.Data2.ToString() == "0")
            {
                m_pianoControlViewMode.RealseKey(e.Message.Data1);
                outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, e.Message.Data1, 0));
            }
        }

        /// <summary>
        /// HandleSysExMessageReceived
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleSysExMessageReceived(object sender, SysExMessageEventArgs e)
        {
            //context.Post(delegate (object dummy)
            //{
                string result = "\n\n"; ;

                foreach (byte b in e.Message)
                {
                    result += string.Format("{0:X2} ", b);
                }

            MessageBox.Show(result, "test2");

            //sysExRichTextBox.Text += result;
            //}, null);
        }

        /// <summary>
        /// HandleSysCommonMessageReceived
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleSysCommonMessageReceived(object sender, SysCommonMessageEventArgs e)
        {
            //context.Post(delegate (object dummy)
            //{
                //sysCommonListBox.Items.Add(
                    //e.Message.SysCommonType.ToString() + '\t' + '\t' +
                    //e.Message.Data1.ToString() + '\t' +
                    //e.Message.Data2.ToString());

            MessageBox.Show(e.Message.SysCommonType.ToString() + " , " + e.Message.Data1.ToString() + " , " + e.Message.Data2.ToString(), "test3");

            //sysCommonListBox.SelectedIndex = sysCommonListBox.Items.Count - 1;
            //}, null);
        }

        /// <summary>
        /// HandleSysRealtimeMessageReceived
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleSysRealtimeMessageReceived(object sender, SysRealtimeMessageEventArgs e)
        {
            //context.Post(delegate (object dummy)
            //{
            MessageBox.Show(e.Message.SysRealtimeType.ToString(), "test4");
                //sysRealtimeListBox.Items.Add(
                //    e.Message.SysRealtimeType.ToString());

                //sysRealtimeListBox.SelectedIndex = sysRealtimeListBox.Items.Count - 1;
            //}, null);
        }

        /// <summary>
        ///  NotifyPropertyChanged implementation
        /// </summary>
        /// <param name="info"></param>
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

    }

    /// <summary>
    /// class CommandHandler
    /// </summary>
    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }

        public void CloseApplication()
        {
          //  m_chordListViewModel.Dispose();
            //m_songListViewModel.Dispose();
            //m_settingsWindowViewModel.Dispose();

            //m_pianoControlViewMode.Dispose();
        }
    }



}
