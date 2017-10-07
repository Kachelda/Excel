using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.ExcelData
{
    class Cell
    {
        public string Data { get; set; }

        public CustomPosition CurrentPosition { get; set; }

        public Cell(string data, CustomPosition currentPosition)
        {
            Data = data;
            CurrentPosition = currentPosition;
        }

        public bool EmptyCell(string str)
        {
            if (str.Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CellWithText(string str)
        {
            char str1 = '\'';

            int indexOfChar = str.IndexOf(str1);

            if (indexOfChar == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CellWithNumbers(string str)
        {
            var number = 0;
            if (Int32.TryParse(str, out number) && number > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CellWithFormula(string str)
        {
            char str1 = '=';

            int indexOfChar = str.IndexOf(str1);

            if (indexOfChar == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
