using OsuReplayParser.Enums;
using OsuReplayParser.Objects;
using OsuReplayParser.Parser;
using System.Text;

class Demo
{
    public static void Main(string[] args)
    {
        Parser parser = new Parser();
        Replay r = parser.ParseReplay("Demo/sample2.osr");
        Console.WriteLine("Ruleset: " + r.Ruleset + ", Player: " + r.PlayerName);
        Console.WriteLine("Set on: " + r.DateSet);
        Console.WriteLine("300s: " + r.Count300s + ", 100s: " + r.Count100s + ", 50s: " + r.Count50s + ", Misses: " + r.Misses);
        Console.WriteLine("Mods: " + r.ModsUsed.ToModsAbbrev() + ", Max Combo: " + r.MaxCombo + "x");
        
        foreach(ReplayFrame rf in r.Frames)
        {
            Console.WriteLine(rf);
        }

        Console.ReadLine();
    }
}