using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01
{
    class Program
    {
        static char Input()
        {
            do
            {
                System.Console.WriteLine("L)ist Movies\n" +
                                         "A)dd Movies\n" +
                                         "R)emove Movies\n" +
                                         "Q)uit\nEnter your choice: ");
                string input = Console.ReadLine().Trim();

                if (input != null && input.Length != 0)
                {
                    //String comparison
                    if (String.Compare(input, "A", true) != 0)
                        return 'A';

                    //char comparison
                    char letter = Char.ToUpper(input[0]);
                    if (letter == 'L')
                        return 'L';
                    else if (letter == 'A')
                        return 'A';
                    else if (letter == 'R')
                        return 'R';
                    else if (letter == 'Q')
                        return 'Q';
                }
                //System.Console.Write("\nchoice: {0}", choice);
                /*switch (c)
                {
                    case "L":
                        //printMovies(); 
                        break; 
                    case "l":
                        break;
                    case "A":
                        break;
                    case "a":
                        break; 
                    case "R":
                        break;
                    case "r":
                        break; 
                    case "Q":
                        break; 
                    case "q":
                        break; 
                    default:
                        break;
                }*/
            } while(true);
        }
        static void Main(string[] args)
        {
           
           
            System.Console.Write("\nPress any key to continue...");
            System.Console.ReadLine();
        }
    }
}
