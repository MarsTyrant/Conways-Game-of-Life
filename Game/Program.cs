﻿using System;

namespace GameOfLife
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool continueDisplay = true;

            while (continueDisplay)
            {
                Console.WriteLine("\nConway's Game of Life");
                Console.WriteLine("1. Create a new random grid");
                Console.WriteLine("2. Load grid from JSON file");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                var option = Console.ReadLine();
                JsonStorage json=new();
                switch (option)
                {
                    case "1":
                        Console.WriteLine("\nNo");
                        json.StoreGrid();
                        // ask user for rows (4-100)
                        // ask user for columns (4-100)
                        // init grid
                        break;
                    case "2":
                        JsonStorage.filePath ="Grids\\grid.json";
                        json.LoadGrid();
                        break;
                    case "3":
                        Console.WriteLine("\nExiting...");
                        continueDisplay = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please try again.");
                        break;
                }
            }
        }
    }
}