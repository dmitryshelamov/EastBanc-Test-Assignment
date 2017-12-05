using EastBancTestAssignment.BLL.Services;
using Microsoft.AspNet.SignalR;

namespace EastBancTestAssignment.Web
{
    public class ProgressHub : Hub
    {
        private BackpackTaskService _service;
        public ProgressHub()
        {
            _service = BackpackTaskService.GetInstance();

            _service.OnUpdatProgressEventHandler += (sender, args) =>
            {
                Clients.Caller.ReportProgress(args.Id, args.Percent);
            };

            _service.OnTaskCompleteEventHandler += (sender, args) =>
            {
                Clients.Caller.ReportComplete(args.Id, args.WeightLimit, args.BestItemPrice, args.Percent, args.Status);
            };
        }
    }
}