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
    internal class SchedullerTest
    {
        [Test]
        public void RunSchedullerTest()
        {
            string path = @"SandBox\I\tests";
            string[] files = Directory.GetFiles(path).Where(x => string.IsNullOrEmpty(Path.GetExtension(x))).ToArray();
            string[] asserts = Directory.GetFiles(path, "*.a");
            for (int i = 0; i < files.Length; i++)
            {
                Task.Run(() =>
                {
                    string[] lines = File.ReadAllLines(files[i]);
                    string[] aLines = File.ReadAllLines(asserts[i]);
                    string procAndTasks = lines[0];
                    string proc = lines[1];
                    List<string> tasks = new List<string>();
                    for (int j = 2; j < lines.Length; j++)
                    {
                        tasks.Add(lines[j]);
                    }
                    string result = Scheduller.Calculate(procAndTasks, proc, tasks);
                    Assert.AreEqual(aLines[0], result, files[i]);
                });
            }
        }

    }
}
