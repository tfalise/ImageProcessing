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