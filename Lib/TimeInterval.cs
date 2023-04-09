using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Interval
    {
        public Interval(DateTime start, DateTime end)
        {
            StartDate = start;
            EndDate = end;
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class IntervalComparer : IComparer<Interval>
    {
        public int Compare(Interval x, Interval y)
        {
            return x.StartDate.CompareTo(y.StartDate);
        }
    }
    public class TimeInterval
    {
        public static string Calculate(List<string> lines)
        {
            bool isYes = true;
            List<Interval> intervals = new List<Interval>();
            for (int j = 0; j < lines.Count; j++)
            {
                string[] moments = lines[j].Split('-');
                string[] start = moments[0].Split(':');
                string[] end = moments[1].Split(':');
                int[] startNum = new int[start.Length];
                int[] endNum = new int[end.Length];

                for (int k = 0; k < start.Length; k++)
                {
                    int startPart = Convert.ToInt32(start[k]);
                    startNum[k] = startPart;
                    int endPart = Convert.ToInt32(end[k]);
                    endNum[k] = endPart;

                    if (k == 0)
                    {
                        if (!(startPart >= 0 && startPart <= 23 && endPart >= 0 && endPart <= 23))
                        {
                            isYes = false;
                            break;
                        }
                    }
                    else
                    {
                        if (!(startPart >= 0 && startPart <= 59 && endPart >= 0 && endPart <= 59))
                        {
                            isYes = false;
                            break;
                        }
                    }
                }

                for (int k = 0; k < startNum.Length; k++)
                {
                    if (startNum[k] == endNum[k])
                        continue;
                    if (startNum[k] < endNum[k])
                        break;
                    if (startNum[k] > endNum[k])
                    {
                        isYes = false;
                        break;
                    }
                }
                if (isYes)
                {
                    DateTime dateStart = new DateTime(1, 1, 1, startNum[0], startNum[1], startNum[2]);
                    DateTime dateEnd = new DateTime(1, 1, 1, endNum[0], endNum[1], endNum[2]);
                    intervals.Add(new Interval(dateStart, dateEnd));
                }
            }
            if (isYes || intervals.Count > 1)
            {
                intervals.Sort(new IntervalComparer());
                for (int k = 1; k < intervals.Count; k++)
                {
                    if (intervals[k - 1].EndDate >= intervals[k].StartDate)
                    {
                        isYes = false;
                        break;
                    }
                }
            }

            return isYes ? "YES" : "NO";
        }
    }
}
