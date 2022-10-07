using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PianoTutorial.ViewModel;
//using Sanford.Multimedia.Midi;

namespace PianoTutorial.View
{
    /// <summary>
    /// Interaction logic for PianoControlView.xaml
    /// </summary>
    public partial class PianoControlView : UserControl
    {
        public PianoControlView()
        {
            InitializeComponent();
            MainWindowViewModel.m_pianoControlViewMode = this.DataContext as PianoControlViewModel;
        }     
    }
}
