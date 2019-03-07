using System;

namespace ImageProcessing
{
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