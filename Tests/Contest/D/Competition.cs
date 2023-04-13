using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tests.Contest
{
    class Position
    {
        public int Place { get; set; }
        public int Count { get; set; }
    }
    internal class Competition
    {
        internal static string Calculate(string playersCount, string results)
        {
            string result = "";
            int pCount = int.Parse(playersCount);
            var res = results.Split(' ').Select(x => int.Parse(x)).ToList();
            SortedDictionary<int, Position> players = new SortedDictionary<int, Position>();

            foreach(var time in res)
            {
                if (players.ContainsKey(time))
                    players[time].Count++;
                else
                    players.Add(time, new Position() { Count = 1 });
            }

            int nextPosition = 1;
            int currentCount = 0;
            
            //for(int i = 0; i < players.Count; i++)
            int min = players.Min(x=>x.Key);
            KeyValuePair<int, Position> previous = players.ElementAt(0);
            foreach(var p in players)
            {
                var current = p;
                if (p.Key == min)
                {
                    current.Value.Place = nextPosition;
                }
                else
                {
                    var prev = previous;
                    if (current.Key - prev.Key == 1)
                        current.Value.Place = nextPosition;
                    else
                    {
                        nextPosition += currentCount;
                        currentCount = 0;
                        current.Value.Place = nextPosition;
                    }

                }
                currentCount += current.Value.Count;
                previous = current;

            }


            List<int> output = new List<int>();
            foreach(var p in res)
            {
                output.Add(players[p].Place);
            }
            result = String.Join(" ", output.ToArray()) + " ";


            return result;
        }
    }
}