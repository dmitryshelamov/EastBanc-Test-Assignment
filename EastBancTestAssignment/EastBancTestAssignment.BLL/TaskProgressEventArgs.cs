using System;

namespace EastBancTestAssignment.BLL
{
    public class TaskProgressEventArgs : EventArgs
    {
        public string Id { get; set; }
        public int Percent { get; set; }
    }
}
