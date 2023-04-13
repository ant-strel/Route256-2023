using System;
using System.Collections.Generic;

namespace Tests.Contest
{
    internal class AutoNums
    {
        internal static List<string> Calculate(List<string> inputs)
        {
            List<string> output = new List<string>();

            Dictionary<int, int> ships = new Dictionary<int, int>();
            foreach (string s in inputs)
            {
                List<string> o = new List<string>();
                bool isCorrect = true;
                int firstKey = 0;
                while (firstKey < s.Length)
                {
                    if (s.Length >= firstKey + 5)
                    {
                        string longS = s.Substring(firstKey, 5);
                        if (IsLongCorrect(longS))
                        {
                            o.Add(longS);
                            firstKey += 5;
                            continue;
                        }
                    }
                    if (s.Length >= firstKey + 4)
                    {
                        string shortS = s.Substring(firstKey, 4);
                        if (IsShortCorrect(shortS))
                        {
                            o.Add(shortS);
                            firstKey += 4;
                            continue;
                        }
                        else
                        {
                            isCorrect = false;
                            break;
                        }
                    }
                    else
                    {
                        isCorrect = false;
                        break;
                    }
                }
                string k = "";
                for (int i = 0; i < o.Count; i++)
                {
                    string space = i + 1 < o.Count ? " " : "";
                    k += o[i] + space;
                    ;
                }
                output.Add(isCorrect ? k : "-");

            }
            return output;
        }

        private static bool IsShortCorrect(string shortS)
        {
            return
            Char.IsLetter(shortS[0]) &&
            Char.IsDigit(shortS[1]) &&
            Char.IsLetter(shortS[2]) &&
            Char.IsLetter(shortS[3]);
        }

        private static bool IsLongCorrect(string longS)
        {
            return
            Char.IsLetter(longS[0]) &&
            Char.IsDigit(longS[1]) &&
            Char.IsDigit(longS[2]) &&
            Char.IsLetter(longS[3]) &&
            Char.IsLetter(longS[4]);
        }

    }
}