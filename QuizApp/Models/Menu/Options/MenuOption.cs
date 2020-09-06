﻿using System;
using System.Threading.Tasks;
using QuizApp.Controllers;
using QuizApp.Interfaces;
using QuizApp.Models.Menu.Interfaces;

namespace QuizApp.Models.Menu.Options
{
    public class MenuOption : IMenuOption
    {
        public string Text { get; set; }
        public Action Action { get; set; }
    }
}
