using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Contest
{
    public class TrademarksTest
    {
        [Test]

        public void TestRun()
        {
            string path = @"Contest\E\tests";
            string[] files = Directory.GetFiles(path).Where(x => string.IsNullOrEmpty(Path.GetExtension(x))).ToArray();
            string[] asserts = Directory.GetFiles(path, "*.a");
            for (int i = 0; i < files.Length; i++)
            {
                string[] lines = File.ReadAllLines(files[i]);
                string[] aLines = File.ReadAllLines(asserts[i]);
                List<string> output = new List<string>();
                int setCount = int.Parse(lines[0]);
                int offset = 1;
                List<string> dict = new List<string>();
                for(int j = 2; j < lines.Length; j++)
                {
                    if (int.TryParse(lines[j], out int dictSize) )
                    {
                        output.Add(Trademark.Calculate(dict));
                        dict = new List<string>();
                    }
                    else
                    {                        
                        dict.Add(lines[j]);
                    }
                }

                if(dict.Count > 0)
                    output.Add(Trademark.Calculate(dict));

                Assert.AreEqual(aLines.Length, output.Count, files[i]);
                for (int j = 0; j < output.Count; j++)
                {
                    Assert.AreEqual(aLines[j], output[j], files[i]);
                }
            }
        }
    }

}
