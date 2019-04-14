using ButterflyMatrices;
using MatrixExpressions;
using System;
using System.IO;
using System.Linq;

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

            DictArrayVariableStore store = new DictArrayVariableStore();
            Matrix matrix = ButterflyMatrix.Generate(8, "b", store).Take(5).Create().Product(); // less memory than .Product().Create()

            Console.WriteLine(matrix.ToString(store));

            Console.ReadLine();

            return 0;
        }
    }
}
