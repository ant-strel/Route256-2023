using Lib;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SandBox
{
    public class TimesTest
    {
        [Test]
        public void RunTimesTest()
        {
            string path = @"SandBox\F\tests";
            string[] files = Directory.GetFiles(path).Where(x => string.IsNullOrEmpty(Path.GetExtension(x))).ToArray();
            string[] asserts = Directory.GetFiles(path, "*.a");

            for (int i = 0; i < files.Length; i++)
            {
                string[] lines = File.ReadAllLines(files[i]);
                string[] aLines = File.ReadAllLines(asserts[i]);

                List<string> results = new List<string>();

                int setCount = Convert.ToInt32(lines[0]);
                int offset = 1;
                for (int j = 0; j < setCount; j++)
                {
                    int timesCount = Convert.ToInt32(lines[offset++]);
                    List<string> times = new List<string>();
                    for(int k  = 0; k < timesCount; k++)
                    {
                        times.Add(lines[offset++]);
                    }
                    string result = TimeInterval.Calculate(times);
                    string expected = aLines[j];
                    Assert.AreEqual(result, expected);
                }
            }
        }

    }
}
