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
            Board.RunBoard();
            PrintBoard();
        }

        public void InitBoard()
        {
            bool correctInput = false;
            while (!correctInput)
            {
                Console.WriteLine("Введите количество строк и столбцов");
                string line = Console.ReadLine();
                string[] listStrings = line.Split('\t');
                if (listStrings.Length == 2)
                {
                    if (Int32.TryParse(listStrings[0], out int rowsResult) && rowsResult > 0 &&
                        Int32.TryParse(listStrings[1], out int columnsResult) && columnsResult > 0)
                    {
                        CountOfRows = rowsResult;
                        CountOfColumns = columnsResult;
                        correctInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка! Повторите ввод!");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка! Повторите ввод!");
                }
            }
            
            List<string> list = new List<string>();

            while (correctInput)
            {
                for (int i = 1; i <= CountOfRows; i++)
                {
                    Console.WriteLine("Введите элементы {0} строки", i);
                    string lineRow = Console.ReadLine();
                    string[] arrayStrings = lineRow.Split('\t');

                    if (arrayStrings.Length == CountOfColumns)
                    {
                        foreach (string str in arrayStrings)
                        {
                            string str1 = str.Replace(" ", string.Empty);
                            list.Add(str1);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка! Повторите ввод!");
                        i--;
                    }
                }
                correctInput = false;
            }

            Board = new Board(CountOfRows, CountOfColumns);
            Board.Initialization(list);
        }

        public void PrintBoard()
        {
            ConsoleOutput output = new ConsoleOutput();
            Console.WriteLine();
            output.PrintTwoDimensional(Board);

            Console.ReadLine();
        }
    }
}
