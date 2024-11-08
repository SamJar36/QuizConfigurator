using QuizConfigurator.Command;
using QuizConfigurator.Model;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Linq;
using Microsoft.VisualBasic.Devices;

namespace QuizConfigurator.ViewModel
{
    internal class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        private ObservableCollection<Question> TemporaryQuestionCollection { get; set; }
        public int CorrectGuesses { get; set; }
        private int _totalQuestionAmount;
        private int _currentQuestionNumber;
        private string _questionText;
        private string _buttonText1;
        private string _buttonText2;
        private string _buttonText3;
        private string _buttonText4;
        public DelegateCommand SelectAnswerCommand { get; }
        public int CurrentQuestionNumber
        {
            get => _currentQuestionNumber;
            set
            {
                _currentQuestionNumber = value;
                RaisePropertyChanged();
            }
        }
        public int TotalQuestionAmount
        {
            get => _totalQuestionAmount;
            set
            {
                _totalQuestionAmount = value;
                RaisePropertyChanged();
            }
        }
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
            SelectAnswerCommand = new DelegateCommand(SelectAnswer, CanSelectAnswer);  
        }
        public void StartQuestionGame()
        {
            this.CorrectGuesses = 0;
            this.CurrentQuestionNumber = 0;
            RandomizeQuestionCollection();
            GetNextQuestionRoom();
            this.TotalQuestionAmount = TemporaryQuestionCollection.Count;
        }
        private void RandomizeQuestionCollection()
        {
            Random random = new Random();
            TemporaryQuestionCollection = new ObservableCollection<Question>(mainWindowViewModel.ActivePack.Questions.OrderBy(q => random.Next()));
        }
        public void GetNextQuestionRoom()
        {
            CurrentQuestionNumber++;
            this.QuestionText = TemporaryQuestionCollection[CurrentQuestionNumber - 1].Query;
            RandomizeButtonsForCurrentQuestion();
        }
        private void RandomizeButtonsForCurrentQuestion()
        {
            Random random = new Random();
            List<string> answers = new List<string>
            {
                TemporaryQuestionCollection[CurrentQuestionNumber - 1].CorrectAnswer,
                TemporaryQuestionCollection[CurrentQuestionNumber - 1].IncorrectAnswer1,
                TemporaryQuestionCollection[CurrentQuestionNumber - 1].IncorrectAnswer2,
                TemporaryQuestionCollection[CurrentQuestionNumber - 1].IncorrectAnswer3
            };
            for (int i = answers.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                string temp = answers[i];
                answers[i] = answers[j];
                answers[j] = temp;
            }
            this.ButtonText1 = answers[0];
            this.ButtonText2 = answers[1];
            this.ButtonText3 = answers[2];
            this.ButtonText4 = answers[3];
        }
        public bool CanSelectAnswer(object? obj) => true;
        public void SelectAnswer(object obj)
        {
            var button = obj as Button;
            if (button != null)
            {
                string buttonText = button.Content.ToString();

                CheckIfCorrectAnswer(buttonText);
            }
        }
        public void CheckIfCorrectAnswer(string yourAnswer)
        {
            // turn all buttons red and green
            if (yourAnswer == TemporaryQuestionCollection[CurrentQuestionNumber - 1].CorrectAnswer)
            {
                //score++
                GetNextQuestionRoom();
            }
            else
            {
                GetNextQuestionRoom();
            }
        }
    }
}
