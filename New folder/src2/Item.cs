using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Costs
{
    public class Item
    {
        private string iDescription;
        private DateTime iBuyDate;
        private decimal iCost;
        private string iComment;
        private int categoryIndex;
        private int itemIndex;

        public int CategoryIndex
        {
            get { return categoryIndex; }
            set { categoryIndex = value; }
        }
        public string Comment
        {
            get { return iComment; }
            set { iComment = value; }
        }

        public decimal Cost
        {
            get { return iCost; }
            set { iCost = value; }
        }

        public DateTime BuyDate
        {
            get { return iBuyDate; }
            set { iBuyDate = value.Date; }
        }

        public string Description
        {
            get { return iDescription; }
            set { iDescription = value; }
        }

        public string Category
        {
            get 
            {
                try { return Buyings.FullSet.GetAllCategories[categoryIndex].Name; }
                catch
                {
                    return categoryIndex == Income.INDEX ? Income.Name : Costs.Category.NAMEUNDEFINED;
                }
            }
            set
            {
                if (categoryIndex > -1 && categoryIndex < Buyings.FullSet.GetAllCategories.Count)
                {
                    Buyings.FullSet.GetAllCategories[categoryIndex].Name = value;
                }
            }
        }
    }
}
