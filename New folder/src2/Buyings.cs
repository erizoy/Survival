using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;

namespace Costs
{
    public class Buyings
    {
        private List<Item> bAllItems = new List<Item>();
        private List<Category> bAllCategories = new List<Category>();

        /// <summary>
        /// Get or sets Categories Collection
        /// </summary>
        public List<Category> GetAllCategories
        {
            get { return bAllCategories; }
            set { bAllCategories = value; }
        }

        /// <summary>
        /// Get or sets Items Collection
        /// </summary>
        public List<Item> GetAllItems
        {
            get { return bAllItems; }
            set { bAllItems = value; }
        }

        /// <summary>
        /// Field to access singleton class.
        /// </summary>
        public static Buyings FullSet = new Buyings();

        /// <summary>
        /// Default constructor.
        /// </summary>
        private Buyings()
        {
        }

        /// <summary>
        /// Saves object to file
        /// </summary>
        public void FullDeserialize()
        {
            XmlSerializer sr = new XmlSerializer(typeof(Buyings));
            FileStream w = new FileStream("Serialize.xml", FileMode.Open);
            FullSet = (Buyings)sr.Deserialize(w);
            w.Close();
        }

        /// <summary>
        /// Loads object from file.
        /// </summary>
        public void FullSerialize()
        {
            Buyings f = FullSet;
            XmlSerializer sr = new XmlSerializer(typeof(Buyings));
            FileStream w = new FileStream("Serialize.xml", FileMode.Create);
            sr.Serialize(w, f);
            w.Close();
        }
        
        /// <summary>
        /// Computes total income.
        /// </summary>
        public static decimal TotalIncome
        {
            get {
                decimal total=0;
                foreach (Item itm in Buyings.FullSet.GetAllItems.FindAll(new Predicate<Item>(isIncome)))
                    total += itm.Cost;
                return total;
            }
        }
        
        /// <summary>
        /// Computes total outcome.
        /// </summary>
        public static decimal TotalOutcome
        {
            get {
                decimal total = 0;
                foreach (Item itm in Buyings.FullSet.GetAllItems.FindAll(new Predicate<Item>(isOutcome)))
                    total += itm.Cost;
                return total;
            }
        }
        
        /// <summary>
        /// Checks if current item is income.
        /// </summary>
        /// <param name="i">Item index in Items Collection.</param>
        /// <returns>Returns true if income, false if not</returns>
        private static bool isIncome(Item i)
        {
            return i.CategoryIndex == Income.INDEX;
        }

        /// <summary>
        /// Checks if current item is outcome.
        /// </summary>
        /// <param name="i">Item index in Items Collection.</param>
        /// <returns>Returns true if outcome, false if not</returns>
        private static bool isOutcome(Item i)
        {
            return i.CategoryIndex != Income.INDEX;
        }
        
        /// <summary>
        /// Returns Brush for Category specified by Index.
        /// </summary>
        /// <param name="id">Category index in Categories Collection.</param>
        /// <returns>Returns Brush for Category specified by Index.</returns>
        public static Brush getBrushByCategoryId(int id)
        {
            if (id == Income.INDEX)
                return Brushes.LightGreen;
            if (id == -1)
                return Brushes.LightGray;
            return new Pen(Buyings.FullSet.GetAllCategories[id].getColor()).Brush;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string getNameByCategoryId(int id)
        {
            if (id == Income.INDEX)
                return Income.Name;
            if (id == -1)
                return Category.NAMEUNDEFINED;
            return Buyings.FullSet.GetAllCategories[id].Name;
        }

        public static int getIdByCategoryName(string name)
        {
            if (name == Income.Name)
                return Income.INDEX;
            foreach (Category cat in Buyings.FullSet.GetAllCategories)
                if (cat.Name == name)
                    return Buyings.FullSet.GetAllCategories.IndexOf(cat);
            return Category.INDEXUNDEFINED;
        }

        public static DateTime MinDate()
        {
            DateTime minDT = DateTime.Now;
            for (int i = 1; i < Buyings.FullSet.GetAllItems.Count; i++)
                if (DateTime.Compare(minDT, Buyings.FullSet.GetAllItems[i].BuyDate) == 1)
                    minDT = Buyings.FullSet.GetAllItems[i].BuyDate;
            return minDT;
        }

        public static decimal MaxPrice()
        {
            decimal price = 0;
            for (int i = 1; i < Buyings.FullSet.GetAllItems.Count; i++)
                if (Buyings.FullSet.GetAllItems[i].Cost > price)
                    price = Buyings.FullSet.GetAllItems[i].Cost;
            return price;
        }
    }
}
