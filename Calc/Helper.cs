using System.Windows.Controls;

namespace Calc
{
   public static class Helper
    {
        public static string ToStr(this object value)
        {
            return (value as Button).Content.ToString();
        }
    }
}
