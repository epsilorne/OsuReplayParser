namespace OsuReplayParser.Enums
{
    public enum ModType
    {
        None = 0,
        NoFail = (1 << 0),
        Easy = (1 << 1),
        TouchDevice = (1 << 2),
        Hidden = (1 << 3),
        HardRock = (1 << 4),
        SuddenDeath = (1 << 5),
        DoubleTime = (1 << 6),
        Relax = (1 << 7),
        HalfTime = (1 << 8),
        Nightcore = (1 << 9), // Note DT is applied whenever NC is applied
        Flashlight = (1 << 10),
        Autoplay = (1 << 11),
        SpunOut = (1 << 12),
        Relax2 = (1 << 13),  // Autopilot
        Perfect = (1 << 14),
        Key4 = (1 << 15),
        Key5 = (1 << 16),
        Key6 = (1 << 17),
        Key7 = (1 << 18),
        Key8 = (1 << 19),
        keyMod = (1 << 20), // k4+k5+k6+k7+k8
        FadeIn = (1 << 21),
        Random = (1 << 22),
        LastMod = (1 << 23), // Cinema
        TargetPractice = (1 << 24),
        Key9 = (1 << 25),
        Coop = (1 << 26),
        Key1 = (1 << 27),
        Key3 = (1 << 28),
        Key2 = (1 << 29),
        ScoreV2 = (1 << 30),
        Mirror = (1 << 31)
    }

    public static class Mod
    {
        /// <summary>
        /// Get the list of mods used for a replay.
        /// </summary>
        /// <param name="encoding">Encoding of mods used from an .osr file.</param>
        /// <returns>A list of the mods used.</returns>
        public static List<ModType> GetModsList(int encoding)
        {
            List<ModType> results = new List<ModType>();

            foreach (ModType currentMod in Enum.GetValues(typeof(ModType)))
            {
                // Bitwise AND to check if mod is applied
                if (((int) currentMod & encoding) != 0)
                {
                    // Remove DT if NC is used
                    if (currentMod == ModType.Nightcore && results.Contains(ModType.DoubleTime))
                    {
                        results.Remove(ModType.DoubleTime);
                    }
                    results.Add(currentMod);
                }
            }

            return results;
        }
        
        /// <summary>
        /// Get the abreviation of a ModType.
        /// </summary>
        /// <param name="type">ModType.</param>
        /// <returns>The ModType's abbreviation.</returns>
        public static string ToStringAbbrev(this ModType type)
        {
            switch (type)
            {
                case ModType.None:
                    return string.Empty;
                case ModType.NoFail:
                    return "NF";
                case ModType.Easy:
                    return "EZ";
                case ModType.TouchDevice:
                    return "TD";
                case ModType.Hidden:
                    return "HD";
                case ModType.HardRock:
                    return "HR";
                case ModType.SuddenDeath:
                    return "SD";
                case ModType.DoubleTime:
                    return "DT";
                case ModType.Relax:
                    return "RX";
                case ModType.HalfTime:
                    return "HT";
                case ModType.Nightcore:
                    return "NC";
                case ModType.Flashlight:
                    return "FL";
                case ModType.Autoplay:
                    return "AT";
                case ModType.SpunOut:
                    return "SO";
                case ModType.Relax2:
                    return "AP";
                case ModType.Perfect:
                    return "PF";
                case ModType.Key4:
                    return "4K";
                case ModType.Key5:
                    return "5K";
                case ModType.Key6:
                    return "6K";
                case ModType.Key7:
                    return "7K";
                case ModType.Key8:
                    return "8K";
                case ModType.keyMod:
                    return "xK"; // Not sure what this means, I don't play mania haha
                case ModType.FadeIn:
                    return "FI";
                case ModType.Random:
                    return "RD";
                case ModType.LastMod:
                    return "CM";
                case ModType.TargetPractice:
                    return "TP";
                case ModType.Key9:
                    return "9K";
                case ModType.Coop:
                    return "CP";
                case ModType.Key1:
                    return "1K";
                case ModType.Key2:
                    return "2K";
                case ModType.Key3:
                    return "3K";
                case ModType.ScoreV2:
                    return "SV2";
                case ModType.Mirror:
                    return "MR";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Gets the abbreviations of all mods used for a replay.
        /// </summary>
        /// <param name="modsUsed">List of mods used.</param>
        /// <returns>All their abbrevivations as they'd appear in game.</returns>
        public static string ToModsAbbrev(this List<ModType> modsUsed)
        {
            string result = string.Empty;

            foreach (ModType modType in modsUsed)
            {
                result += modType.ToStringAbbrev();
            }

            return result;
        }
    }
}
