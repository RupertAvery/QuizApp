using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using QuizApp.Exceptions;
using QuizApp.Interfaces;
using QuizApp.Models.Menu.Interfaces;
using QuizApp.Models.Menu.Options;
using QuizApp.Services;
using QuizApp.Validators;

namespace QuizApp.Models.Menu
{
    public class Menu
    {
        private readonly MenuBuilder _menuBuilder;
        private readonly List<IMenuOption> _options = new List<IMenuOption>();
        private readonly IDatabase _db;
        private readonly TasksService _tasksService;

        public List<IMenuOption> Options => _options;
        public bool Exit { get; private set; }

        public Menu(
            MenuBuilder menuBuilder,
            IDatabase db,
            TasksService tasksService
        )
        {
            _tasksService = tasksService;
            _menuBuilder = menuBuilder;
            _db = db;

            _options.Add(new MenuOption { Text = "Exit", Action = () => ExitApplication() });
            _options.Add(new MenuOption { Text = "New Quiz", Action = () => CreateNewQuiz() });
            _options.Add(new MenuOption { Text = "Reply to created quiz", Action = () => throw new NotImplementedException() });
        }

        public void CreateNewQuiz()
        {
            Console.WriteLine("Give me a quiz name!");

            string quizName = Console.ReadLine();
            Quiz newQuiz = Quiz.Create(quizName);
            Console.Clear();

            _menuBuilder.CreateQuestions(newQuiz);

            if (newQuiz.HasQuestions)
            {
                _tasksService.AddTask(_db.SaveQuiz(newQuiz));
            }
        }

        public void ExitApplication()
        {
            try
            {
                _tasksService.AddTask(Task.Delay(5000).ContinueWith((t) =>
                {
                    Console.WriteLine("Done waiting!");
                }));
                Exit = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public IMenuOption SelectMenuOption()
        {
            if (!Console.ReadLine().SelectIntParse(Options.Count, out int input))
            {
                Console.WriteLine("Incorrect input!");
                return null;
            }

            IMenuOption selectedOption = Options[input - 1];

            return selectedOption;
        }
    }
}
