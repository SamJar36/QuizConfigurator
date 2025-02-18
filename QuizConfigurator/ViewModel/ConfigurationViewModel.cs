﻿using QuizConfigurator.Command;
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
        public DelegateCommand AddQuestionCommand { get; }
        public DelegateCommand RemoveQuestionCommand { get; }
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
        //public string? ActivePackName => mainWindowViewModel?.ActivePack?.Name;

        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            AddQuestionCommand = new DelegateCommand(AddQuestion, CanAddQuestion);
            RemoveQuestionCommand = new DelegateCommand(RemoveQuestion, CanRemoveQuestion);
        }
        public bool CanAddQuestion(object? arg) => mainWindowViewModel?.Packs.Count > 0 && mainWindowViewModel?.CurrentView != "Player";
        public void AddQuestion(object obj)
        {
            mainWindowViewModel?.ActivePack?.Questions.Add(new Question("New Question", "", "", "", ""));
            mainWindowViewModel?.UpdateAllCommands();
        }
        public bool CanRemoveQuestion(object? arg) => mainWindowViewModel?.ActivePack?.Questions.Count > 0 && mainWindowViewModel?.CurrentView != "Player";
        public void RemoveQuestion(object? obj)
        {
            mainWindowViewModel?.ActivePack?.Questions.Remove(SelectedQuestion);
            mainWindowViewModel?.UpdateAllCommands();
        }
    }
}
