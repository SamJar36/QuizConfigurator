using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Security.RightsManagement;
using System.IO;
using System.Xml;
using static System.Windows.Forms.Design.AxImporter;

namespace QuizConfigurator.Model
{
    internal class Configuration
    {
        private string SavedPath { get; set; }
        public Configuration()
        {
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string labb3FolderPath = Path.Combine(localAppDataPath, "Labb3");
            if (!Directory.Exists(labb3FolderPath))
            {
                Directory.CreateDirectory(labb3FolderPath);
            }
            string jsonFilePath = Path.Combine(labb3FolderPath, "QuestionPacks.json");
            SavedPath = jsonFilePath;
            if (!File.Exists(jsonFilePath))
            {
                var questionPack = new[]
                {
                    new
                    {
                        Name = "Default Question Pack",
                        Difficulty = 1,
                        TimeLimitInSeconds = 15,
                        Questions = new[]
                        {
                            new
                            {
                                Query = "What color is my shirt?",
                                CorrectAnswer = "Black",
                                IncorrectAnswer1 = "White",
                                IncorrectAnswer2 = "Green",
                                IncorrectAnswer3 = "Blue"
                            },
                            new
                            {
                                Query = "What did I eat for lunch?",
                                CorrectAnswer = "Sushi",
                                IncorrectAnswer1 = "Mexican",
                                IncorrectAnswer2 = "Italian",
                                IncorrectAnswer3 = "Candy"
                            },
                            new
                            {
                                Query = "What is my profession?",
                                CorrectAnswer = "Student",
                                IncorrectAnswer1 = "Librarian",
                                IncorrectAnswer2 = "Taxi Driver",
                                IncorrectAnswer3 = "Dentist"
                            }
                        }
                    }
                };
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(questionPack, options);
                File.WriteAllText(jsonFilePath, json);
            }         
        }
        public List<QuestionPack> Load()
        {
            string json = File.ReadAllText(SavedPath);
            var uggabugga = JsonSerializer.Deserialize<List<QuestionPack>>(json);
            return uggabugga!;
        }
        public void Save()
        {
            //serializing
        }
    }
}
