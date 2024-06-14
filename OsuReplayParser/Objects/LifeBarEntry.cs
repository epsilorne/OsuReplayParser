namespace OsuReplayParser.Objects
{
    public class LifeBarEntry
    {
        public int Time { get; set; }
        public float Health { get; set; }

        public LifeBarEntry(int time, float health)
        {
            this.Time = time;
            this.Health = health;
        }

        public override string ToString()
        {
            return "[Time: " + Time + ", HP: " + Health + "]";
        }
    }
}
