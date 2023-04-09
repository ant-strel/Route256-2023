using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public  class ETable
    {
        public static List<string> Calculate(List<string> args)
        {
            string[] rowCol = args[0].Split(' '); // rows and cols input
            int rows = Convert.ToInt32(rowCol[0]);
            int cols = Convert.ToInt32(rowCol[1]);
            int[,] table = new int[rows, cols];
            int nextrow = 1;
            for (int j = 0; j < rows; j++)
            {
                string[] row = args[nextrow +j].Split(' '); // rows input
                for (var k = 0; k < cols; k++)
                {
                    table[j, k] = Convert.ToInt32(row[k]);
                }
            }

            int clicksCount = Convert.ToInt32(args[nextrow + rows]);//clicks count input
            string[] clicks = args[nextrow + rows + 1].Split(' '); // columns input
            for (int k = 0; k < clicksCount; k++)
            {
                int[] tempRow = new int[cols];
                int colName = Convert.ToInt32(clicks[k]) - 1;
                for (int write = 0; write < rows; write++)
                {
                    for (int sort = 0; sort < rows - 1; sort++)
                    {
                        if (table[sort, colName] > table[sort + 1, colName])
                        {
                            for (int o = 0; o < cols; o++)
                            {
                                tempRow[o] = table[sort + 1, o];
                            }
                            for (int o = 0; o < cols; o++)
                            {
                                table[sort + 1, o] = table[sort, o];
                            }
                            for (int o = 0; o < cols; o++)
                            {
                                table[sort, o] = tempRow[o];
                            }
                        }
                    }
                }
            }
            List<string> lines = new List<string>();
            for (int k = 0; k < rows; k++)
            {
                string line = "";
                for (int o = 0; o < cols; o++)
                {
                    line += (table[k, o]);
                    if (o < cols - 1)
                        line += (" ");
                }
                lines.Add(line);
            }
            return lines;

        }
    }
}
