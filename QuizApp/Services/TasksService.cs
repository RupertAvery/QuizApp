using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Services
{
    public class TasksService
    {
        private readonly List<Task> _tasks = new List<Task>();

        public void AddTask(Task taskToAdd)
        {
            _tasks.Add(taskToAdd);
        }

        public void RemoveTask(Task taskToRemove)
        {
            _tasks.Remove(taskToRemove);
        }

        public Task[] Tasks => _tasks.ToArray();
    }
}
