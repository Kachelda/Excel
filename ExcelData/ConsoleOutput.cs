using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.ExcelData
{
    class ConsoleOutput
    {
        public void PrintOneDimensional(Row row)
        {
            foreach (var cell in row.Cells)
            {
                Console.Write(cell.Data);
                Console.Write("\t");
            }
        }

        public void PrintTwoDimensional(Board board)
        {
            foreach (var row in board.Rows)
            {
                PrintOneDimensional(row);
                Console.Write("\n");
            }
            Console.WriteLine();
        }
    }
}
