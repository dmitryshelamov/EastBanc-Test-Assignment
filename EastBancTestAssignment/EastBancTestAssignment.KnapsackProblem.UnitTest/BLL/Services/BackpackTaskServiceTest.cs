using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EastBancTestAssignment.KnapsackProblem.App_Start;
using EastBancTestAssignment.KnapsackProblem.BLL.DTOs;
using EastBancTestAssignment.KnapsackProblem.BLL.Services;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;
using NSubstitute;
using NUnit.Framework;

namespace EastBancTestAssignment.KnapsackProblem.UnitTest.BLL.Services
{
    [TestFixture]
    public class BackpackTaskServiceTest
    {
        private List<ItemDto> _itemDtos;
        private List<Item> _items;
        private IUnitOfWork _unitOfWork;

        [OneTimeSetUp]
        public void GloablTestInitialize()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
        }

        [SetUp]
        public void TestInitialize()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _itemDtos = new List<ItemDto>
            {
                new ItemDto { Name = "Book", Price = 600, Weight = 1},
                new ItemDto { Name = "Binoculars", Price = 5000, Weight = 2},
                new ItemDto { Name = "First Aid Kit", Price = 1500, Weight = 4},
                new ItemDto { Name = "Laptop", Price = 40000, Weight = 2},
                new ItemDto { Name = "Bowler", Price = 500, Weight = 1},
            };
            _items = new List<Item>
            {
                new Item { Name = "Book", Price = 600, Weight = 1},
                new Item { Name = "Binoculars", Price = 5000, Weight = 2},
                new Item { Name = "First Aid Kit", Price = 1500, Weight = 4},
                new Item { Name = "Laptop", Price = 40000, Weight = 2},
                new Item { Name = "Bowler", Price = 500, Weight = 1},
            };
        }

        [Test]
        public async Task CreateNewBackpackTask_PassValidArgs_ShouldReturnBackpackTask()
        {
            //  arrange
            var taskName = "TaskName";
            var weightLimit = 8;
            BackpackTaskService service = new BackpackTaskService();
            BackpackTask backpackTask = null;
            _unitOfWork.BackpackTaskRepository.Add(Arg.Do<BackpackTask>(bc => backpackTask = bc));
            //  act
            string id = await service.NewBackpackTask(_itemDtos, taskName, weightLimit);
            //  assert
            Assert.NotNull(id);
            Assert.NotNull(backpackTask);
            Assert.AreEqual(taskName, backpackTask.Name);
            Assert.AreEqual(weightLimit, backpackTask.WeightLimit);
            Assert.AreEqual(_itemDtos.Count, backpackTask.BackpackItems.Count);
        }

        [Test]
        public async Task StartBackpackTask_PassValidBackpackTask_ShouldCalculateCorrect()
        {
            //  arrange
            var taskName = "TaskName";
            var weightLimit = 8;
            BackpackTaskService service = new BackpackTaskService();
            BackpackTask backpackTask = null;
            _unitOfWork.BackpackTaskRepository.Add(Arg.Do<BackpackTask>(bc => backpackTask = bc));
            string backpackTaskId = await service.NewBackpackTask(_itemDtos, taskName, weightLimit);
            //  act
            _unitOfWork.BackpackTaskRepository.Get(Arg.Any<string>()).Returns(backpackTask);
            await service.StartBackpackTask(backpackTaskId);
            //  assert
            //  assert
            Assert.AreEqual(31, backpackTask.CombinationSets.Count);
//            Assert.AreEqual(46500, backpackTask.BestItemSetPrice);
            Assert.AreEqual(weightLimit, backpackTask.BestItemSetWeight);
            Assert.AreEqual(3, backpackTask.BestItemSet.Count);
            Assert.IsTrue(backpackTask.CombinationSets.All(s => s.IsCalculated));
            Assert.IsTrue(backpackTask.Complete);
        }

        [Test]
        public async Task ContinueBackpackTask_PassValidBackpackTask_ShouldComplete()
        {
            //  arrange
            List<ItemCombination> itemCombinations = new List<ItemCombination>();
            itemCombinations.Add(new ItemCombination { Item = _items[0] });
            itemCombinations.Add(new ItemCombination { Item = _items[1] });


            BackpackTask backpackTask = new BackpackTask();
            backpackTask.Id = "id";
            backpackTask.Name = "ContinueTask";
            backpackTask.WeightLimit = 8;
            backpackTask.BackpackItems = _items;
            backpackTask.CombinationSets = new List<CombinationSet>
            {
                new CombinationSet()
                {
                    Id = "AlreadyExist1",
                    IsCalculated = true,
                    ItemCombinations = itemCombinations
                },
            };



            BackpackTaskService service = new BackpackTaskService();
            _unitOfWork.BackpackTaskRepository.Add(Arg.Do<BackpackTask>(bc => backpackTask = bc));
            //  act
            _unitOfWork.BackpackTaskRepository.Get(Arg.Any<string>()).Returns(backpackTask);
            await service.StartBackpackTask("id");
            //  assert
            //  assert
            Assert.AreEqual(31, backpackTask.CombinationSets.Count);
            Assert.AreEqual(46500, backpackTask.BestItemSetPrice);
            Assert.AreEqual(8, backpackTask.BestItemSetWeight);
            Assert.AreEqual(3, backpackTask.BestItemSet.Count);
            Assert.IsTrue(backpackTask.CombinationSets.All(s => s.IsCalculated));
        }
    }
}
