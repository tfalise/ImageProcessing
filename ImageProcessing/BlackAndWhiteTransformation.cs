﻿using System;

namespace ImageProcessing
{
    public class BlackAndWhiteTransformation
    {
        private BlackAndWhiteAlgorithm _algorithm;

        public BlackAndWhiteTransformation() : this(BlackAndWhiteAlgorithm.Average)
        {
            
        }

        public BlackAndWhiteTransformation(BlackAndWhiteAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public MyImage Process(MyImage source)
        {
            var result = new MyImage(source.Width, source.Height);
            for (var i = 0; i < result.Pixels.Length; i++)
            {
                if(_algorithm == BlackAndWhiteAlgorithm.Average)
                {
                    var grayLevel = (source.Pixels[i].Red + source.Pixels[i].Blue + source.Pixels[i].Green) / 3;
                    result.Pixels[i] = new Pixel(grayLevel, grayLevel, grayLevel);
                }
                else 
                {
                    var maxRGB = Math.Max(Math.Max(source.Pixels[i].Red, source.Pixels[i].Blue), source.Pixels[i].Green);
                    var minRGB = Math.Min(Math.Min(source.Pixels[i].Red, source.Pixels[i].Blue), source.Pixels[i].Green);
                    var grayLevel = (maxRGB + minRGB) / 2;
                    result.Pixels[i] = new Pixel(grayLevel, grayLevel, grayLevel);
                }
            }
            return result;
        }
    }
}