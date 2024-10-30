using QuizConfigurator.Command;
using QuizConfigurator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizConfigurator.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<QuestionPackViewModel> Packs { get; set; }
        private QuestionPackViewModel? _activePack;
        public PlayerViewModel PlayerViewModel { get; }
        public ConfigurationViewModel ConfigurationViewModel { get; }

        public DelegateCommand ExitProgramCommand { get; }

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
            PlayerViewModel = new PlayerViewModel(this);
            ConfigurationViewModel = new ConfigurationViewModel(this);
            ActivePack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));

            ExitProgramCommand = new DelegateCommand(ExitProgram, CanExitProgram);
        }
        public bool CanExitProgram(object? arg) => true;

        public void ExitProgram(object obj)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
