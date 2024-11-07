using QuizConfigurator.Command;
using QuizConfigurator.Model;
using QuizConfigurator.Dialogs;
using System.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using QuizConfigurator.View;
using System.Windows;
using System.Windows.Input;
using System.Security.Policy;

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
        public DelegateCommand SwitchToPlayerViewCommand { get; }
        public DelegateCommand SwitchToConfigurationViewCommand { get; }
        public DelegateCommand OpenNewPackWindowCommand { get; }
        public DelegateCommand CreateNewPackCommand { get; }

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
            Configuration config = new Configuration();
            var loadedQuestionPacks = config.Load();

            CurrentView = "Configuration";

            //ViewModels
            ActivePack = new QuestionPackViewModel(loadedQuestionPacks[0]);
            PlayerViewModel = new PlayerViewModel(this);
            ConfigurationViewModel = new ConfigurationViewModel(this);

            //Packs
            Packs = new ObservableCollection<QuestionPackViewModel>();
            foreach (var questionpack in loadedQuestionPacks)
            {
                QuestionPackViewModel newPack = new QuestionPackViewModel(questionpack);
                Packs.Add(newPack);
            }

            //Commands
            ExitProgramCommand = new DelegateCommand(ExitProgram, CanExitProgram);
            OpenPackOptionsWindowCommand = new DelegateCommand(OpenOptions, CanOpenOptions);
            SwitchToConfigurationViewCommand = new DelegateCommand(SwitchToConfigurationView, CanSwitchToConfigurationView);
            SwitchToPlayerViewCommand = new DelegateCommand(SwitchToPlayerView, CanSwitchToPlayerView);
            OpenNewPackWindowCommand = new DelegateCommand(OpenCreateNewPack, CanOpenCreateNewPack);
            CreateNewPackCommand = new DelegateCommand(CreateNewPack, CanCreateNewPack);
        }
        public bool CanExitProgram(object? arg) => true;

        public void ExitProgram(object obj)
        {
            System.Windows.Application.Current.Shutdown();
        }
        public bool CanOpenOptions(object? arg) => true;
        public void OpenOptions(object obj)
        {
            PackOptionsDialog optionsWindow = new PackOptionsDialog();
            optionsWindow.ShowDialog();
        }
        public bool CanSwitchToConfigurationView(object? arg) => CurrentView == "Player";
        public void SwitchToConfigurationView(object obj)
        {
            CurrentView = "Configuration";
        }
        public bool CanSwitchToPlayerView(object? arg) => CurrentView == "Configuration";
        public void SwitchToPlayerView(object obj)
        {
            CurrentView = "Player";
            PlayerViewModel.StartQuestionGame();
        }
        public bool CanOpenCreateNewPack(object? arg) => true;
        public void OpenCreateNewPack(object obj)
        {
            CreateNewPackDialog createPackWindow = new CreateNewPackDialog();
            createPackWindow.ShowDialog();
        }
        public bool CanCreateNewPack(object? arg) => true;
        public void CreateNewPack(object obj)
        {
            
        }
    }
}
