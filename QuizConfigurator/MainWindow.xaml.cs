using QuizConfigurator.ViewModel;
using QuizConfigurator.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace QuizConfigurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.UpdateAllCommands();
                viewModel.Config.Save(viewModel.Packs);
            }
        }
    }
}

// incorrect answers list instead
// pack options border shadows
// move pack options window
// Move pack options method to configurationviewmode
// dialog not center screen but center on window

//time limit
//when last question go to score screen
//score screen (play again, exit, score)
// some sort of pause inbetween clicking on buttons and next room