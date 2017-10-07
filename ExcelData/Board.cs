using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.ExcelData
{
    enum TokenValueType
    {
        Default,
        IntTypeToken,
        CharTypeToken
    }
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
                        try
                        {
                            cell.Data = RunCellWithFormula(cell).ToString();
                        }
                        catch (Exception e)
                        {
                            cell.Data = e.Message;
                        }
                        
                    }
                    else
                    {
                        cell.Data = "# Ошибка ввода";
                    }
                }
            }
        }

        public int RunCellWithFormula(Cell cell)
        {
            string stringFormula = cell.Data.Substring(1);

            Stack<int> operands = new Stack<int>();
            Stack<char> functions = new Stack<char>();
            List<Cell> repeatableCells = new List<Cell>();
            
            int position = 0;
            TokenValueType token;
            char prevToken = '\0';

            repeatableCells.Add(cell);

            do
            {
                if (functions.Count == operands.Count - 1 && functions.Count > 0)
                {
                    PopFunction(operands, functions);
                }

                token = GetToken(stringFormula, ref position);

                switch (token)
                {
                        case TokenValueType.CharTypeToken:

                            var tokenChar = ReadChar(stringFormula, ref position);

                            if (tokenChar >= 'A' && tokenChar <= 'Z')
                            {
                                prevToken = tokenChar;
                                continue;
                            }
                            else if (tokenChar == '+' || tokenChar == '-' || tokenChar == '*' || tokenChar == '/')
                            {
                                functions.Push(tokenChar);
                            }
                            else
                            {
                                throw new Exception("# Ошибка ввода");
                            }
                        break;

                        case TokenValueType.IntTypeToken:

                            var tokenInt = Convert.ToInt32(ReadInt(stringFormula, ref position));

                            if (prevToken >= 'A' && prevToken <= 'Z')
                            {
                                if (GetRepeatTime(repeatableCells, prevToken, tokenInt))
                                {
                                    throw new Exception("Ссылка на самого себя");
                                }
                                operands.Push(GetValuePosition(prevToken, tokenInt));
                                prevToken = '\0';
                            }
                            else
                            {
                                operands.Push(tokenInt);
                            }
                        break;

                        case TokenValueType.Default:
                        break;
                }
               
            }
            while (token != TokenValueType.Default);

            if (operands.Count > 1 || functions.Count > 0)
            {
                throw new Exception("Ошибка в разборе выражения");
            }

            return operands.Pop();
        }

        public void PopFunction(Stack<int> operands, Stack<char> functions)
        {
            int b = operands.Pop();
            int a = operands.Pop();
            switch (functions.Pop())
            {
                case '+':
                    operands.Push(a + b);
                    break;
                case '-':
                    operands.Push(a - b);
                    break;
                case '*':
                    operands.Push(a * b);
                    break;
                case '/':
                    operands.Push(a / b);
                    break;
            }
        }

        public TokenValueType GetToken(string stringFormula, ref int position)
        {
            if (position == stringFormula.Length)
            {
                return TokenValueType.Default;
            }
            if (char.IsDigit(stringFormula[position]))
            {
                return TokenValueType.IntTypeToken; 
            }
            else
            {
                return TokenValueType.CharTypeToken;
            }
        }

        private char ReadChar(string stringFormula, ref int position)
        {
            return stringFormula[position++];
        }

        public string ReadInt(string stringFormula, ref int position)
        {
            string res = "";
            while (position < stringFormula.Length && char.IsDigit(stringFormula[position]))
            {
                res += stringFormula[position++];
            }
            return res;
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

        public bool GetRepeatTime(List<Cell> cells, char prevToken, int token) 
        {
            foreach (Cell cell in cells)
            {
                if (cell.CurrentPosition.X == prevToken &&
                    cell.CurrentPosition.Y == token)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
