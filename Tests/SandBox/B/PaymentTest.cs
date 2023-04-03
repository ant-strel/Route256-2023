using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace Tests.SandBox
{
    internal class PaymentTest
    {
        [Test]
        public void PaymentTestRun()
        {
            string path = @"SandBox\B\tests";
            string[] files = Directory.GetFiles(path).Where(x=>string.IsNullOrEmpty(Path.GetExtension(x))).ToArray();
            string[] asserts = Directory.GetFiles(path, "*.a");
            for(int i = 0; i < files.Length; i++)
            {
                string[] lines = File.ReadAllLines(files[i]);
                string[] aLines = File.ReadAllLines(asserts[i]);
                int count = Convert.ToInt32(lines[0]);
                int offset = 1;
                for(int j = 0; j < count; j++)
                {
                    Assert.AreEqual(aLines[j], Payment.SumGoods(lines[offset],lines[offset + 1]));
                    offset+=2;
                }
            }

        }

    }
}
