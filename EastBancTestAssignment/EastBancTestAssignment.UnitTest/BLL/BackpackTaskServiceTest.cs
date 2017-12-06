using System.Collections.Generic;
using System.Threading.Tasks;
using EastBancTestAssignment.BLL.DTOs;
using EastBancTestAssignment.BLL.Services;
using NUnit.Framework;

namespace EastBancTestAssignment.UnitTest.BLL
{
    [TestFixture]
    class BackpackTaskServiceTest
    {
        private List<ItemDto> _itemDtos;

        [SetUp]
        public void TestInitialize()
        {
            _itemDtos = new List<ItemDto>
            {
                new ItemDto { Name = "Book", Price = 600, Weight = 1},
                new ItemDto { Name = "Binoculars", Price = 5000, Weight = 2},
                new ItemDto { Name = "First Aid Kit", Price = 1500, Weight = 4},
                new ItemDto { Name = "Laptop", Price = 40000, Weight = 2},
                new ItemDto { Name = "Bowler", Price = 500, Weight = 1},
            };
        }

        [Test]
        public async Task CreateNewBackpackTask_PassValidArgs_ShouldReturnBackpackTask()
        {
            //  arrange
            var taskName = "TaskName";
            var weightLimit = 8;
            BackpackTaskService service = BackpackTaskService.GetInstance();
            //  act
            var backpackTask = await service.NewBackpackTask(_itemDtos, taskName, weightLimit);
            //  assert
            Assert.NotNull(backpackTask);
            Assert.AreEqual(taskName, backpackTask.Name);
            Assert.AreEqual(weightLimit, backpackTask.WeightLimit);
            Assert.AreEqual(_itemDtos.Count, backpackTask.ItemDtos.Count);
        }


        [Test]
        public async Task StartBackpackTask_PassValidBackpackTask_ShouldCalculateCorrect()
        {
            //  arrange
            var taskName = "TaskName";
            var weightLimit = 8;
            BackpackTaskService service = BackpackTaskService.GetInstance();
            var backpackTask = await service.NewBackpackTask(_itemDtos, taskName, weightLimit);
            //  act
            await service.StartBackpackTask(backpackTask.Id);
            //  assert
            //  assert
            Assert.AreEqual(31, backpackTask.CurrentProgress);
            Assert.AreEqual(46500, backpackTask.BestItemSetPrice);
            Assert.AreEqual(8, backpackTask.BestItemSetWeight);
            Assert.AreEqual(3, backpackTask.BestItemDtosSet.Count);
        }
    }
}
