using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.ExcelData
{
    class Board
    {
        public int CountOfRows { get; set; }

        public int CountOfColumns { get; set; }
        
        public List<Row> Rows { get; set; }

        public Cell Cell { get; set; }

        public Board(int rows, int columns)
        {
            CountOfRows = rows;
            CountOfColumns = columns;
            Rows = new List<Row>();
        }

        public void Initialization(List<string> list)
        {
            char a = 'A';
            Row row = new Row();
            foreach (string str in list)
            {
                Cell = new Cell(str, new CustomPosition(a, Rows.Count + 1));
                a++;
                row.Cells.Add(Cell);
              
                if (row.Cells.Count == CountOfColumns)
                {
                    Rows.Add(row);
                    if (Rows.Count == CountOfRows)
                    {
                        return;
                    }
                    else
                    {
                        row = new Row();
                        a = 'A';
                    }
                }
            }
        }
    }
}
