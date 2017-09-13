using System;
using System.Collections.Generic;
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
    }
}
