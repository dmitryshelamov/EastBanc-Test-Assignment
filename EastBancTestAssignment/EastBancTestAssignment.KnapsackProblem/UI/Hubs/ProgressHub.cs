using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace EastBancTestAssignment.KnapsackProblem.UI.Hubs
{
    public class ProgressHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}