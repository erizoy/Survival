using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Costs
{
    public class Filter
    {
        public DateTime startDate, endDate;
        public decimal startCost, endCost;
        public string descriptionSearch;
        public List<int> categoryIndexes;

//        public int columnIndex = 3;
//        public ListSortDirection Direction = ListSortDirection.Descending;
        /// <summary>
/// Class constructor. All fields will be set to default values.
/// </summary>
        public Filter()
        {
            startDate = Buyings.MinDate();
            endDate = DateTime.Now;
            startCost = 0;
            endCost = 0;
            descriptionSearch = "";
            categoryIndexes = new List<int>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns items filtered by this object.</returns>
        public List<Item> getItemsSpecifiedBy()
        {
            return Buyings.FullSet.GetAllItems.FindAll(new Predicate<Item>(correctItem));
        }

        /// <summary>
        /// This is Predicate method.
        /// </summary>
        /// <param name="itm">Current item.</param>
        /// <returns>Returns is this Item correct to this filter or not.</returns>
        private bool correctItem(Item itm)
        {
            if (itm.BuyDate < startDate)
                return false;
            if (itm.BuyDate > endDate)
                return false;
            if (startCost != 0 && itm.Cost < startCost)
                return false;
            if (endCost != 0 && itm.Cost > endCost)
                return false;
            if (!String.IsNullOrWhiteSpace(descriptionSearch) && !itm.Comment.Contains(descriptionSearch))
                return false;
            if (!categoryIndexes.Contains(itm.CategoryIndex)&&categoryIndexes.Count!=0)
                return false;
            return true;
        }
    }
}
