using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuReplayParser.Enums
{
   public enum Keypress
    {
        M1 = (1 << 0),
        M2 = (1 << 1),
        K1 = (1 << 2) | (1 << 0),
        K2 = (1 << 3) | (1 << 1),
        Smoke = (1 << 4)
    }

    public static class Keys
    {
        /// <summary>
        /// Returns a list of keys pressed.
        /// </summary>
        /// <param name="encoding">Encoding from a replay frame.param>
        /// <returns>Keys pressed as a list.</returns>
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
