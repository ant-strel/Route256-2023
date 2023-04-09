using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Payment
    {
        public static string SumGoods(params string[] args)
        {
            int numCount = Convert.ToInt32(args[0]);
            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
            string[] goodsStrings = args[1].Split(' ');
            for (int j = 0; j < numCount; j++)
            {
                int value = Convert.ToInt32(goodsStrings[j]);
                if (keyValuePairs.ContainsKey(value))
                    keyValuePairs[value]++;
                else
                    keyValuePairs.Add(value, 1);
            }
            int sum = 0;
            foreach (KeyValuePair<int, int> pair in keyValuePairs)
            {
                int sets = pair.Value / 3;
                int rem = pair.Value % 3;
                sum += (sets * 2 * pair.Key) + (rem * pair.Key);
            }
            return $"{sum}";
        }

    }
}
