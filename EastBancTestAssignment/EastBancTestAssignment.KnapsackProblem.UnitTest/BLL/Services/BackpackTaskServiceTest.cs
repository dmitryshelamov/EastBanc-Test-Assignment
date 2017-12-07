using System.Collections.Generic;
using System.Threading.Tasks;
using EastBancTestAssignment.KnapsackProblem.BLL.DTOs;
using EastBancTestAssignment.KnapsackProblem.BLL.Services;
using EastBancTestAssignment.KnapsackProblem.Core.Models;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace EastBancTestAssignment.KnapsackProblem.UnitTest.BLL.Services
{
    [TestFixture]
    public class BackpackTaskServiceTest
    {
        private List<ItemDto> _itemDtos;
        private IUnitOfWork _unitOfWork;

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
        }

        [Test]
        public async Task CreateNewBackpackTask_PassValidArgs_ShouldReturnBackpackTask()
        {
            //  arrange
            var taskName = "TaskName";
            var weightLimit = 8;
            BackpackTaskService service = new BackpackTaskService(_unitOfWork);
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
    }
}
