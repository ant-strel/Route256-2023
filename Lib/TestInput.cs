using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class TestInput
    {
        private string _file;
        private static string[] _lines;
        private static int _currentline;
        public TestInput(string file)
        {
            _file = file;
            string[] lines = File.ReadAllLines(_file);
            _lines = lines;
            _currentline = 0;
        }
        public static string ReadLine()
        {
            string line = _lines[_currentline];
            _currentline++;
            return line;
        }
    }
}
