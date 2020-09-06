using System;
using System.Collections.Generic;
using System.Linq;
using QuizApp.Validators;
using QuizApp.Views;

namespace QuizApp.Models.Menu
{
    public class MenuBuilder
    {
        private readonly MenuView _menuView;
        private readonly GameConfiguration _gameConfiguration;

        public MenuBuilder(MenuView menuView, GameConfiguration gameConfiguration)
        {
            _menuView = menuView;
            _gameConfiguration = gameConfiguration;
        }

        public void CreateQuestions(Quiz newQuiz)
        {
            _menuView.HowManyQuestions();
            int maxNumberOfQuestions = _gameConfiguration.MaxQuestions;

            if (!Console.ReadLine()
                .ParseInRange(0, maxNumberOfQuestions, out int input))
            {
                Console.WriteLine("Incorrect input!");
                return;
            }
            Console.Clear();
            for (int i = 0; i < input; i++)
            {
                _menuView.AskForQuestion(i + 1);
                string questionTitle = Console.ReadLine();
                Question question = Question.Create(questionTitle);
                Console.Clear();
                CreateAnswers(question);
                SelectCorrectAnswer(question);

                newQuiz.AddQuestion(question);
                Console.Clear();
            }
        }

        private void CreateAnswers(Question currentQuestion)
        {
            int numberOfAnswers = _gameConfiguration.NumberOfAnswers;
            for (int i = 0; i < numberOfAnswers; i++)
            {
                Console.Clear();
                _menuView.AskForAnswer(i + 1);
                Answer newAnswer = Answer.Create(Console.ReadLine());
                currentQuestion.AddAnswer(newAnswer);
            }
        }

        private void SelectCorrectAnswer(Question currentQuestion)
        {
            Console.Clear();
            List<Answer> answers = currentQuestion.Answers;
            List<string> answersTitle = answers.Select(x => x.Title).ToList();
            _menuView.ShowAnswers(answersTitle);

            if (Console.ReadLine().SelectIntParse(answersTitle.Count, out int input))
            {
                answers[input - 1].IsCorrect = true;
            }
            else
            {
                Console.WriteLine("Incorrect input");
            }

        }

    }
}