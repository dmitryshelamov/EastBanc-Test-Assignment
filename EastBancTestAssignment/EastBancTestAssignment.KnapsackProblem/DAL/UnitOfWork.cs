﻿using System.Threading.Tasks;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces.Repositories;
using EastBancTestAssignment.KnapsackProblem.DAL.Repositories;

namespace EastBancTestAssignment.KnapsackProblem.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IBackpackTaskRepository BackpackTaskRepository { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            BackpackTaskRepository = new BackpackTaskRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}