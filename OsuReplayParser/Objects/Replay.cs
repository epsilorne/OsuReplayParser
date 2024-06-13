using OsuReplayParser.Enums;

namespace OsuReplayParser.Objects
{
    public class Replay
    {
        public Ruleset Ruleset { get; set; }
        public int GameVersion { get; set; }
        public string BeatmapHash { get; set; }
        public string PlayerName { get; set; }
        public string ReplayHash { get; set; }

        public short Count300s { get; set; }
        public short Count100s { get; set; }
        public short Count50s { get; set; }
        public short Gekis { get; set; }
        public short Katus { get; set; }
        public short Misses { get; set; }

        public int TotalScore { get; set; }
        public short MaxCombo { get; set; }
        public bool PerfectFullCombo { get; set; }
        public List<ModType> ModsUsed { get; set; }

        public string LifebarGraph { get; set; }
        public DateTime DateSet { get; set; }

        public int ReplayDataLength { get; set; }
        public byte[] CompressedReplayData { get; set; }

        public long OnlineScoreID { get; set; }
        public double AdditionalModInfo { get; set; }
    }
}
