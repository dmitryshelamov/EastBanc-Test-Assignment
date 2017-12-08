using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EastBancTestAssignment.KnapsackProblem.DAL.Interfaces;
using EastBancTestAssignment.KnapsackProblem.DAL.Models;

namespace EastBancTestAssignment.KnapsackProblem.BLL.Services
{
    public static class CalculationService
    {

        public static void StartCalculation(BackpackTask backpackTask, TaskProgress service, IUnitOfWork unitOfWork)
        {
            List<Item> set = backpackTask.BackpackItems;
            List<CombinationSet> result = backpackTask.CombinationSets;
            if (set.Count > 0 && !result.Where(l => l.ItemCombinations.Count == set.Count).Any(l =>
            {
                List<Item> items = l.ItemCombinations.Select(argItemCombination => argItemCombination.Item).ToList();
                return items.SequenceEqual(set);
            }))
            {
                CombinationSet combinationSet = new CombinationSet { ItemCombinations = set.Select(item => new ItemCombination { Item = item }).ToList() };
                result.Add(combinationSet);
                CalculateCombinationSet(backpackTask, combinationSet, service, unitOfWork);
            }

            GenerateCombinationRecursive(backpackTask, set, result, service, unitOfWork);
        }

        private static void GenerateCombinationRecursive(BackpackTask backpackTask, List<Item> set, List<CombinationSet> result, TaskProgress service, IUnitOfWork unitOfWork)
        {
            for (int i = 0; i < set.Count; i++)
            {
                List<Item> temp = new List<Item>(set.Where((s, index) => index != i));

                if (temp.Count > 0 && !result.Where(l => l.ItemCombinations.Count == temp.Count).Any(l =>
                {
                    List<Item> items = l.ItemCombinations.Select(argItemCombination => argItemCombination.Item).ToList();
                    return items.SequenceEqual(temp);
                }))
                {
                    CombinationSet combinationSet = new CombinationSet { ItemCombinations = temp.Select(item => new ItemCombination { Item = item }).ToList() };
                    result.Add(combinationSet);
                    CalculateCombinationSet(backpackTask, combinationSet, service, unitOfWork);
                    GenerateCombinationRecursive(backpackTask, temp, result, service, unitOfWork);
                }
            }
        }

        private static void CalculateCombinationSet(BackpackTask backpackTask, CombinationSet set, TaskProgress service, IUnitOfWork unitOfWork)
        {
            //  iterate over all item combinations
            Debug.WriteLine("CombinationSet Id: " + set.Id);
            //  iterate over all item int current item set
            //  calculate total weight and price of current item set
            var totalWeight = 0;
            var totalPrice = 0;
            foreach (var itemCombination in set.ItemCombinations)
            {
                totalWeight += itemCombination.Item.Weight;
                totalPrice += itemCombination.Item.Price;
            }

            //  check if we ok with totalWeight and current price of item set is greater that current best item set price
            if (totalWeight <= backpackTask.WeightLimit &&
                totalPrice > backpackTask.BestItemSetPrice)
            {
                //  update current solution
                backpackTask.BestItemSetPrice = totalPrice;
                backpackTask.BestItemSetWeight = totalWeight;
                List<Item> items = new List<Item>();
                foreach (var itemCombo in set.ItemCombinations)
                {
                    items.Add(itemCombo.Item);
                }
                backpackTask.BestItemSet = new List<BestItemSet>();
                foreach (var item in items)
                {
                    backpackTask.BestItemSet.Add(new BestItemSet() {Item = item});
                }
            }
            //  mark current set as calucated
            set.IsCalculated = true;
            unitOfWork.Complete();
            service.UpdateProgress();
        }
    }
}