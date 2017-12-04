﻿//using System.Collections.Generic;
//using EastBancTestAssignment.BLL.DTOs;
//using EastBancTestAssignment.BLL.Services;
//using NUnit.Framework;
//
//namespace EastBancTestAssignment.UnitTest.BLL
//{
//    [TestFixture]
//    class BackpackTaskServiceTest
//    {
//        private List<ItemDto> _itemDtos;
//
//        [SetUp]
//        public void TestInitialize()
//        {
//            _itemDtos = new List<ItemDto>
//            {
//                new ItemDto { Name = "Book", Price = 600, Weight = 1},
//                new ItemDto { Name = "Binoculars", Price = 5000, Weight = 2},
//                new ItemDto { Name = "First Aid Kit", Price = 1500, Weight = 4},
//                new ItemDto { Name = "Laptop", Price = 40000, Weight = 2},
//                new ItemDto { Name = "Bowler", Price = 500, Weight = 1},
//            };
//        }
//
//        [Test]
//        public void CreateNewBackpackTask_PassValidArgs_ShouldReturnBackpackTask()
//        {
//            //  arrange
//            var taskName = "TaskName";
//            var weightLimit = 8;
//            BackpackTaskService service = new BackpackTaskService();
//            //  act
//            var backpackTask = service.CreateNewBackpackTask(_itemDtos, taskName, weightLimit);
//            //  assert
//            Assert.NotNull(backpackTask);
//            Assert.AreEqual(taskName, backpackTask.Name);
//            Assert.AreEqual(weightLimit, backpackTask.WeightLimit);
//            Assert.AreEqual(_itemDtos.Count, backpackTask.Items.Count);
//        }
//
//
//        [Test]
//        public void StartBackpackTask_PassValidBackpackTask_ShouldCalculateCorrect()
//        {
//            //  arrange
//            var taskName = "TaskName";
//            var weightLimit = 8;
//            BackpackTaskService service = new BackpackTaskService();
//            var backpackTask = service.CreateNewBackpackTask(_itemDtos, taskName, weightLimit);
//            //  act
//            service.StartBackpackTask(backpackTask);
//            //  assert
//            //  assert
//            Assert.AreEqual(31, backpackTask.BackpackTaskSolution.CombinationCalculated);
//            Assert.AreEqual(46500, backpackTask.BackpackTaskSolution.BestItemSetPrice);
//            Assert.AreEqual(8, backpackTask.BackpackTaskSolution.BestItemSetWeight);
//            Assert.AreEqual(3, backpackTask.BackpackTaskSolution.BestItemsSet.Count);
//        }
//    }
//}
