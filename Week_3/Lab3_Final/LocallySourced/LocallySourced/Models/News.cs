using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocallySourced.Models
{
    public class News
    {
        public int NewsID { get; set; }

        public string HeadLine { get; set; }
        public DateTime Date { get; set; }
        public string Article { get; set; }

        public static string Ordinal(int number)
        {
            var ones = number % 10;
            var tens = Math.Floor(number / 10f) % 10;
            if (tens == 1)
            {
                return number + "th";
            }

            switch (ones)
            {
                case 1: return number + "st";
                case 2: return number + "nd";
                case 3: return number + "rd";
                default: return number + "th";
            }
        }
    }

    

    

 
}