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
        static void Main( string[] args )
        {
            bool quit = false; 
            do
            {
                char choice = GetInput();
                switch (choice)
                {
                    case 'a':
                    case 'A': AddProducts(); break;

                    case 'l':
                    case 'L': ListProducts(); break;

                    case 'q': 
                    case 'Q': quit = true; break; 
                };

            } while (!quit);
        }

        private static void AddProducts()
        {
            Console.Write("Enter Product Name: ");
            productName = Console.ReadLine().Trim();

            //Ensure not empty

            Console.Write("Enter Product Price: ");
            productPrice = ReadDecimal();

            Console.Write("Enter Product Description: ");
            productDescription = Console.ReadLine().Trim();

            Console.Write("Is it Discontinued (Y/N): ");
             productDiscontinued = ReadYesNO();
        }

        private static void ListProducts()
        {
            //Name $prices [Discontinued]
            //Description
            // Option 1: 
            //string msg = String.Format("{0}\t\t\t${1}\t\t{2}", productName, productPrice, productDiscontinued ? "[Discontinued]" : "");
            
            // Option 2: 
            //Console.WriteLine(String.Format("{0}\t\t\t${1}\t\t{2}", productName, productPrice, productDiscontinued ? "[Discontinued]" : ""));

            // Option 3: 
            string msg = $"{productName}\t\t\t${productPrice}\t\t{(productDiscontinued ? "[Discontinued]" : "")}";
            Console.WriteLine(msg);
            Console.WriteLine(productDescription);
        }

        static char GetInput()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine("".PadLeft(10, '-')); 
                Console.WriteLine("A)dd Product");
                Console.WriteLine("L)ist Products");
                Console.WriteLine("Q)uit");

                var input = Console.ReadLine().Trim();      // var is type infrancing, not Java or other, no type conversion

                //Option 1 - string literal 
                // if (input != "")

                //Option 2 - string field
                //if (input != String.Empty)

                //Option 3 - length
                if (input != null && input.Length != 0)
                {
                    //String comparison
                    if (String.Compare(input, "A", true) == 0)
                        return 'A'; 
                    
                    //char comparison
                    char letter = Char.ToUpper(input[0]);
                    if (letter == 'A')
                        return 'A';
                    else if (letter == 'L')
                        return 'L';
                    else if (letter == 'Q')
                        return 'Q';
                }
                
                //Error Case
                Console.WriteLine("Please choose a valid option.");
            }
        }

        // A single line comment 
        static void Main2( string[] args )
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

            //String Formatting - John worked 10 hours

            //Option 1
            string format1 = name + "worked" + hours.ToString() + " hours";

            //Option 2
            string format2 = String.Format("{0} work for {1} hours", name, hours);

            //Option 3
            string format3 = $"{name} work for {hours} hours";

            // Value Type 
            int value1 = 10;
            Program program = new Program();

            var areEqual1 = value1 == 10;
            var areEqual2 = program == program;
            var areEqual3 = program == new Program(); // this is C#
        }

        /// <summary>Read a decimal from console</summary>
        /// <returns>The decimal value.</returns>
        static decimal ReadDecimal() // for other data types just change the type of the function and the variables inside the function
        {
            do
            {
                string input = Console.ReadLine();

                //decimal result;
                if (Decimal.TryParse(input, out decimal result))
                    return result;
            } while (true);
        }

        /// <summary>Read a bool from console</summary>
        /// <returns>The bool value.</returns>
        static bool ReadYesNO() 
        {
            do
            {
                string input = Console.ReadLine();

                if (!String.IsNullOrEmpty(input))
                {
                    switch (Char.ToUpper(input[0]))
                    {
                        case 'Y': return true;
                        case 'N': return false; 
                    }
                }
                Console.WriteLine("Enter either Y or N");
            } while (true);
        }
 
        static string ReadString ( string errorMessage, bool allowEmpty ) 
        {
            // if (errorMessage == null)
            //   errorMessage = "Enter a valid string"; 
            
            // null coalease
            errorMessage = errorMessage ?? "Enter a valid string";

            // null conditional 
            errorMessage = errorMessage?.Trim(); 

            do
            {
                string input = Console.ReadLine();
                if (String.IsNullOrEmpty(input) && allowEmpty)
                    return "";
                else if (!String.IsNullOrEmpty(input))
                    return input;

                Console.WriteLine(errorMessage);
            } while (true);
        }
        //Product
        static string productName;
        static decimal productPrice;
        static string productDescription;
        static bool productDiscontinued; 
    }
}