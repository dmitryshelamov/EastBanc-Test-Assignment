using System.Collections.Generic;

namespace EastBancTestAssignment.BLL.DTOs
{
    public class BackpackTaskDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int WeightLimit { get; set; }
        public List<ItemDto> ItemDtos { get; set; }
        public BackpackTaskSolutionDto BackpackTaskSolutionDto { get; set; }

        public BackpackTaskDto()
        {
            BackpackTaskSolutionDto = new BackpackTaskSolutionDto();
            ItemDtos = new List<ItemDto>();
        }
    }
}
