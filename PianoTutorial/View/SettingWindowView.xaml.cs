using PianoTutorial.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PianoTutorial.View
{
    /// <summary>
    /// Interaction logic for SettingWindowView.xaml
    /// </summary>
    public partial class SettingWindowView : UserControl
    {
        public SettingWindowView()
        {
            InitializeComponent();
            this.DataContext = new SettingWindowViewModel();
            MainWindowViewModel.m_settingsWindowViewModel = this.DataContext as SettingWindowViewModel;
        }
    }
}
