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
        private readonly QuestionBuilder _questionBuilder;
        private readonly List<IMenuOption> _options = new List<IMenuOption>();
        private readonly IDatabase _db;
        private readonly TasksService _tasksService;

        public List<IMenuOption> Options => _options;
        public bool Exit { get; private set; }

        public Menu(
            QuestionBuilder questionBuilder,
            IDatabase db,
            TasksService tasksService
        )
        {
            _tasksService = tasksService;
            _questionBuilder = questionBuilder;
            _db = db;

            _options.Add(new MenuOption { Text = "Exit", Action = () => ExitApplication() });
            _options.Add(new MenuOption { Text = "New Quiz", Action = () => CreateNewQuiz() });
            _options.Add(new MenuOption { Text = "Reply to created quiz", Action = () => throw new NotImplementedException() });
        }

        public void CreateNewQuiz()
        {
            Console.WriteLine("Give me a quiz name!");

            while (true)
            {
                string quizName = Console.ReadLine();
                var titleValidator = new TitleValidator();

                if (titleValidator.Validate(quizName))
                {
                    Quiz newQuiz = new Quiz(quizName);
                    Console.Clear();

                    _questionBuilder.CreateQuestions(newQuiz);

                    if (newQuiz.HasQuestions)
                    {
                        _tasksService.AddTask(_db.SaveQuiz(newQuiz));
                    }

                    break;
                }

                Console.WriteLine("Invalid quiz name!");
                foreach (var validationError in titleValidator.ValidationErrors)
                {
                    Console.WriteLine(validationError);
                }
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


        public IMenuOption SelectMenuOption(int selection)
        {
            if (selection < 1 || selection > Options.Count) return null;
            return Options[selection - 1];
        }
    }
}
