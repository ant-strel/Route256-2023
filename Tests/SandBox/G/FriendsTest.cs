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
    public class FriendsTest
    {
        [Test, MaxTime(75000)]
        public void RunFriendsTest()
        {
            string path = @"SandBox\G\tests";
            string[] files = Directory.GetFiles(path).Where(x => string.IsNullOrEmpty(Path.GetExtension(x))).ToArray();
            string[] asserts = Directory.GetFiles(path, "*.a");

               for (int i = 0; i < files.Length; i++)
              {
                //i = 1;
                Task task = Task.Run(() =>
                {
                    List<string> output = new List<string>();
                string[] lines = File.ReadAllLines(files[i]);
                string[] aLines = File.ReadAllLines(asserts[i]);

                    string[] parts = lines[0].Split(' ');
                    int userInputCount = Convert.ToInt32(parts[0]);
                    int pairCount = Convert.ToInt32(parts[1]);
                    List<string> pairs = lines.ToList();
                    pairs.RemoveAt(0);
                    output = Friends.FriendsRecomendations(userInputCount, pairCount, pairs);

               
                //task.Wait(3000);

                //Assert.IsTrue(task.IsCompleted, files[i] +" "+ asserts[i]);

                Assert.AreEqual(aLines.Length, output.Count,$"alines {aLines.Length} output {output.Count} test {files[i]}");

                for (int j = 0; j < output.Count; j++)
                {
                    Assert.AreEqual(aLines[j], output[j]);
                }
                });

            }
        }
    }
}
