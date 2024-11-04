using QuizConfigurator.Command;
using QuizConfigurator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Linq;

namespace QuizConfigurator.ViewModel
{
    internal class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        private ObservableCollection<Question> TemporaryQuestionCollection { get; set; }
        private int CurrentQuestionNumber { get; set; }
        public int CorrectGuesses { get; set; }
        private string _questionText;
        private string _buttonText1;
        private string _buttonText2;
        private string _buttonText3;
        private string _buttonText4;
        public string QuestionText
        {
            get => _questionText;
            set
            {
                _questionText = value;
                RaisePropertyChanged();
            }
        }
        public string ButtonText1
        {
            get => _buttonText1;
            set
            {
                _buttonText1 = value;
                RaisePropertyChanged();
            }
        }
        public string ButtonText2
        {
            get => _buttonText2;
            set
            {
                _buttonText2 = value;
                RaisePropertyChanged();
            }
        }
        public string ButtonText3
        {
            get => _buttonText3;
            set
            {
                _buttonText3 = value;
                RaisePropertyChanged();
            }
        }
        public string ButtonText4
        {
            get => _buttonText4;
            set
            {
                _buttonText4 = value;
                RaisePropertyChanged();
            }
        }


        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            StartQuestionGame();
        }
        public void StartQuestionGame()
        {
            this.CorrectGuesses = 0;
            this.CurrentQuestionNumber = 0;
            // restart question list?
            RandomizeQuestionCollection();
            GetCurrentQuestionRoom();
        }
        private void RandomizeQuestionCollection()
        {
            Random random = new Random();
            TemporaryQuestionCollection = new ObservableCollection<Question>(mainWindowViewModel.ActivePack.Questions.OrderBy(q => random.Next()));
        }
        //TemporaryQuestionList.Add(mainWindowViewModel.ActivePack.Questions[i]);
        public void GetCurrentQuestionRoom()
        {
            this.QuestionText = TemporaryQuestionCollection[CurrentQuestionNumber].Query;
            RandomizeButtonsForCurrentQuestion();
        }
        private void RandomizeButtonsForCurrentQuestion()
        {
            //Random random = new Random();
            //int randomizedButtons = random.Next(0, 4);
            this.ButtonText1 = TemporaryQuestionCollection[CurrentQuestionNumber].CorrectAnswer;
            this.ButtonText2 = TemporaryQuestionCollection[CurrentQuestionNumber].IncorrectAnswer1;
            this.ButtonText3 = TemporaryQuestionCollection[CurrentQuestionNumber].IncorrectAnswer2;
            this.ButtonText4 = TemporaryQuestionCollection[CurrentQuestionNumber].IncorrectAnswer3;
        }
        public void CheckIfAnswerIsCorrect()
        {
            
        }
    }
}
