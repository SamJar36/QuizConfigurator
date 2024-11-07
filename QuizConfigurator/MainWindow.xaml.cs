﻿using QuizConfigurator.ViewModel;
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
    }
}

// incorrect answers list instead
// pack options border shadows
// move pack options window
// Move pack options method to configurationviewmode
// dialog not center screen but center on window

//CreateNewPacks in Dialogs, not view
//fix play/edit bug
//time limit
//when last question go to score screen
//score screen (play again, exit, score)
//exiting or switching packs saves the question pack to json
//if button is blank in SelectButton, dont crash game
// new question pack
//select question pack
// delete question pack
//fullscreen
// some sort of pause inbetween clicking on buttons and next room