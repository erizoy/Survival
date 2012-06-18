using System.Drawing;

namespace Costs
{
    public class Category
    {
        public const string NAMEUNDEFINED = "Нет категории";
        public const int INDEXUNDEFINED = -1;
        public string Name;
        public string Description;
        public int cR;
        public int cG;
        public int cB;
        public Color getColor()
        {
            return Color.FromArgb(cR,cG,cB);
        }
        public void setColor(Color value)
        {
            cR = value.R;
            cG = value.G;
            cB = value.B;
        }
    }
}
