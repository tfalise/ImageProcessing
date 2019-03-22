namespace ImageProcessing
{
    public class RotateTransformation
    {
        private readonly ImageRotation _rotation;

        public RotateTransformation(ImageRotation rotation)
        {
            _rotation = rotation;
        }

        public MyImage Process(MyImage source)
        {
            var result = CreateRotatedCanvas(source);

            if (_rotation == ImageRotation.Clockwise90)
            {
                for (var row = 0; row < result.Height; row++)
                {
                    for (var column = 0; column < result.Width; column++)
                    {
                        var sourcePixel = source[source.Height - 1 - column, row];
                        result[row, column] = sourcePixel;
                    }
                }
            }

            if (_rotation == ImageRotation.Clockwise180)
            {
                for (var row = 0; row < result.Height; row++)
                {
                    for (var column = 0; column < result.Width; column++)
                    {
                        var sourcePixel = source[source.Height - 1 - row, source.Width - 1 - column];
                        result[row, column] = sourcePixel;
                    }
                }
            }

            if (_rotation == ImageRotation.Clockwise270)
            {
                for (var row = 0; row < result.Height; row++)
                {
                    for (var column = 0; column < result.Width; column++)
                    {
                        var sourcePixel = source[column, source.Width - 1 - row];
                        result[row, column] = sourcePixel;
                    }
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