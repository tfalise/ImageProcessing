﻿namespace ImageProcessing
{
    public class BlackAndWhiteTransformation
    {
        public MyImage Process(MyImage source)
        {
            var result = new MyImage(source.Width, source.Height);
            for (var i = 0; i < result.Pixels.Length; i++)
            {
                var grayLevel = (source.Pixels[i].Red + source.Pixels[i].Blue + source.Pixels[i].Green) / 3;
                result.Pixels[i] = new Pixel(grayLevel, grayLevel, grayLevel);
            }
            return result;
        }
    }
}