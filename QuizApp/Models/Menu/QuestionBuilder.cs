using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using QuizApp.Validators;
using QuizApp.Views;

namespace QuizApp.Models.Menu
{



    public class QuestionBuilder
    {
        private readonly MenuView _menuView;
        private readonly GameConfiguration _gameConfiguration;
        private readonly TitleValidator _titleValidator;

        public QuestionBuilder(MenuView menuView, GameConfiguration gameConfiguration)
        {
            _menuView = menuView;
            _gameConfiguration = gameConfiguration;
            _titleValidator = new TitleValidator();
        }

        public void CreateQuestions(Quiz newQuiz)
        {
            Console.WriteLine("How many questions do you want to have?");

            int maxNumberOfQuestions = _gameConfiguration.MaxQuestions;
            var rangeValidator = new RangeValidator(1, maxNumberOfQuestions);

            int input;
            while (true)
            {
                if (ConsoleEx.TryReadInt(out input))
                {
                    if (rangeValidator.Validate(input))
                    {
                        break;
                    }

                    foreach (var validationError in rangeValidator.ValidationErrors)
                    {
                        Console.WriteLine(validationError);
                    }
                }
                Console.WriteLine("Incorrect input!");
            }

            Console.Clear();
            for (int i = 0; i < input; i++)
            {
                Console.WriteLine($"Please give me a title to  question {i + 1}");
                while (true)
                {
                    string questionTitle = Console.ReadLine();
                    if (_titleValidator.Validate(questionTitle))
                    {
                        Question question = new Question(questionTitle);
                        Console.Clear();
                        CreateAnswers(question);
                        SelectCorrectAnswer(question);

                        newQuiz.AddQuestion(question);
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        foreach (var validationError in _titleValidator.ValidationErrors)
                        {
                            Console.WriteLine(validationError);
                        }
                    }
                }
            }
        }

        private void CreateAnswers(Question currentQuestion)
        {
            int numberOfAnswers = _gameConfiguration.NumberOfAnswers;
            for (int i = 0; i < numberOfAnswers; i++)
            {
                Console.Clear();
                Console.WriteLine($"Give the text for answer {i + 1}");
                var value = Console.ReadLine();
                if (_titleValidator.Validate(value))
                {
                    var newAnswer = new Answer(value);
                    currentQuestion.AddAnswer(newAnswer);
                }
            }
        }

        private void SelectCorrectAnswer(Question currentQuestion)
        {
            Console.Clear();
            List<Answer> answers = currentQuestion.Answers;
            List<string> answersTitle = answers.Select(x => x.Title).ToList();

            // Here we can now loop until we get a correct input
            while (true)
            {
                _menuView.ShowAnswers(answersTitle);

                // This is more verbose, but also more clear what is happening and allows us to react properly
                if (ConsoleEx.TryReadInt(out int input))
                {
                    if (input > 0 && input < answersTitle.Count)
                    {
                        answers[input - 1].IsCorrect = true;
                        break;
                    }
                }
                Console.WriteLine("Incorrect input");
            }

            // Don't mix parsing and validation
            // it makes it hard to act on validation properly

            //if (Console.ReadLine().SelectIntParse(answersTitle.Count, out int input))
            //{
            //}
            //else
            //{
            //}

        }


        private void GiveQuizName()
        {
            Console.WriteLine("Give me a quiz name!");
        }

        public void HowManyQuestions()
        {
            Console.WriteLine("How many questions do you want to have?");
        }

        public void AskForQuestion(int numberOfQuestion)
        {
            Console.WriteLine($"Please give ma a title to {numberOfQuestion} question");
        }

        public void AskForAnswer(int answerNumber)
        {
            Console.WriteLine($"Give a {answerNumber} answer title");
        }


    }
}