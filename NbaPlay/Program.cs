using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        const int CurrentYear = 2019;

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the path to the JSON file that contains the list of basketball players.");
            string filePath = Console.ReadLine();
            Console.WriteLine("Please enter the maximum number of years the player has played in the league to qualify.");
            uint playingSince = uint.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the minimum rating the player should have to qualify.");
            uint minRating = uint.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the path to file containing information about the basketball players.");
            string outputFilePath = Console.ReadLine();

            string fileData = File.ReadAllText(filePath);
            IEnumerable<Player> players = JsonConvert.DeserializeObject<IEnumerable<Player>>(fileData);

            IEnumerable<string> playersCsv = players
                .Where(p => p.Rating > minRating && playingSince > CurrentYear - p.PlayingSince)
                .OrderByDescending(p => p.Rating)
                .Select(p => $"{p.Name}, {p.Rating}")
                .Prepend("Name, Rating");

            File.WriteAllLines(outputFilePath, playersCsv);
        }
    }
}
