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
    internal class RhymesTest
    {
        [Test]
        public void RunRhymesTest()
        {
            string path = @"SandBox\J\tests";
            string[] files = Directory.GetFiles(path).Where(x => string.IsNullOrEmpty(Path.GetExtension(x))).ToArray();
            string[] asserts = Directory.GetFiles(path, "*.a");

            for (int i = 0; i < files.Length; i++)
            {
                List<string> output = new List<string>();
                string[] lines = File.ReadAllLines(files[i]);
                string[] aLines = File.ReadAllLines(asserts[i]);
                List<string> dict = new List<string>();
                List<string> queries = new List<string>();
                int wordCount = Convert.ToInt32(lines[0]);
                int queryCount = Convert.ToInt32(lines[1 + wordCount]);
                for(int j = 1; j <= wordCount; j++)
                {
                    dict.Add(lines[j]);
                }
                for(int j = wordCount + 2; j < lines.Length; j++)
                {
                    queries.Add(lines[j]);
                }
                 
                output = Rhymes.Calculate(dict, queries);
                Assert.AreEqual(aLines.Length, output.Count, files[i]);

                //for(int j = 0; j < output.Count; j++)
                //{
                //    Assert.AreEqual(aLines[j], output[j]);
                //}
            }
        }
    }
}

