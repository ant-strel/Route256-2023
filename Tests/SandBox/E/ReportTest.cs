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
    public class ReportTest
    {
        [Test]
        public void RunReportTest()
        {
            string path = @"SandBox\E\tests";
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
                    string result = Report.CreateReport(lines[offset++], lines[offset++]);
                    string expected = aLines[j];
                   Assert.AreEqual(result,expected);
                }
            }
        }

    }
}
