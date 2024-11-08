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
using System.Windows.Markup;

namespace QuizConfigurator.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public QuestionPack NewQuestionPack { get; set; }
        public Configuration Config { get; set; }
        public PlayerViewModel PlayerViewModel { get; }
        public ConfigurationViewModel ConfigurationViewModel { get; }

        public DelegateCommand ExitProgramCommand { get; }
        public DelegateCommand OpenPackOptionsWindowCommand { get; }
        public DelegateCommand SwitchToPlayerViewCommand { get; }
        public DelegateCommand SwitchToConfigurationViewCommand { get; }
        public DelegateCommand OpenNewPackWindowCommand { get; }
        public DelegateCommand CreateNewPackCommand { get; }
        public DelegateCommand SwitchActivePackCommand { get; }
        public DelegateCommand DeleteActivePackCommand { get; }
        private ObservableCollection<QuestionPackViewModel> _packs;
        public ObservableCollection<QuestionPackViewModel> Packs
        {
            get => _packs;
            set
            {
                _packs = value;
                RaisePropertyChanged();
            }
        }
        private Question _selectedQuestion;
        public Question SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged();
            }
        }

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
        private QuestionPackViewModel? _activePack;
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

            //Load from json file
            Config = new Configuration();
            var loadedQuestionPacks = Config.Load();

            //ViewModels
            Packs = new ObservableCollection<QuestionPackViewModel>();
            if (loadedQuestionPacks != null && loadedQuestionPacks.Count > 0)
            {
                foreach (var questionpack in loadedQuestionPacks)
                {
                    QuestionPackViewModel newPack = new QuestionPackViewModel(questionpack);
                    Packs.Add(newPack);
                }
            }
            ActivePack = Packs.FirstOrDefault();
            PlayerViewModel = new PlayerViewModel(this);
            ConfigurationViewModel = new ConfigurationViewModel(this);

            //Commands
            ExitProgramCommand = new DelegateCommand(ExitProgram, CanExitProgram);
            OpenPackOptionsWindowCommand = new DelegateCommand(OpenOptions, CanOpenOptions);
            SwitchToConfigurationViewCommand = new DelegateCommand(SwitchToConfigurationView, CanSwitchToConfigurationView);
            SwitchToPlayerViewCommand = new DelegateCommand(SwitchToPlayerView, CanSwitchToPlayerView);
            OpenNewPackWindowCommand = new DelegateCommand(OpenCreateNewPackWindow, CanOpenCreateNewPackWindow);
            CreateNewPackCommand = new DelegateCommand(CreateNewPack, CanCreateNewPack);
            SwitchActivePackCommand = new DelegateCommand(SwitchActivePack, CanSwitchActivePack);
            DeleteActivePackCommand = new DelegateCommand(DeleteActivePack, CanDeleteActivePack);
        }
        public bool CanExitProgram(object? arg) => true;

        public void ExitProgram(object obj)
        {
            Config.Save(Packs);
            UpdateAllCommands();
            System.Windows.Application.Current.Shutdown();
        }
        public bool CanOpenOptions(object? arg) => Packs.Count > 0 && CurrentView != "Player";
        public void OpenOptions(object obj)
        {
            PackOptionsDialog optionsWindow = new PackOptionsDialog();
            optionsWindow.ShowDialog();
            UpdateAllCommands();
        }
        public bool CanSwitchToConfigurationView(object? arg) => CurrentView == "Player";
        public void SwitchToConfigurationView(object obj)
        {
            CurrentView = "Configuration";
            UpdateAllCommands();
        }
        public bool CanSwitchToPlayerView(object? arg) => CurrentView == "Configuration";
        public void SwitchToPlayerView(object obj)
        {
            CurrentView = "Player";
            PlayerViewModel.StartQuestionGame();
            UpdateAllCommands();
        }
        public bool CanOpenCreateNewPackWindow(object? arg) => CurrentView != "Player";
        public void OpenCreateNewPackWindow(object obj)
        {
            NewQuestionPack = new QuestionPack("<Pack Name>");
            CreateNewPackDialog createPackWindow = new CreateNewPackDialog();
            createPackWindow.ShowDialog();
            UpdateAllCommands();
        }
        public bool CanCreateNewPack(object? arg) => CurrentView != "Player";
        public void CreateNewPack(object obj)
        {
            ActivePack = new QuestionPackViewModel(NewQuestionPack);
            Packs.Add(ActivePack);
            UpdateAllCommands();
        }
        public bool CanSwitchActivePack(object? arg) => Packs.Count > 0 && CurrentView != "Player";
        public void SwitchActivePack(object obj)
        {
            if (obj is QuestionPackViewModel selectedPack)
            {
                ActivePack = selectedPack;
            }
            UpdateAllCommands();
        }
        public bool CanDeleteActivePack(object? arg) => Packs.Count > 0 && CurrentView != "Player";
        public void DeleteActivePack(object obj)
        {
            var result = MessageBox.Show("Are you sure you want to delete this question pack?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Packs.Remove(ActivePack);
                ActivePack = Packs.FirstOrDefault();
            }
            UpdateAllCommands();
        }
        public void UpdateAllCommands()
        {
            OpenNewPackWindowCommand.RaiseCanExecuteChanged();
            CreateNewPackCommand.RaiseCanExecuteChanged();
            SwitchToPlayerViewCommand.RaiseCanExecuteChanged();
            SwitchToConfigurationViewCommand.RaiseCanExecuteChanged();
            SwitchActivePackCommand.RaiseCanExecuteChanged();
            DeleteActivePackCommand.RaiseCanExecuteChanged();
            OpenPackOptionsWindowCommand.RaiseCanExecuteChanged();
            ConfigurationViewModel.AddQuestionCommand.RaiseCanExecuteChanged();
            ConfigurationViewModel.RemoveQuestionCommand.RaiseCanExecuteChanged();
            PlayerViewModel.SelectAnswerCommand.RaiseCanExecuteChanged();
            PlayerViewModel.RestartQuizCommand.RaiseCanExecuteChanged();
        }
    }
}
