using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Contest
{
    internal class DocAppoint
    {
        internal static string Calculate(string first, string second)
        {
            string result = "";
            int winCount = first.Split(' ').Select(x=>int.Parse(x)).First();
            int patients = first.Split(' ').Select(x=>int.Parse(x)).Last();

            List<int> records = second.Split(' ').Select(x=>int.Parse(x)).ToList();




            return result;

        }
    }
}