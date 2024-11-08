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
using System.Collections.ObjectModel;
using QuizConfigurator.ViewModel;
using System.Configuration;

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
            string jsonFilePath = Path.Combine(labb3FolderPath, "Labb3QuestionPacks.json");
            string otherFilePath = Path.Combine(labb3FolderPath, "Configuration.json");
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
                    },
                    new
                    {
                        Name = "Video Game Quiz",
                        Difficulty = 1,
                        TimeLimitInSeconds = 15,
                        Questions = new[]
                        {
                            new
                            {
                                Query = "Who is nearly always the protagonist in Zelda games?",
                                CorrectAnswer = "Link",
                                IncorrectAnswer1 = "Zelda",
                                IncorrectAnswer2 = "Ganon",
                                IncorrectAnswer3 = "Zora"
                            },
                            new
                            {
                                Query = "In Mario 64, which level is in the door all the way to the right when you first enter?",
                                CorrectAnswer = "Jolly Roger Bay",
                                IncorrectAnswer1 = "Cool Cool Mountain",
                                IncorrectAnswer2 = "Bob-omb Battlefield",
                                IncorrectAnswer3 = "Boo's Haunted Mansion"
                            },
                            new
                            {
                                Query = "What is the name of the antagonist who hunts you in the Waterfall zone in Undertale?",
                                CorrectAnswer = "Undyne",
                                IncorrectAnswer1 = "Sans",
                                IncorrectAnswer2 = "Papyrus",
                                IncorrectAnswer3 = "Asgore"
                            }
                        }
                    }
                };
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(questionPack, options);
                File.WriteAllText(SavedPath, json);
            }         
        }
        public List<QuestionPack> Load()
        {
            string read = File.ReadAllText(SavedPath);
            var json = JsonSerializer.Deserialize<List<QuestionPack>>(read);
            return json!;
        }
        public void Save(ObservableCollection<QuestionPackViewModel> packs)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(packs, options);
            File.WriteAllText(SavedPath, json);
        }
    }
}
