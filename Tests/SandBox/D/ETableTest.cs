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
    public class ETableTest
    {
        [Test]
        public static void ETableTestRun()
        {
            string path = @"SandBox\D\tests";
            string[] files = Directory.GetFiles(path).Where(x => string.IsNullOrEmpty(Path.GetExtension(x))).ToArray();
            string[] asserts = Directory.GetFiles(path, "*.a");

            for (int i = 0; i < files.Length; i++)
            {
                string[] inputs = File.ReadAllLines(files[i]);
                string[] aLines = File.ReadAllLines(asserts[i]);

                int count = Convert.ToInt32(inputs[0]);
                int offset = 1;
                List<string> result = new List<string>();
                List<List<string>> input = new List<List<string>>();
                List<List<string>> output = new List<List<string>> ();
                List<List<string>> expected = new List<List<string>> ();
                List<string> buffer = new List<string>();
                for(int j = 2; j < inputs.Length; j++)
                {
                    if (inputs[j] == "") 
                    {
                        input.Add(new List<string>(buffer));
                        buffer = new List<string>();
                    }
                    else
                    {
                        buffer.Add(inputs[j]);
                    }
                }
                if(buffer.Count > 0)
                    input.Add(new List<string>(buffer));
                buffer = new List<string>();
                foreach(var inp in input)
                {
                    List<string> res = ETable.Calculate(inp);
                    output.Add(new List<string>(res));
                }
                for (int j = 0; j < aLines.Length; j++)
                {
                    if (aLines[j] == "")
                    {
                        expected.Add(new List<string>(buffer));
                        buffer = new List<string>();
                    }
                    else
                    {
                        buffer.Add(aLines[j]);
                    }
                }

                Assert.IsTrue(output.Count == expected.Count);
                for (int j = 0; j < output.Count; j++)
                {
                    Assert.IsTrue(output[j].Count == expected[j].Count);

                    for (int k = 0; k < output[j].Count; k++)
                    {
                        Assert.AreEqual(expected[j][k], output[j][k]);
                    }
                }
                
            }
        }
    }
}
