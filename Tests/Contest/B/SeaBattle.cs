using System;
using System.Collections.Generic;

namespace Tests.Contest
{
    internal class SeaBattle
    {
        internal static List<string> Calculate(List<string> inputs)
        {
            List<string> output = new List<string>();

            Dictionary<int, int> ships = new Dictionary<int, int>();
            foreach (string i in inputs)
            {
                ships = new Dictionary<int, int>() { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 } };
                string[] input = i.Split(' ');
                foreach (string s in input)
                {
                    int ship = Convert.ToInt32(s);
                    ships[ship]++;
                }
                if (ships[1] > 4 || ships[2] > 3 || ships[3] > 2 || ships[4] > 1)
                    output.Add("NO");
                else
                    output.Add("YES");
            }

            return output;
        }
    }
}