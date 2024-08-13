using System.Text.RegularExpressions;

namespace Turnit_Test_2024;

public static class BusiestTimeFinder
{
    
    /// <summary>
    /// Use sweep line algorithm to find the busiest interval of time in the bus schedule.
    /// </summary>
    /// <param name="events">List of events (2D array of [time: int][+1 / -1])</param>
    /// <returns></returns>
    public static (string, int) FindBusiestTime(int[][] events)
    {
        // Sweep line algorithm - sort events by start time and iterate through them, keeping track of the current overlap.
        // An event is either a start or end, so +1 overlap when bus driver starting break, and -1 overlap when bus driver ending break.
        Array.Sort(events, (a, b) => a[0].CompareTo(b[0]));
        
        var maxTotalOverlap = 0;
        var currentOverlap = 0;
        var maxStartTime = -1;
        var maxEndTime = -1;

        for (var e = 0; e < events.Length; e++)
        {
            // tempOverlap used because both start and end events are included in the same interval
            var tempOverlap = currentOverlap;
            if (events[e][1] > 0) { tempOverlap += 1; }
            if (tempOverlap > maxTotalOverlap)
            {
                maxTotalOverlap = currentOverlap;
                maxEndTime = events[e][0];
            }
            currentOverlap += events[e][1];
        }

        // Once you reach first element with max overlap, you can be sure that that is the start time of the busiest time
        currentOverlap = 0;
        for (var e = 0; e < events.Length; e++)
        {
            currentOverlap += events[e][1];
            if (currentOverlap == maxTotalOverlap)
            {
                maxStartTime = events[e][0];
                break;
            }
        }
        
        string formattedStartTime = $"{(maxStartTime / 100):D2}:{(maxStartTime % 100):D2}";
        string formattedEndTime = $"{(maxEndTime / 100):D2}:{(maxEndTime % 100):D2}";

        return ($"{formattedStartTime}-{formattedEndTime}", maxTotalOverlap);
    }
    
    /// <summary>
    /// Convert time input from hh:mmhh::mm format to 2d int array of events (time, +1/-1 for start/end)
    /// </summary>
    /// <param name="input">String input</param>
    /// <returns></returns>
    public static int[][] ConvertTimeInput(string input)
    {
        // regex pattern for hh:mmhh::mm\n
        var pattern = @"\d{2}:\d{2}\d{2}:\d{2}(\\n)?";
        
        var eventStrings = input.Split('\n');
        foreach (var line in eventStrings)
        {
            if (!Regex.IsMatch(line, pattern))
            {
                throw new ArgumentException("Invalid input: " + line);
            }
        }
        
        var events = new List<int[]>();
        for (var i = 0; i < eventStrings.Length; i++)
        {
            var startTime = int.Parse(eventStrings[i].Substring(0, 5).Replace(":", ""));
            var endTime = int.Parse(eventStrings[i].Substring(5, 5).Replace(":", ""));
            
            events.Add(new int[] {startTime, 1});
            events.Add(new int[] {endTime, -1});
        }
        
        return events.ToArray();
    }
}