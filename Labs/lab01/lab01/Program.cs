/*
 * Name: Mohamed Rahaman
 * Class: ITSE 1430 20630
 * Date: September 18, 2017
 * Lab: 01
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01
{
    class Program
    {
        static string movieTitle = "";
        static string movieDescription = "";
        static int movieRuntime = 0;
        static bool movieOwned = false;
        
        static char GetInput()
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine("".PadLeft(15, '-'));
                Console.WriteLine("L)ist Movies");
                Console.WriteLine("A)dd Movies");
                Console.WriteLine("R)emove Movies");
                Console.WriteLine("Q)uit");
                Console.WriteLine("".PadLeft(15, '-'));
                Console.Write("Your Choice: ");

                string input = Console.ReadLine().Trim();

                if (input != null && input.Length != 0)
                {
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

                Console.WriteLine("Please choose a valid option.");
            } while (true);
        }
        static void Main(string[] args)
        {
            bool quit = false;
            do
            {
                char choice = GetInput();
                switch (choice)
                {
                    case 'l':
                    case 'L': ListMovies(); break;
                    case 'a':
                    case 'A': AddMovies(); break;
                    case 'r':
                    case 'R': RemoveMovies(); break;
                    case 'q':
                    case 'Q': quit = true; break;                         
                };

            } while (!quit);
        }

        private static void RemoveMovies()
        {
            Console.WriteLine("".PadLeft(15, '-'));
            Console.Write("Are you sure you want to delete the movie? (Y/N): ");
            bool removeStatus = ReadYesNo();

            if (removeStatus)
            {
                movieTitle = "";
                movieDescription = "";
                movieRuntime = 0;
                movieOwned = false;
            }
            Console.WriteLine();
        }

        private static void AddMovies()
        {
            Console.WriteLine("".PadLeft(15, '-'));

            do
            {
                Console.Write("Enter a title: ");
                movieTitle = Console.ReadLine().Trim();

                if (String.IsNullOrEmpty(movieTitle))
                    Console.WriteLine("You must enter a value");

            } while (String.IsNullOrEmpty(movieTitle));
            
           
            Console.Write("Enter a description [Optional]: ");
            movieDescription = Console.ReadLine();

            do
            {
                Console.Write("Enter the runtime in minutes [Optional]: ");
                movieRuntime = ReadInteger();

                if (!(movieRuntime >= 0))
                    Console.WriteLine("\tYou must enter a value >= 0");
                
            } while (!(movieRuntime >= 0));

            Console.Write("Do you own this movie? (Y/N): ");
            movieOwned = ReadYesNo();
            Console.WriteLine();
        }

        private static void ListMovies()
        {
            Console.WriteLine("".PadLeft(15, '-'));

            if (String.IsNullOrEmpty(movieTitle))
                Console.WriteLine("NO MOVIES AVAILABLE");
            else
            {
                string result = $"Movie Title = {movieTitle}\nDescription = {movieDescription}\n" +
                                $"Run Time = {movieRuntime}\nStatus = {(movieOwned ? "Owned" : "Not Owned")}";
                Console.WriteLine(result);
            }
            Console.WriteLine();
        }

        static int ReadInteger()
        {
            do
            {
                string input = Console.ReadLine();

                // integer result
                if (Int32.TryParse(input, out int result))
                    return result;

                Console.Write("Enter a valid integer: ");

            } while (true);
        }

        static bool ReadYesNo()
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
                    Console.Write("Enter either Y or N: ");
                }
            } while (true);
        }
    }
}
