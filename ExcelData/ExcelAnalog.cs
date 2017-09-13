using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.ExcelData
{
    class ExcelAnalog
    {
        public int CountOfRows { get; set; }

        public int CountOfColumns { get; set; }

        public Board Board;

        public ExcelAnalog()
        {
            InitBoard();
        }

        public void InitBoard()
        {
            Console.WriteLine("Введите первую строку");
            string line = Console.ReadLine();
            string[] listStrings = line.Split('\t');

            CountOfRows = Convert.ToInt32(listStrings[0]);
            CountOfColumns = Convert.ToInt32(listStrings[1]);

            List<string> list = new List<string>();

            Console.WriteLine("Введите элементы первой строки");
            string line1 = Console.ReadLine();
            string[] listElements1 = line1.Split('\t');

            foreach (string str in listElements1)
            {
                list.Add(str);
            }

            Console.WriteLine("Введите элементы второй строки");
            string line2 = Console.ReadLine();
            string[] listElements2 = line2.Split('\t');

            foreach (string str in listElements2)
            {
                list.Add(str);
            }

            Board = new Board(CountOfRows, CountOfColumns);
            Board.Initialization(list);

            ConsoleOutput Output = new ConsoleOutput();
            Console.WriteLine();
            Output.PrintTwoDimensional(Board);

            Console.ReadLine();
        }
    }
}
