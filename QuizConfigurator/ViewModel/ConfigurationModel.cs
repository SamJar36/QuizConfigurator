using QuizConfigurator.Command;
using QuizConfigurator.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace QuizConfigurator.ViewModel
{
    internal class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        private Question _selectedQuestion;

        public DelegateCommand AddQuestionCommand { get; }

        public Question SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged();
            }
        }
        public string? ActivePackName => mainWindowViewModel?.ActivePack?.Name;

        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            AddQuestionCommand = new DelegateCommand(AddQuestion, CanAddQuestion);
        }
        public bool CanAddQuestion(object? arg) => true;
        public void AddQuestion(object obj)
        {
            mainWindowViewModel.ActivePack.Questions.Add(new Question("New Question", "", "", "", ""));
            //RaisePropertyChanged();
        }
    }
}
