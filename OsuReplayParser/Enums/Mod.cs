namespace OsuReplayParser.Enums
{
    public enum ModType
    {
        None = 0,
        NoFail = 1,
        Easy = 2,
        TouchDevice = 4,
        Hidden = 8,
        HardRock = 16,
        SuddenDeath = 32,
        DoubleTime = 64,
        Relax = 128,
        HalfTime = 256,
        Nightcore = 512, // Note DT is applied whenever NC is applied
        Flashlight = 1024,
        Autoplay = 2048,
        SpunOut = 4096,
        Relax2 = 8192,  // Autopilot
        Perfect = 16384,
        Key4 = 32768,
        Key5 = 65536,
        Key6 = 131072,
        Key7 = 262144,
        Key8 = 524288,
        keyMod = 1015808, // k4+k5+k6+k7+k8
        FadeIn = 1048576,
        Random = 2097152,
        LastMod = 4194304, // Cinema
        TargetPractice = 8388608,
        Key9 = 16777216,
        Coop = 33554432,
        Key1 = 67108864,
        Key3 = 134217728,
        Key2 = 268435456,
        ScoreV2 = 536870912,
        Mirror = 1073741824
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
                default:
                    return string.Empty;
            }
        }

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
