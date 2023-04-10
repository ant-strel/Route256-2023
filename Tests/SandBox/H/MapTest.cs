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
    internal class MapTest
    {
        [Test]
        public void RunMapTest()
        {
            string path = @"SandBox\H\tests";
            string[] files = Directory.GetFiles(path).Where(x => string.IsNullOrEmpty(Path.GetExtension(x))).ToArray();
            string[] asserts = Directory.GetFiles(path, "*.a");

            for (int i = 0; i < files.Length; i++)
            {
               // Task.Run(() =>
               // {
                    List<string> output = new List<string>();
                    string[] lines = File.ReadAllLines(files[i]);
                    string[] aLines = File.ReadAllLines(asserts[i]);
                    output = Map.MapValidate(lines);

                    Assert.AreEqual(aLines.Length, output.Count, $"alines {aLines.Length} output {output.Count} test {files[i]}");

                    for (int j = 0; j < output.Count; j++)
                    {
                        Assert.AreEqual(aLines[j], output[j]);
                    }
               // });

            }
        }

    }
}
