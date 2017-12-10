using System;
using System.Linq;
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

            CreateMap<BestItemSet, ItemDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Item.Id))
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Item.Name))
                .ForMember(des => des.Price, opt => opt.MapFrom(src => src.Item.Price))
                .ForMember(des => des.Weight, opt => opt.MapFrom(src => src.Item.Weight));


            CreateMap<BackpackTask, BackpackTaskDto>()
                .ForMember(dest => dest.ItemDtos, opt => opt.MapFrom(src => src.BackpackItems))
                .ForMember(des => des.PercentComplete, opt => opt.ResolveUsing(src =>
                {
                    if (src.Complete)
                        return 100;
                    var progress = (int) Math.Round(
                        (double) (100 * src.CombinationSets.Count) /
                        (int) Math.Round(Math.Pow(2, src.BackpackItems.Count) - 1));
                    return progress;
                }))
                .ForMember(dest => dest.BestItemSet, opt => opt.ResolveUsing(src => src.BestItemSet));

            CreateMap<BackpackTaskDto, BackpackTaskViewModel>()
                .ForMember(des => des.StatusMessage, opt => opt.ResolveUsing(src =>
                {
                    return src.Complete ? "Complete" : "In Progress";
                }))
                .ForMember(des => des.BestPrice, opt => opt.ResolveUsing(src =>
                {
                    return !src.Complete ? default(int?) : src.BestItemSetPrice;
                }));

            CreateMap<BackpackTaskDto, BackpackTaskDetailViewModel>()
                .ForMember(des => des.Status, opt => opt.ResolveUsing(src =>
                {
                    return src.Complete ? "Complete" : "In Progress";
                }))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.ItemDtos))
                .ForMember(dest => dest.BestItemSet, opt => opt.MapFrom(src => src.BestItemSet))
                .ForMember(dest => dest.BestPrice, opt => opt.ResolveUsing(src => src.BestItemSetPrice))
                .ForMember(des => des.CalculationTime, opt => opt.ResolveUsing(src => src.EndTime - src.StartTime));

        }
    }
}