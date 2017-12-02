﻿using System.Data.Entity;
using EastBancTestAssignment.Core.Models;

namespace EastBancTestAssignment.DAL.Interfaces
{
    public interface IAppDbContext
    {
        IDbSet<Item> Items { get; }
        IDbSet<BackpackTask> BackpackTasks { get; }
    }
}