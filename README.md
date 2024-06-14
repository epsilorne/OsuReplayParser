# OsuReplayParser
Basic .osr parser implemented in C#, allowing you to read osu! replay files, with future plans to allow editing of replay files.

**Please note this is a heavy work-in-progress; if you run into any problems, open an issue! :)**

## Dependencies
Written in .NET 8.0, but this library is simple enough it should work fine for earlier versions.

## Usage
Use the `Parser` helper class and `ParseReplay()` method to parse a replay, as shown in `Demo.cs`. You just need to prove the filepath to the .osr file.

```cs
using OsuReplayParser.Enums;
using OsuReplayParser.Objects;
using OsuReplayParser.Parser;

class Demo
{
    public static void Main(string[] args)
    {
        Replay r = Parser.ParseReplay("Demo/sample2.osr");
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
```

## Documentation
TBA