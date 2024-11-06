using QuizConfigurator.Command;
using QuizConfigurator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using QuizConfigurator.View;
using System.Windows;

namespace QuizConfigurator.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<QuestionPackViewModel> Packs { get; set; }
        private QuestionPackViewModel? _activePack;
        public PlayerViewModel PlayerViewModel { get; }
        public ConfigurationViewModel ConfigurationViewModel { get; }

        public DelegateCommand ExitProgramCommand { get; }
        public DelegateCommand OpenPackOptionsWindowCommand { get; }
        public DelegateCommand SwitchViewCommand { get; }

        private string _currentView;
        public string CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                RaisePropertyChanged();
            }
        }
        public QuestionPackViewModel? ActivePack
        {
            get => _activePack;
            set
            {
                _activePack = value;
                RaisePropertyChanged();
            }
        }
        public MainWindowViewModel()
        {
            CurrentView = "Configuration";

            ActivePack = new QuestionPackViewModel(new QuestionPack("Default Question Pack"));
            PlayerViewModel = new PlayerViewModel(this);
            ConfigurationViewModel = new ConfigurationViewModel(this);
            
            ExitProgramCommand = new DelegateCommand(ExitProgram, CanExitProgram);
            OpenPackOptionsWindowCommand = new DelegateCommand(OpenOptions, CanOpenOptions);
            SwitchViewCommand = new DelegateCommand(SwitchView, CanSwitchView);
        }
        public bool CanExitProgram(object? arg) => true;

        public void ExitProgram(object obj)
        {
            System.Windows.Application.Current.Shutdown();
        }
        public bool CanOpenOptions(object? arg) => true;
        public void OpenOptions(object obj)
        {
            PackOptionsView options = new PackOptionsView();
            options.ShowDialog();
        }
        public bool CanSwitchView(object? arg) => true;
        public void SwitchView(object obj)
        {
            CurrentView = CurrentView == "Configuration" ? "Player" : "Configuration";
        }
    }
}
