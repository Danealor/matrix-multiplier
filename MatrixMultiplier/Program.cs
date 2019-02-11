using MatrixExpressions;
using System;
using System.IO;

namespace MatrixMultiplier
{
    class Program
    {
        const string USAGE = " input.txt [output.txt]";

        private static string GetExecutingFilename()
        {
            return Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location);
        }

        private static int PrintUsage()
        {
            Console.WriteLine("Usage: " + GetExecutingFilename() + USAGE);
            return 1;
        }

        static int Main(string[] args)
        {
            string infile = null, outfile = null;

            if (args.Length != 1 && args.Length != 2)
                return PrintUsage();

            if (string.IsNullOrEmpty(args[0]))
                return PrintUsage();
            infile = args[0];

            if (args.Length == 2)
            {
                if (string.IsNullOrEmpty(args[1]))
                    return PrintUsage();
                outfile = args[1];
            }

            LimitedPrimeSieve primeSieve = new AtkinsSieve();
            foreach (int p in primeSieve.TakeUpTo(10000))
            {
                Console.WriteLine(p);
            }

            Console.ReadLine();

            return 0;
        }
    }
}
