﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.Models.Menu
{
    public class MenuOptionNewQuiz : IMenuOption
    {
        public void Action()
        {
            Console.WriteLine("New Quiz");
        }
    }
}
