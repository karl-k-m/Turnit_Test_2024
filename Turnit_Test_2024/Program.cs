using System.Text.RegularExpressions;

namespace Turnit_Test_2024;

public class Program
{
    public void RunCliLoop(List<string>? presetEvents)
    {
        var events = new List<string>();
        string pattern = @"^\d{2}:\d{2} \d{2}:\d{2}$"; // regex pattern for hh:mm hh:mm
        
        // Manual input
        if (presetEvents != null)
        {
            Console.WriteLine("You may add further times:");
            events.AddRange(presetEvents);
        }
        Console.WriteLine("Enter the bus drivers' breaks in the format 'hh:mm hh::mm' (e.g. '09:00 09:30')");
        Console.WriteLine("Enter 'exit' to quit.");
        while (true)
        {
            var input = Console.ReadLine();
            if (input == "exit") { break; }
            if (!Regex.IsMatch(input, pattern))
            {
                Console.WriteLine("Invalid input. Please enter the time in the format 'hh:mm hh:mm'.");
                continue;
            }
            events.Add(input.Replace(" ", ""));
            var eventsArray = BusiestTimeFinder.ConvertTimeInput(string.Join("\n", events));
            var result = BusiestTimeFinder.FindBusiestTime(eventsArray);
            Console.WriteLine("Busiest time: " + result.Item1 + " Number of drivers taking a break: " + result.Item2);
        }
    }
    
    public static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            if (args[0] != "-filepath" || args.Length != 2)
            {
                Console.WriteLine("Invalid arguments. Use -filepath <path> to specify a file path.");
            } else
            {
                try
                {
                    var events = File.ReadAllText(args[1]).Trim();
                    var eventsArray = BusiestTimeFinder.ConvertTimeInput(events);
                    var result = BusiestTimeFinder.FindBusiestTime(eventsArray);
                    Console.WriteLine("Busiest time: " + result.Item1);
                    Console.WriteLine("Number of buses: " + result.Item2);
                    new Program().RunCliLoop(events.Split('\n').ToList());
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("File not found. Error: " + e.Message);
                }
            }
        } else
        {
            new Program().RunCliLoop(null);
        }
    }
}