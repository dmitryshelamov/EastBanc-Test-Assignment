using AutoMapper;
using EastBancTestAssignment.KnapsackProblem.BLL.DTOs;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;

namespace EastBancTestAssignment.KnapsackProblem.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ItemDto, Item>()
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(des => des.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(des => des.Weight, opt => opt.MapFrom(src => src.Weight));

            CreateMap<Item, ItemDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(des => des.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(des => des.Weight, opt => opt.MapFrom(src => src.Weight));
        }
    }
}