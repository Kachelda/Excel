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

        public Board(int rows, int columns)
        {
            CountOfRows = rows;
            CountOfColumns = columns;
            Rows = new List<Row>();
        }

        public void Initialization(List<string> list)
        {
            char firstLetter = 'A';
            char bufferFirstLetter = firstLetter;
            Row row = new Row();
            foreach (string str in list)
            {
                Cell cell = new Cell(str, new CustomPosition(firstLetter, Rows.Count + 1));
                firstLetter++;
                row.Cells.Add(cell);
              
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
                        firstLetter = bufferFirstLetter;
                    }
                }
            }
        }

        public void RunBoard()
        {
            foreach (var row in Rows)
            {
                foreach (var cell in row.Cells)
                {
                    if (cell.EmptyCell(cell.Data))
                    {
                        cell.Data = String.Empty;
                    }
                    else if (cell.CellWithText(cell.Data))
                    {
                        cell.Data = cell.Data.Substring(1);
                    }
                    else if (cell.CellWithNumbers(cell.Data))
                    {
                        cell.Data = cell.Data;
                    }
                    else if (cell.CellWithFormula(cell.Data))
                    {
                        if (RunCellWithFormula(cell) == 1000)
                        {
                            cell.Data = "#Error";
                        }
                        else
                        {
                            cell.Data = RunCellWithFormula(cell).ToString();
                        }
                    }
                    else
                    {
                        cell.Data = "#Error";
                    }
                }
            }
        }

        public int RunCellWithFormula(Cell cell)
        {
            string str = cell.Data.Substring(1);

            str = str + ')';
            Stack<int> Operands = new Stack<int>();
            Stack<char> Functions = new Stack<char>();
            int errorInput = 1000;
            int position = 0;
            object token;
            object prevToken = 'Ы';

            do
            {
                token = GetToken(str, ref position);

                if (token is char && prevToken is char &&
                    ((char)token == '+' || (char)token == '-'))
                {
                    Operands.Push(0);
                }

                if (token is char && prevToken is char &&
                    ((char)token == '*' || (char)token == '/'))
                {
                    Operands.Push(1);
                }

                if (token is int)
                {
                    if ((char)prevToken >= 'A' && (char)prevToken <= 'Z')
                    {
                        if (cell.CurrentPosition.X == (char) prevToken && cell.CurrentPosition.Y == (int) token)
                        {
                            return errorInput;
                            break;
                        }
                        Operands.Push(GetValuePosition((char)prevToken, (int)token));
                    }
                    else
                    {
                        Operands.Push((int)token);
                    }
                }

                else if (token is char)
                {
                    if ((char)token == ')')
                    {
                        while (Functions.Count > 0)
                        {
                            PopFunction(Operands, Functions);
                        }
                    }
                    if ((char)token >= 'A' && (char)token <= 'Z')
                    {
                        prevToken = token;
                        continue;
                    }
                    if (Functions.Count > 0)
                    {
                        PopFunction(Operands, Functions);
                    }
                    if ((char)token == '+' || (char)token == '-' || (char)token == '*' || (char)token == '/')
                    {
                        Functions.Push((char)token);
                    }
                }
                prevToken = token;
            }
            while (token != null);

            if (Operands.Count > 1 || Functions.Count > 0)
            {
                throw new Exception("Ошибка в разборе выражения");
            }

            return Operands.Pop();
        }

        public void PopFunction(Stack<int> Operands, Stack<char> Functions)
        {
            int B = Operands.Pop();
            int A = Operands.Pop();
            switch (Functions.Pop())
            {
                case '+':
                    Operands.Push(A + B);
                    break;
                case '-':
                    Operands.Push(A - B);
                    break;
                case '*':
                    Operands.Push(A * B);
                    break;
                case '/':
                    Operands.Push(A / B);
                    break;
            }
        }

        public object GetToken(string s, ref int position)
        {
            ReadWhiteSpace(s, ref position);

            if (position == s.Length)
                return null;
            if (char.IsDigit(s[position]))
                return Convert.ToInt32(ReadInt(s, ref position));
            else
                return ReadChar(s, ref position);
        }

        private static char ReadChar(string s, ref int position)
        {
            return s[position++];
        }

        public string ReadInt(string s, ref int position)
        {
            string res = "";
            while (position < s.Length && char.IsDigit(s[position]))
                res += s[position++];

            return res;
        }

        public void ReadWhiteSpace(string s, ref int position)
        {
            while (position < s.Length && char.IsWhiteSpace(s[position]))
                position++;
        }

        public int GetValuePosition(char prevToken, int token)
        {
            int value = 0;

            foreach (var row in Rows)
            {
                foreach (var cell in row.Cells)
                {
                    if (cell.CurrentPosition.X == prevToken && cell.CurrentPosition.Y == token)
                    {
                        if (Int32.TryParse(cell.Data, out value))
                        {
                            return value;
                        }
                        else
                        {
                            return RunCellWithFormula(cell);
                        }
                    }
                    
                }
            }
            return value;

        }
    }
}
