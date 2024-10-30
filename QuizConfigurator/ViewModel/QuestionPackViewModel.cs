using QuizConfigurator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizConfigurator.ViewModel
{
    internal class QuestionPackViewModel : ViewModelBase
    {
        private readonly QuestionPack model;

        public ObservableCollection<Question> Questions { get; }
        public string Name
        {
            get => model.Name;
            set
            {
                model.Name = value;
            }
        }
        public Difficulty Difficulty
        {
            get => model.Difficulty;
            set
            {
                model.Difficulty = value;
            }
        }
        public int TimeLimitInSeconds
        {
            get => model.TimeLimitInSeconds;
            set
            {
                model.TimeLimitInSeconds = value;
                // CallerMemberName kommer automatiskt anropa t.ex TimeLimitInSeconds så man inte behöver skriva in det som ett argument
                RaisePropertyChanged();
            }
        }
        public QuestionPackViewModel(QuestionPack model)
        {
            this.model = model;
            this.Questions = new ObservableCollection<Question>(model.Questions);
        }
    }
}
