using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Contest
{
    public class PrintDocTest
    {
        [Test]

        public void TestRun()
        {
            string path = @"Contest\F\tests";
            string[] files = Directory.GetFiles(path).Where(x => string.IsNullOrEmpty(Path.GetExtension(x))).ToArray();
            string[] asserts = Directory.GetFiles(path, "*.a");
            for (int i = 0; i < files.Length; i++)
            {
               
                string[] lines = File.ReadAllLines(files[i]);
                string[] aLines = File.ReadAllLines(asserts[i]);
                List<string> output = new List<string>();
                int setCount = int.Parse(lines[0]);
                for(int j =1; j<lines.Length; j += 2)
                {
                    output.Add(PrintDoc.Calculate(lines[j], lines[j + 1]));
                }

                Assert.AreEqual(aLines.Length, output.Count);
                for(int j = 0; j < aLines.Length;j++)
                {
                    Assert.IsTrue(aLines[j].Length == output[j].Length, $"{files[i]} {aLines[j]} {output[j]}");
                }
            }
        }
    }
}
