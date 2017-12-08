using AutoMapper;
using EastBancTestAssignment.KnapsackProblem.BLL.DTOs;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;
using EastBancTestAssignment.KnapsackProblem.UI.MVC.ViewModels;

namespace EastBancTestAssignment.KnapsackProblem.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ItemDto, Item>()
                .ForMember(des => des.Id, opt => opt.Ignore());
            CreateMap<Item, ItemDto>();

            CreateMap<ItemViewModel, ItemDto>();
        }
    }
}