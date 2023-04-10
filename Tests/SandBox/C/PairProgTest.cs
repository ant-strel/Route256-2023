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
    internal class PairProgTest
    {
        [Test]
        public void PairProgTestRun()
        {
            string path = @"SandBox\C\tests";
            string[] files = Directory.GetFiles(path).Where(x => string.IsNullOrEmpty(Path.GetExtension(x))).ToArray();
            string[] asserts = Directory.GetFiles(path, "*.a");
  
            for (int i = 0; i < files.Length; i++)
            {
                Task.Run(() => {
                string[] lines = File.ReadAllLines(files[i]);
                string[] aLines = File.ReadAllLines(asserts[i]);
                List<List<string>> actual = new List<List<string>>();
                List<string> expected = new List<string>();
                foreach (string line in aLines)
                {
                    if(!string.IsNullOrEmpty(line))
                        expected.Add(line);
                    else
                    {
                        actual.Add(expected);
                        expected = new List<string>();
                    }
                }
                if (expected.Count != 0)
                    actual.Add(expected);

                int count = Convert.ToInt32(lines[0]);
                int offset = 1;
                for (int j = 0; j < count; j++)
                {
                    List<string> res = PairProg.PairProgramming(lines[offset], lines[offset + 1]);
                    offset += 2;
                    for(int k = 0; k < res.Count; k++)
                    {
                        Assert.AreEqual(actual[j][k], res[k]);
                    }
                }
                });
            }
        }
    }
}
