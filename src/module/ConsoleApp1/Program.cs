using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIToolsDll.Compare;
using TIToolsDll.Utility;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var compare = new DirCompare(
                new LogHandler(
                    logProgress: log => Console.WriteLine(log),
                    logVerbose: log => Console.WriteLine(log),
                    logDebug: log => Console.WriteLine(log),
                    logWarning: log => Console.WriteLine(log),
                    logError: exp => Console.WriteLine(exp.Message)
                    )
                );

            var task = compare.GetDiff(
                "D:\\home\\repositories\\windows\\WaveRecoder",
                "D:\\home\\repositories\\windows\\WaveRecoder",
                DiffMethod.ExistOrNot_MD5Hash,
                4);

            task.Wait();
            var result = task.Result;

            Console.WriteLine(result.Length);
            Console.ReadLine();
        }
    }
}
