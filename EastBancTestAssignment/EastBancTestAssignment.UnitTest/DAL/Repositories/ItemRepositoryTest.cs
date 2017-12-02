using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EastBancTestAssignment.Core.Models;
using EastBancTestAssignment.DAL.Interfaces;
using EastBancTestAssignment.DAL.Repositories;
using EastBancTestAssignment.UnitTest.Extensions;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;

namespace EastBancTestAssignment.UnitTest.DAL.Repositories
{
    [TestFixture]
    public class ItemRepositoryTest
    {
        private ItemRepository _repository;
        private IDbSet<Item> _itemsDbSet;
        private List<Item> _itemsInDb;
        private IAppDbContext _context;


        [SetUp]
        public void TestInitialize()
        {
            _itemsDbSet = Substitute.For<IDbSet<Item>>();
            _context = Substitute.For<IAppDbContext>();
            _context.Items.Returns(_itemsDbSet);
            _repository = new ItemRepository(_context);
            _itemsInDb = new List<Item>
            {
                new Item { Id = "1", Name = "Book", Price = 600, Weight = 1},
                new Item { Id = "2", Name = "Binoculars", Price = 5000, Weight = 2},
                new Item { Id = "3", Name = "First Aid Kit", Price = 1500, Weight = 4},
                new Item { Id = "4", Name = "Laptop", Price = 40000, Weight = 2},
                new Item { Id = "5", Name = "Bowler", Price = 500, Weight = 1},
            };
        }

        [Test]
        public void Add_PassValidItem_ShouldBeAdded()
        {
            //  arrange
            _itemsDbSet.SetSource(_itemsInDb);
            //  act
            _repository.Add(new Item());
            //  assert
            _itemsDbSet.Received(1).Add(Arg.Any<Item>());
        }

        [Test]
        public void Get_PassValidId_ShouldReturnItem()
        {
            //  arrange
            _itemsDbSet.SetSource(_itemsInDb);
            //  act
            var item = _repository.Get("2");
            //  assert
            Assert.NotNull(item);
            Assert.AreEqual(_itemsInDb[1], item);
        }

        [Test]
        public void Get_PassInvalidId_ShouldReturnNull()
        {
            //  arrange
            _itemsDbSet.SetSource(_itemsInDb);
            //  act
            var item = _repository.Get("Invalid_Id");
            //  assert
            Assert.IsNull(item);
        }
    }
}
