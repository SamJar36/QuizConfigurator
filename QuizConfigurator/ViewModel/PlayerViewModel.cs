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
        private int _totalQuestionAmount;
        private int _currentQuestionNumber;
        private string _questionText;
        private string _buttonText1;
        private string _buttonText2;
        private string _buttonText3;
        private string _buttonText4;
        private int _timeLeft;
        private int _timeLeftNextRoom;
        private DispatcherTimer _timer;
        private DispatcherTimer _nextRoomTimer;
        private string[] _buttonColors = new string[4];
        private bool _canShowCorrectAnswer;
        private bool _areThereQuestionsLeft;
        private bool _isFinishScreenShowing;
        private int _correctGuesses;
        public DelegateCommand SelectAnswerCommand { get; }
        public DelegateCommand RestartQuizCommand { get; }
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
        public int TimeLeft
        {
            get => _timeLeft;
            set
            {
                _timeLeft = value;
                RaisePropertyChanged();
            }
        }
        public int TimeLeftNextRoom
        {
            get => _timeLeftNextRoom;
            set
            {
                _timeLeftNextRoom = value;
                RaisePropertyChanged();
            }
        }
        public string[] ButtonColors
        {
            get => _buttonColors;
            set
            {
                _buttonColors = value;
                RaisePropertyChanged();
            }
        }
        public bool CanShowCorrectAnswer
        {
            get => _canShowCorrectAnswer;
            set
            {
                _canShowCorrectAnswer = value;
                RaisePropertyChanged();
            }
        }
        public bool AreThereQuestionsLeft
        {
            get => _areThereQuestionsLeft;
            set
            {
                _areThereQuestionsLeft = value;
                RaisePropertyChanged();
            }
        }
        public bool IsFinishScreenShowing
        {
            get => _isFinishScreenShowing;
            set
            {
                _isFinishScreenShowing = value;
                RaisePropertyChanged();
            }
        }
        public int CorrectGuesses
        {
            get => _correctGuesses;
            set
            {
                _correctGuesses = value;
                RaisePropertyChanged();
            }
        }

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            SelectAnswerCommand = new DelegateCommand(SelectAnswer, CanExecuteSelectAnswer);
            RestartQuizCommand = new DelegateCommand(RestartQuiz, CanExecuteRestartQuiz);
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            _nextRoomTimer = new DispatcherTimer();
            _nextRoomTimer.Interval = TimeSpan.FromSeconds(1);
            _nextRoomTimer.Tick += Timer_Tick_Until_Next_Room;
        }
        public void StartQuestionGame()
        {
            AreThereQuestionsLeft = true;
            IsFinishScreenShowing = false;
            CorrectGuesses = 0;
            CurrentQuestionNumber = 0;
            RandomizeQuestionCollection();
            GetNextQuestionRoom();
            TotalQuestionAmount = TemporaryQuestionCollection.Count;
        }
        private void RandomizeQuestionCollection()
        {
            Random random = new Random();
            TemporaryQuestionCollection = new ObservableCollection<Question>(mainWindowViewModel.ActivePack.Questions.OrderBy(q => random.Next()));
        }
        public void GetNextQuestionRoom()
        {
            CanShowCorrectAnswer = true;
            TimeLeft = mainWindowViewModel.ActivePack.TimeLimitInSeconds;
            _timer.Start();
            CurrentQuestionNumber++;
            QuestionText = TemporaryQuestionCollection[CurrentQuestionNumber - 1].Query;
            RandomizeButtonsForCurrentQuestion();
            ResetButtonColors();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (TimeLeft > 1)
            {
                TimeLeft--;
            }
            else
            {
                TimeLeft = 0;
                ShowCorrectAnswer();
            }
        }
        private void Timer_Tick_Until_Next_Room(object sender, EventArgs e)
        {
            if (TimeLeftNextRoom > 1)
            {
                TimeLeftNextRoom--;
            }
            else
            {
                TimeLeftNextRoom = 0;
                _nextRoomTimer.Stop();
                if (CurrentQuestionNumber < TemporaryQuestionCollection.Count)
                {
                    GetNextQuestionRoom();
                }
                else
                {
                    AreThereQuestionsLeft = false;
                    IsFinishScreenShowing = true;
                    mainWindowViewModel.UpdateAllCommands();
                }
            }
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
        public bool CanExecuteSelectAnswer(object? obj) => true;
        public void SelectAnswer(object obj)
        {
            var button = obj as Button;
            if (button != null)
            {
                string buttonText = button.Content.ToString();

                ShowCorrectAnswer(buttonText);
            }
        }
        public bool CanExecuteRestartQuiz(object? obj) => true;
        public void RestartQuiz(object obj)
        {
            mainWindowViewModel.UpdateAllCommands();
            StartQuestionGame();
        }
        public void ShowCorrectAnswer(string yourAnswer = null)
        {
            if (CanShowCorrectAnswer)
            {
                CanShowCorrectAnswer = false;
                _timer.Stop();
                TimeLeftNextRoom = 2;
                _nextRoomTimer.Start();
                if (yourAnswer == TemporaryQuestionCollection[CurrentQuestionNumber - 1].CorrectAnswer && yourAnswer != null)
                {
                    CorrectGuesses++;
                }
                ShowButtonColors();
            }  
        }
        private void ShowButtonColors()
        {
            ButtonColors[0] = ButtonText1 == TemporaryQuestionCollection[CurrentQuestionNumber - 1].CorrectAnswer ? "Green" : "Red";
            ButtonColors[1] = ButtonText2 == TemporaryQuestionCollection[CurrentQuestionNumber - 1].CorrectAnswer ? "Green" : "Red";
            ButtonColors[2] = ButtonText3 == TemporaryQuestionCollection[CurrentQuestionNumber - 1].CorrectAnswer ? "Green" : "Red";
            ButtonColors[3] = ButtonText4 == TemporaryQuestionCollection[CurrentQuestionNumber - 1].CorrectAnswer ? "Green" : "Red";
            RaisePropertyChanged(nameof(ButtonColors));
        }
        private void ResetButtonColors()
        {
            for (int i = 0; i < ButtonColors.Length; i++)
            {
                ButtonColors[i] = "White";
            }
            RaisePropertyChanged(nameof(ButtonColors));
        }
    }
}
