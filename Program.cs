﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new ExcelData.ExcelAnalog();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
              
            }

            Console.ReadLine();
        }
    }
}
