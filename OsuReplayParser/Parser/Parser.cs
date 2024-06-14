using OsuReplayParser.Enums;
using OsuReplayParser.Objects;
using System.Text;

namespace OsuReplayParser.Parser
{
    public static class Parser
    {
        /// <summary>
        /// Parse .osr file.
        /// </summary>
        /// <param name="filePath">File path of the replay.</param>
        /// <returns>Replay object.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static Replay ParseReplay(String filePath)
        {
            if (File.Exists(filePath))
            {
                Replay replay = new Replay();

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

                            // Life bar parsing
                            replay.LifeBarGraph = new List<LifeBarEntry>();
                            string[] lifebarEntries = reader.ReadOsrString().Split(',');
                            foreach(string entry in lifebarEntries)
                            {
                                if(entry == "")
                                {
                                    break;
                                }

                                string[] values = entry.Split("|");
                                int time = Convert.ToInt32(values[0]);
                                float health = (float) Convert.ToDouble(values[1]);
                                replay.LifeBarGraph.Add(new LifeBarEntry(time, health));
                            }

                            replay.DateSet = new DateTime(reader.ReadInt64(), DateTimeKind.Local);

                            replay.ReplayDataLength = reader.ReadInt32();

                            // LZMA Decompression
                            replay.Frames = new List<ReplayFrame>();
                            byte[] rawReplayData = reader.ReadBytes(replay.ReplayDataLength);
                            byte[] decompressedData = Util.Decompress(rawReplayData);
                            string[] frames = Encoding.ASCII.GetString(decompressedData).Split(',');
                            long currentMs = 0;

                            for (int i = 0; i < frames.Length; i++)
                            {
                                string[] frameInfo = frames[i].Split('|');
                                
                                long prevMs = (long) Convert.ToDouble(frameInfo[0]);
                                float currentX = (float) Convert.ToDouble(frameInfo[1]);
                                float currentY = (float) Convert.ToDouble(frameInfo[2]);
                                int keyPresses = Convert.ToInt32(frameInfo[3]);

                                // Send the replay seed if we get -12345|0|0|seed
                                if (frameInfo[0] == "-12345")
                                {
                                    replay.Seed = Convert.ToInt32(frameInfo[3]);
                                    break;
                                }

                                ReplayFrame rf = new ReplayFrame(currentMs, prevMs, currentX, currentY, keyPresses);
                                replay.Frames.Add(rf);

                                currentMs += prevMs;
                            }

                            replay.OnlineScoreID = reader.ReadInt64();
                            if (replay.ModsUsed.Contains(ModType.TargetPractice))
                            {
                                replay.AdditionalModInfo = reader.ReadDouble();
                            }
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
