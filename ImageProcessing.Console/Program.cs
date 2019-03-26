using System;

namespace ImageProcessing.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new MyImage(args[0]);
            var transformation = new BlackAndWhiteTransformation();
            var result = transformation.Process(source);
            result.WriteToFile(args[1]);
        }
    }
}
