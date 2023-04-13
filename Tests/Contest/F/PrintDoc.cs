using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Contest
{
    internal class PrintDoc
    {
        internal static string Calculate(string count, string printed)
        {
            string result = "";
            int pagesCount = int.Parse(count);
            SortedDictionary<int,bool> pages = new SortedDictionary<int, bool>(Enumerable.Range(1, pagesCount).ToDictionary(x=>x, x=>false));
            List<bool> indexedPages = new List<bool>() { false };
            string[] intervals = printed.Split(',');

            foreach (var interval in intervals)
            {
                if(!interval.Contains("-"))
                {
                    pages[int.Parse(interval)]= true;

                }
                else
                {
                    string first = "";
                    string last = "";
                    bool isFirst = true;
                    foreach(var value in interval)
                    {
                        if (value == '-')
                        {
                            isFirst = false;
                            continue;
                        }
                        if(isFirst)
                            first += value;
                        else
                            last += value;
                    }
                    for (int i = int.Parse(first); i <= int.Parse(last);i++)
                    {
                        pages[i] = true;
                    }
                }
            }

            foreach(var page in pages)
            {
                indexedPages.Add(page.Value);
            }

            int lastValue = 0;
            int MaxPage = pages.Max(x=>x.Key);

            foreach(var page in pages)
            {
                if(string.IsNullOrEmpty(result) || result.EndsWith(","))
                {
                    if (page.Value)
                    {
                        continue;
                    }
                    else
                    {
                        if (page.Key == MaxPage)
                            result += page.Key;
                        else
                        {
                            bool isNextReaded = indexedPages[page.Key + 1];
                            if (isNextReaded)
                            {
                                result += $"{page.Key},";                                
                            }
                            else
                            {
                                lastValue = page.Key;
                                result += $"{page.Key}-";
                            }
                        }
                    }
                }
                else if (result.EndsWith("-"))
                {
                    if(page.Value)
                    {
                        result += $"{lastValue},";
                    }
                    else
                    {
                        if(page.Key - lastValue == 1 && page.Key != MaxPage)
                        {
                            lastValue = page.Key;
                        }
                        else
                        {
                            result += $"{page.Key},";
                            lastValue = 0;
                        }
                    }

                }
            }            

            return result.Trim(',');           

        }
    }
}