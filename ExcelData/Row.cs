using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.ExcelData
{
    class Row
    {
        public List<Cell> Cells { get; set; }

        public Row()
        {
            Cells = new List<Cell>();
        }
    }
}
