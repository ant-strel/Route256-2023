using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Contest
{
    internal class Trademark
    {
        internal static string Calculate(List<string> dict)
        {
            var requests = new HashSet<string>();
            foreach (var req in dict)
            {
                var prev = req[0];
                var sameChars = 1;
                var proceed = new StringBuilder(prev.ToString());
                for (var i = 1; i < req.Length; i++)
                {
                    if (prev == req[i])
                    {
                        sameChars++;
                    }
                    else
                    {
                        sameChars = 1;
                    }

                    prev = req[i];

                    if (sameChars <= 2)
                    {
                        proceed.Append(req[i]);
                    }
                }

                requests.Add(proceed.ToString());
            }



            return requests.Count.ToString();
        }
    }
}