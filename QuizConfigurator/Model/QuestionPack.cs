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

            //Question question1 = new Question("What color is the sky?", "blue", "green", "no color", "banana");
            //Question question2 = new Question("Do I own a cat?", "no", "yes", "maybe?", "banana");
            //Question question3 = new Question("What is my name?", "sam", "blubba", "kaloopsy", "strawberry");
            //Question question4 = new Question("What's in my coffee cup?", "nothing", "coffee", "cats", "juice");
            //Question question5 = new Question("Is donald trump gonna win?", "no", "yes", "help", "llama");

            //this.Questions = new List<Question>() { question1, question2, question3, question4, question5};
        }
    }
}
