using Turnit_Test_2024;

namespace Turnit_Test_2024_test;

public class BusiestTimeFinderTest
{
    [Test (Description = "FindBusiestTime algo with 1 event returns the same interval as the event")]
    public void TestFindBusiestTimeWithOneEvent()
    {
        var events = new int[][] { new int[] { 1000, 1 }, new int[] { 1100, -1 } };
        var result = BusiestTimeFinder.FindBusiestTime(events);
        Assert.That(("10:00-11:00", 1), Is.EqualTo(result));
    }
    
    [Test (Description = "FindBusiestTime algo with 5 events returns the correct interval")]
    public void TestFindBusiestTimeWithFiveEvents()
    {
        /*
         10:30-11:35
        10:15-11:15
        11:20-11:50
        10:35-11:40
        10:20-11:20
        */
        var events = new int[][] { new int[] { 1030, 1 }, new int[] { 1135, -1 },
                                   new int[] { 1015, 1 }, new int[] { 1115, -1 },
                                   new int[] { 1120, 1 }, new int[] { 1150, -1 },
                                   new int[] { 1035, 1 }, new int[] { 1140, -1 },
                                   new int[] { 1020, 1 }, new int[] { 1120, -1 } };
        var result = BusiestTimeFinder.FindBusiestTime(events);
        Assert.That(("10:35-11:15", 4), Is.EqualTo(result));
    }
    
    [Test (Description = "FindBusiestTime algo with 4 events returns the correct interval")]
    public void TestFindBusiestTimeWithFourEvents()
    {
        /*
        10:00-13:00
        10:00-11:00
        11:00-13:00
        12:00-13:00
        */
        var events = new int[][] { new int[] { 1000, 1 }, new int[] { 1300, -1 },
                                   new int[] { 1000, 1 }, new int[] { 1100, -1 },
                                   new int[] { 1100, 1 }, new int[] { 1300, -1 },
                                   new int[] { 1200, 1 }, new int[] { 1300, -1 } };
        var result = BusiestTimeFinder.FindBusiestTime(events);
        Assert.That(("12:00-13:00", 3), Is.EqualTo(result));
    }
    
    [Test (Description = "FindBusiestTime algo with 2 non-intersecting events returns the first found interval")]
    public void TestFindBusiestTimeWithTwoNonIntersectingEvents()
    {
        var events = new int[][] { new int[] { 1000, 1 }, new int[] { 1100, -1 },
                                   new int[] { 1200, 1 }, new int[] { 1300, -1 } };
        var result = BusiestTimeFinder.FindBusiestTime(events);
        Assert.That(("10:00-11:00", 1), Is.EqualTo(result));
    }
    
    [Test (Description = "ConvertTimeInput correctly throws exception for invalid input")]
    public void TestConvertTimeInputWithInvalidInput()
    {
        Assert.Throws<ArgumentException>(() => BusiestTimeFinder.ConvertTimeInput("10:00 1:00"));
    }
}