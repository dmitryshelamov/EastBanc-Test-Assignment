using System;

namespace EastBancTestAssignment.BLL.Services
{
    public class TaskPrepareEventArgs : EventArgs
    {
        public string Id { get; set; }
        public int Percent { get; set; }
        public string Message { get; set; }
    }
}
