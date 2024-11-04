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
            ActivePack = new QuestionPackViewModel(new QuestionPack("Default Question Pack"));
            PlayerViewModel = new PlayerViewModel(this);
            ConfigurationViewModel = new ConfigurationViewModel(this);
            
            ExitProgramCommand = new DelegateCommand(ExitProgram, CanExitProgram);
            OpenPackOptionsWindowCommand = new DelegateCommand(OpenOptions, CanOpenOptions);
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
    }
}
