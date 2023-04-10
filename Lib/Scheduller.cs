using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Processor
    {
        public Processor(long energy)
        {
            Energy = energy;
        }
        public long Energy { get; private set; }
        public long EndWorkTime { get; set; }

    }

    public class ProcessorComparer : IComparer<Processor>
    {
        public int Compare(Processor x, Processor y)
        {
            var result = x.EndWorkTime.CompareTo(y.EndWorkTime);

            //then power
            if (result == 0)
                result = x.Energy.CompareTo(y.Energy);

            return result;
        }
    }

    public class Scheduller
    {    
            public static string Calculate(string procAndTask, string proc, List<string> tasks)
            {

                    var processesandtasks = procAndTask.Split(' ').Select(x => long.Parse(x)).ToList();
                    long procCount = processesandtasks.First();
                    long tasksCount = processesandtasks.Last();

                    var processorsInput = proc.Split(' ').Select(x => long.Parse(x)).ToList();
                    SortedSet<long> freeProcessors = new SortedSet<long>();
                    SortedSet<Processor> busyProcessors = new SortedSet<Processor>(new ProcessorComparer());
                    long cost = 0;

                    foreach (var process in processorsInput)
                    {
                        freeProcessors.Add(process);
                    }


                    for (int i = 0; i < tasksCount; i++)
                    {
                        var input = tasks[i].Split(' ').Select(x=>long.Parse(x)).ToList();
                        long order = input[0];
                        long duration = input[1];

                        if (busyProcessors.Count > 0)
                        {
                            var setCount = busyProcessors.Count;

                            for (int j = 0; j < setCount; j++)
                            {
                                var procBusy = busyProcessors.FirstOrDefault();

                                if (procBusy.EndWorkTime <= order)
                                {
                                    busyProcessors.Remove(procBusy);
                                    procBusy.EndWorkTime = 0;

                                    freeProcessors.Add((int)procBusy.Energy);

                                }
                                else
                                {
                                    break;
                                }
                            }

                        }

                        if (freeProcessors.Count > 0)
                        {
                            var procPower = freeProcessors.First();
                            freeProcessors.Remove(procPower);

                            cost += duration * procPower;
                            busyProcessors.Add(new Processor(procPower) { EndWorkTime = order + duration });

                        }

                    }
            
                    return cost.ToString();
            }

    }
}
