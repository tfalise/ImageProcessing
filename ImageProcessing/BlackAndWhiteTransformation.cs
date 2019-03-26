using System;
using System.Collections.Generic;

namespace ImageProcessing
{
    public class BlackAndWhiteTransformation
    {
        private BlackAndWhiteAlgorithm _algorithm;

        private static readonly Dictionary<BlackAndWhiteAlgorithm, Func<Pixel, int>> GrayLevels = new Dictionary<BlackAndWhiteAlgorithm, Func<Pixel, int>>() 
        {
            [BlackAndWhiteAlgorithm.Average] = (pixel) => (pixel.Red + pixel.Green + pixel.Blue) / 3,
            [BlackAndWhiteAlgorithm.Lightness] = (pixel) => (Math.Max(Math.Max(pixel.Red, pixel.Blue), pixel.Green) + Math.Min(Math.Min(pixel.Red, pixel.Blue), pixel.Green)) / 2,
            [BlackAndWhiteAlgorithm.Luminosity] = (pixel) => (int)(0.21 * pixel.Red + 0.07 * pixel.Blue + 0.72 * pixel.Green)
        };

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
                var grayLevel = GrayLevels[_algorithm](source.Pixels[i]);
                result.Pixels[i] = new Pixel(grayLevel, grayLevel, grayLevel);
            }
            return result;
        }
    }
}