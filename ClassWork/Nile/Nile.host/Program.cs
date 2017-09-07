/*
 * Mohamed Rahaman
 * ITSE 1430
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.host 
{    
    class Program 
    {
        // A single line comment 
        static void Main( string[] args )
        {
            int hours;
            hours = 10; 

            //+-*/%
            //hours = (4 + 3) * 7.25 / 4;
            //hours = Math.Min(hours, 30); 

            string name = "John";

            //Concat 
            name = name + "Williams";

            //Copy
            name = "Hello";

            bool areEqual = name == "Hello";
            bool areNotEqual = name != "Hello";
                                              
            // Verbatim String - no escape sequences
            string path = @"C:\Temp\test.txt";

            //Option 1
            string names = "John" + "William" + "Murphy" + "Charles" + "Henry";
            
            //Option 2
            StringBuilder builder = new StringBuilder();
            builder.Append("John");
            builder.Append("William");
            string name2 = builder.ToString();

            //Option 3
            string names3 = String.Concat("John" + "William" + "Murphy" + "Charles" + "Henry"); // String.Concat uses StringBuilder

            //String Formatting 
            //John worked 10 hours
            string format1 = name + "worked" + hours.ToString() + " hours";

            string format2 = String.Format("{0} work for {1} hours", name, hours); 
        }
    }
}