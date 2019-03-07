using System;
using System.Diagnostics;

namespace ImageProcessing
{
    [DebuggerDisplay("R{Red}G{Green}B{Blue}")]
    public struct Pixel
    {
        public Pixel(int blue, int green, int red)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
        
        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }
    }
}