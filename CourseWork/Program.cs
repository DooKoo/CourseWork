using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISCore.Models;
using ISCore;
using System.IO;
using System.Threading;

namespace CourseWork
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args[0] == "-path")
                {
                    View MainView = new View(args[1]);
                }
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine("Error. Use -path param with args");
                Console.ReadKey();
            }
        }
    }
}
