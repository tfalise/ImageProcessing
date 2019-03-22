using System;
using System.Collections.Generic;
using System.Data;

namespace ImageProcessing
{
    public class RotateTransformation
    {
        private readonly ImageRotation _rotation;
        private readonly Dictionary<ImageRotation, Func<int,int,MyImage,Pixel>> _rotationMappings = new Dictionary<ImageRotation, Func<int, int, MyImage, Pixel>>()
        {
            [ImageRotation.None] = (row, column, image) => image[row, column],
            [ImageRotation.Clockwise90] = (row, column, image) => image[image.Height - 1 - column, row],
            [ImageRotation.Clockwise180] = (row, column, image) => image[image.Height - 1 - row, image.Width - 1 - column],
            [ImageRotation.Clockwise270] = (row, column, image) => image[column, image.Width - 1 - row],
        };

        public RotateTransformation(ImageRotation rotation)
        {
            _rotation = rotation;
        }

        public MyImage Process(MyImage source)
        {
            var result = CreateRotatedCanvas(source);

            for (var row = 0; row < result.Height; row++)
            {
                for (var column = 0; column < result.Width; column++)
                {
                    result[row, column] = _rotationMappings[_rotation](row, column, source);
                }
            }

            return result;
        }

        private MyImage CreateRotatedCanvas(MyImage source)
        {
            MyImage result;
            if (_rotation == ImageRotation.Clockwise90 || _rotation == ImageRotation.Clockwise270)
            {
                result = new MyImage(source.Height, source.Width);
            }
            else
            {
                result = new MyImage(source.Width, source.Height);
            }

            return result;
        }
    }
}