﻿using System;
using System.Collections.Generic;
using QuizApp.Models.Menu;

namespace QuizApp.Views
{
    public class MenuView
    {
        public void ShowMenu(Dictionary<string, IMenuOption> menuOptions)
        {
            Console.WriteLine("Hello in my quiz application!");
            Console.WriteLine("Select what would you like to do!");
             
            int i = 1;
            foreach (var option in menuOptions)
            {
                Console.WriteLine($"{i++}. {option.Key}");
            }

            Console.WriteLine("Make your choice!");
        }

        public void GiveQuizName()
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
    }
}
