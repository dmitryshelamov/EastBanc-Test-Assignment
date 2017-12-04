﻿using System.Threading.Tasks;
using EastBancTestAssignment.Core.Models;

namespace EastBancTestAssignment.DAL.Interfaces.Repositories
{
    public interface IBackpackTaskRepository
    {
        void Add(BackpackTask backpackTask);
        Task<BackpackTask> Get(string id);
    }
}