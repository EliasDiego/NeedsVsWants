using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants
{
    public struct StringFormat
    {        
        static string RoundDecimals(double value) => 
            value % 1 == 0 ? value.ToString() + ".00" : ((float)Math.Round(value * 100f) / 100f).ToString();

        static string AddCommas(string value)
        {
            string intString = value.Split('.')[0];
            string valueText = "";

            int currentIndex;

            for(int i = intString.Length; i > 0; i -= 3)
            {
                currentIndex = Mathf.Clamp(i - 3, 0, intString.Length);

                valueText = intString.Substring(currentIndex, Mathf.Clamp(i - currentIndex, 0, 3)) + (intString.Length - currentIndex > 3 ? "," : "") + valueText;
            }

            return valueText + "." + value.Split('.')[1];
        }

        public static string ToPriceFormat(double price)
        {
            string formattedPrice;

            formattedPrice = price.ToString("C").Remove(0, 1);

            // formattedPrice = RoundDecimals(price);
            // formattedPrice = AddCommas(formattedPrice);

            return "P " + formattedPrice;
        }
    }
}