using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Contest
{
    public class SeaBattleTest
    {
        [Test]
        public static void RunTest()
        {
            string path = @"Contest\B\tests";
            string[] files = Directory.GetFiles(path).Where(x => string.IsNullOrEmpty(Path.GetExtension(x))).ToArray();
            string[] asserts = Directory.GetFiles(path, "*.a");
            for (int i = 0; i < files.Length; i++)
            {
                string[] lines = File.ReadAllLines(files[i]);
                string[] aLines = File.ReadAllLines(asserts[i]);
                int count = Convert.ToInt32(lines[0]);

                List<string>inputs = new List<string>();
                for (int j = 1; j < lines.Length; j++)
                {
                    inputs.Add(lines[j]);
                }

                List<string> outputs = SeaBattle.Calculate(inputs);

                Assert.AreEqual(aLines.Length, outputs.Count);
                for (int j = 0; j < outputs.Count; j++)
                {
                    Assert.AreEqual(aLines[j], outputs[j]); 
                }

            }
        }
    } 

}
