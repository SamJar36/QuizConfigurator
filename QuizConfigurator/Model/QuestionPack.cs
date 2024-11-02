using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizConfigurator.Model
{
    enum Difficulty { Easy, Medium, Hard }
    internal class QuestionPack
    {
        public string Name { get; set; }
        public Difficulty Difficulty { get; set; }
        public int TimeLimitInSeconds { get; set; }
        public List<Question> Questions { get; set; }
        public QuestionPack(string name, Difficulty difficulty = Difficulty.Medium, int timeLimitInSeconds = 30)
        {
            Name = name;
            Difficulty = difficulty;
            TimeLimitInSeconds = timeLimitInSeconds;

            Question question1 = new Question("What color is the sky?", "blue", "green", "no color", "banana");
            Question question2 = new Question("Do I own a cat?", "no", "yes", "maybe?", "banana");

            this.Questions = new List<Question>() { question1, question2};
        }
    }
}
