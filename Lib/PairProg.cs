using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class PairProg
    {
        public static List<string> PairProgramming(params string[] args)
        {
            int numCount = Convert.ToInt32(args[0]);
            Dictionary<int, int> resultPairs = new Dictionary<int, int>();
            string[] strings = args[1].Split(' ');
            int[] vs = new int[numCount];
            for (int j = 0; j < numCount; j++)
            {
                vs[j] = Convert.ToInt32(strings[j]);
            }
            for (int j = 0; j < numCount; j++)
            {
                if (vs[j] < 0)
                    continue;

                int maxDif = 100;
                int pair2 = 0;
                for (int k = j + 1; k < numCount; k++)
                {
                    if (vs[k] < 0)
                        continue;
                    int currentDif = Math.Abs(vs[j] - vs[k]);
                    if (currentDif < maxDif)
                    {
                        maxDif = currentDif;
                        pair2 = k;
                    }
                }
                resultPairs.Add(j + 1, pair2 + 1);
                vs[pair2] = -1;
            }
            List<string> list = new List<string>();
            foreach (KeyValuePair<int, int> pair in resultPairs)
            {
                list.Add($"{pair.Key} {pair.Value}");
            }
            return list;

        }
    }
}
