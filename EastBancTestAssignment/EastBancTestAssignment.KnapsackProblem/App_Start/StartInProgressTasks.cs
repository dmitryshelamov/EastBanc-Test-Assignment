using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EastBancTestAssignment.KnapsackProblem.BLL.Services;
using EastBancTestAssignment.KnapsackProblem.DAL;
using EastBancTestAssignment.KnapsackProblem.DAL.Repositories;

namespace EastBancTestAssignment.KnapsackProblem.App_Start
{
    public static class StartInProgressTasks
    {
        public static void StartTasks()
        {
            new BackpackTaskService(new UnitOfWork(new AppDbContext())).ContinueInProgressBackpackTasks();
        }
    }
}