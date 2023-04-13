using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Contest
{
    public class CompetitionResultsTest
    {
        [Test]

        public void TestRun()
        {
            string path = @"Contest\D\tests";
            string[] files = Directory.GetFiles(path).Where(x => string.IsNullOrEmpty(Path.GetExtension(x))).ToArray();
            string[] asserts = Directory.GetFiles(path, "*.a");
            for (int i = 0; i < files.Length; i++)
            {
                string[] lines = File.ReadAllLines(files[i]);
                string[] aLines = File.ReadAllLines(asserts[i]);
                int count = Convert.ToInt32(lines[0]);
                List<string> outputs = new List<string>();
                int offset = 1;
                for (int j = 0; j < count; j++)
                {
                    outputs.Add(Competition.Calculate(lines[offset++], lines[offset++]));
                }

                Assert.AreEqual(aLines.Length, outputs.Count, files[i]);
                for (int j = 0; j < outputs.Count; j++)
                {
                    Assert.AreEqual(aLines[j], outputs[j], files[i]);
                }
            }
        }
    }
}
