﻿using System;
using System.Collections.Generic;
using System.Linq;
using QuizApp.Models;
using QuizApp.Models.Menu;
using QuizApp.Models.Menu.Interfaces;

namespace QuizApp.Views
{
    public class MenuView
    {
        public void ShowMenu(Menu menu)
        {
            Console.WriteLine("Hello in my quiz application!");
            Console.WriteLine("Select what would you like to do!");
             
            int i = 1;
            foreach (IMenuOption option in menu.Options)
            {
                Console.WriteLine($"{i++}. {option.Text}");
            }

            Console.WriteLine("Make your choice!");
        }

        public void ShowAnswers(List<string> answersTitle)
        {
            Console.WriteLine("Which answer is correct?");
            for (int i = 0; i < answersTitle.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {answersTitle[i]}");
            }

        }
    }
}
