using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace hcapby 
{
    static class Program 
    {
        static void Main()
        {
            string solve = Info.captchasolver.SolveCaptcha();
            Console.WriteLine($"Capcha solve {sha1(solve)}: {solve.Substring(0, 50)}...");
            Console.ReadKey(false);
        }
    }
}
