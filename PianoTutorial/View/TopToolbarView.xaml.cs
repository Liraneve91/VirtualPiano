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
using MvvmTools.Annotations;
using PianoTutorial.ViewModel;

namespace PianoTutorial.View
{
    /// <summary>
    /// Interaction logic for TopToolbarView.xaml
    /// </summary>
    public partial class TopToolbarView : UserControl
    {
        public TopToolbarView()
        {
            InitializeComponent();
            MainWindowViewModel.m_topToolBarViewModel = this.DataContext as TopToolbarViewModel;
        }
    }
}
