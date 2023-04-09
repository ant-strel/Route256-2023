using System;
using System.Collections.Generic;

namespace Lib
{
    public class Report
    {
        public static string CreateReport(params string[] args)
        {
            int valuesCount = Convert.ToInt32(args[0]);

            string[] values = args[1].Split(' '); // report
            bool isYes = true;
            Dictionary<string, bool> valuesDictionary = new Dictionary<string, bool>();
            valuesDictionary.Add(values[0], false);
            for (int j = 1; j < valuesCount; j++)
            {
                if (valuesDictionary.ContainsKey(values[j]))
                {
                    if (values[j] == values[j - 1])
                        continue;
                    else
                    {
                        isYes = false;
                        break;
                    }
                }
                else
                {
                    valuesDictionary.Add(values[j], false);
                }
            }
            return isYes ? "YES" : "NO";
        }
    }
}
