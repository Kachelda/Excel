using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.ExcelData
{
    class CustomPosition
    {
        public char X { get; set; }

        public int Y { get; set; }

        public CustomPosition(char x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
