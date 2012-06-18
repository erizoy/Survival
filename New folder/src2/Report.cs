using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Costs
{    
    public struct CategoryReport 
    {
        public int categoryIndex;
        public decimal categoryTotal;
        public CategoryReport(int cI, decimal cT)
        {
            this.categoryIndex = cI;
            this.categoryTotal = cT;
        }
    }

    class Report
    {
        public static decimal getReportForCurrentCategory(int categoryIndex)
        {
            decimal result = 0;
            var reportItems = from item in Buyings.FullSet.GetAllItems
                         where item.CategoryIndex == categoryIndex
                         select item;
            foreach (var item in reportItems)
                result += item.Cost;
            return result;
        }

        public static List<CategoryReport> getCategoryReportList()
        {
            List<CategoryReport> list = new List<CategoryReport>();
            decimal categoryTotal;
            for (int i = -1; i < Buyings.FullSet.GetAllCategories.Count; i++)
            {
                categoryTotal = Report.getReportForCurrentCategory(i);
                if (categoryTotal != 0)
                    list.Add(new CategoryReport(i, categoryTotal));
            }
            list.Add(new CategoryReport(Income.INDEX, Report.getReportForCurrentCategory(Income.INDEX)));
            return list;
        }

        public static decimal getReportForCurrentCategoryBetween(int currentCategoryIndex, DateTime startDate, DateTime endDate)
        {
            decimal result = 0;
            var reportItems = from item in Buyings.FullSet.GetAllItems
                              where (item.BuyDate > startDate && item.BuyDate < endDate) && item.CategoryIndex == currentCategoryIndex
                              select item;
            foreach (var item in reportItems)
                result += item.Cost;
            return result;
        }

        public static List<CategoryReport> getCategoryReportListBetween(DateTime startDate, DateTime endDate)
        {
            List<CategoryReport> list = new List<CategoryReport>();
            decimal categoryTotal;
            for (int i = -1; i < Buyings.FullSet.GetAllCategories.Count; i++)
            {
                categoryTotal = Report.getReportForCurrentCategoryBetween(i, startDate, endDate);
                if (categoryTotal != 0)
                    list.Add(new CategoryReport(i, categoryTotal));
            }
            list.Add(new CategoryReport(Income.INDEX, Report.getReportForCurrentCategoryBetween(Income.INDEX, startDate, endDate)));
            return list;
        }
    }
}
