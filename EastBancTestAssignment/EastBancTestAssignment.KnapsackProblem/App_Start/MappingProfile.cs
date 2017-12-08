﻿using System;
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

            CreateMap<BackpackTask, BackpackTaskDto>()
                .ForMember(dest => dest.ItemDtos, opt => opt.MapFrom(src => src.BackpackItems))
                .ForMember(des => des.PercentComplete, opt => opt.ResolveUsing(src =>
                {
                    var progress = (int) Math.Round(
                        (double) (100 * src.CombinationSets.Where(i => i.IsCalculated).ToList().Count) /
                        (int) Math.Round(Math.Pow(2, src.BackpackItems.Count) - 1));
                    return progress;

                }));

            CreateMap<BackpackTaskDto, BackpackTaskViewModel>()
                .ForMember(des => des.Status, opt => opt.ResolveUsing(src =>
                {
                    if (src.Complete)
                        return "Complete";
                    return "In Progress";
                }));

        }
    }
}