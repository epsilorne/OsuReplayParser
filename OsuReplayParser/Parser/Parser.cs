using OsuReplayParser.Enums;
using OsuReplayParser.Objects;

namespace OsuReplayParser.Parser
{
    class Parser
    {
        /// <summary>
        /// Parse .osr file.
        /// </summary>
        /// <param name="filePath">File path of the replay.</param>
        /// <returns>Replay object.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Replay ParseReplay(String filePath)
        {
            if (File.Exists(filePath))
            {
                Replay replay = new Replay();

                // TODO: parse rest of file (including replay data)
                using(var stream = File.Open(filePath, FileMode.Open))
                {
                    using(var reader = new BinaryReader(stream))
                    {
                        try
                        {
                            replay.Ruleset = (Ruleset) reader.ReadByte();
                            replay.GameVersion = reader.ReadInt32();
                            replay.BeatmapHash = reader.ReadOsrString();
                            replay.PlayerName = reader.ReadOsrString();
                            replay.ReplayHash = reader.ReadOsrString();

                            replay.Count300s = reader.ReadInt16();
                            replay.Count100s = reader.ReadInt16();
                            replay.Count50s = reader.ReadInt16();
                            replay.Gekis = reader.ReadInt16();
                            replay.Katus = reader.ReadInt16();
                            replay.Misses = reader.ReadInt16();

                            replay.TotalScore = reader.ReadInt32();
                            replay.MaxCombo = reader.ReadInt16();
                            replay.PerfectFullCombo = reader.ReadByte() == 1;
                            replay.ModsUsed = Mod.GetModsList(reader.ReadInt32());

                            replay.LifebarGraph = reader.ReadOsrString();
                            replay.DateSet = new DateTime(reader.ReadInt64(), DateTimeKind.Local);

                            replay.ReplayDataLength = reader.ReadInt32();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Exception when attempting to parse replay: " + ex);
                        }
                    }
                }

                return replay;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}
