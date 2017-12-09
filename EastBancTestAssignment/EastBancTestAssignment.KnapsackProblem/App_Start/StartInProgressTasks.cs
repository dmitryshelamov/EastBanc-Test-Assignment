using EastBancTestAssignment.KnapsackProblem.BLL.Services;

namespace EastBancTestAssignment.KnapsackProblem.App_Start
{
    public static class StartInProgressTasks
    {
        public static void StartTasks()
        {
            new BackpackTaskService().ContinueInProgressBackpackTasks();
        }
    }
}