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

            Question question = new Question("What color is the sky?", "blue", "green", "purple", "carrot");
            Question question2 = new Question("How do you spell dog?", "dog", "caterpillar", "swan", "carrot");

            Questions = new List<Question>() { question, question2 };
        }
    }
}
