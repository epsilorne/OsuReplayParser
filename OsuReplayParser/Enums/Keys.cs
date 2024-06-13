using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuReplayParser.Enums
{
   public enum Keypress
    {
        M1 = 1,
        M2 = 2,
        K1 = 5,
        K2 = 10,
        Smoke = 16
    }

    public static class Keys
    {
        public static List<Keypress> GetKeysPressed(int encoding)
        {
            List<Keypress> results = new List<Keypress>();

            foreach (Keypress kp in Enum.GetValues(typeof(Keypress)))
            {
                // Bitwise AND to check if key is pressed
                if (((int) kp & encoding) != 0)
                {
                    // Remove mouse buttons when key is pressed
                    if (kp == Keypress.K1 && results.Contains(Keypress.M1))
                    {
                        results.Remove(Keypress.M1);
                    }
                    if (kp == Keypress.K2 && results.Contains(Keypress.M2))
                    {
                        results.Remove(Keypress.M2);
                    }
                    results.Add(kp);
                }
            }

            return results;
        }
    }
}
