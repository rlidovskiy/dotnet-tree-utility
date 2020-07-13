using System;

namespace TreeUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            var output = Console.Out;
            if (args.Length != 1 && args.Length != 2)
            {
                output.WriteLine("usage: dotnet run . [-f]");
                return;
            }

            var path = args[0];
            var printFiles = args.Length == 2 && args[1] == "-f";
            
            var builder = new TreeBuilder(output);
            builder.Build(path, printFiles);
        }
    }
}