using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using QuizApp.Controllers;
using QuizApp.Models;
using QuizApp.Models.Menu;
using QuizApp.Models.Menu.Interfaces;
using QuizApp.Services;
using QuizApp.Views;

namespace QuizApp
{
    public class Program
    {
        private static IContainer Container { get; set; }
        
        private static void Main(string[] args)
        {
            Build();
            StartApplication();
        }

        private static void StartApplication()
        {
            using ILifetimeScope scope = Container.BeginLifetimeScope();

            Menu menu = scope.Resolve<Menu>();
            var menuView = new MenuView();

            while(!menu.Exit)
            {
                menuView.ShowMenu(menu);
                IMenuOption menuAction = menu.SelectMenuOption();
                Console.Clear();
                menuAction?.Action(); 
            }

            var tasksService = scope.Resolve<TasksService>();
            Task.WaitAll(tasksService.Tasks);


        }

        private static void Build()
        {
            ContainerBuilder builder = new ContainerBuilder();
            //Assembly executingAssembly = Assembly.GetExecutingAssembly();
            //builder.RegisterAssemblyTypes(executingAssembly)
            //    .AsSelf()
            //    .AsImplementedInterfaces();

            
            builder.RegisterType<GameConfiguration>().InstancePerDependency();
            builder.RegisterType<DatabaseController>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<Menu>().InstancePerDependency();
            builder.RegisterType<MenuView>().InstancePerDependency();
            builder.RegisterType<MenuBuilder>().InstancePerDependency();
            builder.RegisterType<TasksService>().SingleInstance();

            Container = builder.Build();
        }
    }
}
