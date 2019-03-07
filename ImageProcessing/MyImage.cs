using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class MyImage
    {
        public MyImage(string path) : this(File.ReadAllBytes(path))
        {
            
        }

        public MyImage(byte[] data)
        {
            ImageType = ReadImageType(data);
            Size = ReadLittleEndian(data, 2);
            Offset = ReadLittleEndian(data, 10);
            Width = ReadLittleEndian(data, 18);
            Height = ReadLittleEndian(data, 22);
            ColorDepth = ReadLittleEndian(data, 28);

            ReadImageData(data);
        }

        private void ReadImageData(byte[] data)
        {
            Pixels = new Pixel[Width * Height];

            var byteWidth = Width * 3;
            if (byteWidth % 4 != 0)
            {
                byteWidth += 4 - byteWidth % 4;
            }

            for (var row = Height - 1; row >= 0; row--)
            {
                for (var column = 0; column < Width; column++)
                {
                    var pixelOffset = Offset + (Height - row - 1) * byteWidth + column * 3;
                    Pixels[Width * row + column] = new Pixel(data[pixelOffset], data[pixelOffset + 1], data[pixelOffset + 2]);
                }
            }
        }

        private int ReadLittleEndian(byte[] data, int index)
        {
            return data[index] 
                 | data[index + 1] << 8 
                 | data[index + 2] << 16 
                 | data[index + 3] << 24;
        }

        private ImageType ReadImageType(byte[] data)
        {
            if (data[0] == 'B' && data[1] == 'M')
            {
                return ImageType.Bitmap;
            }

            return ImageType.Unknown;
        }

        public ImageType ImageType { get; }
        public int Size { get; }
        public int Offset { get; }
        public int Height { get; }
        public int Width { get; }
        public int ColorDepth { get; }
        public Pixel[] Pixels { get; private set; }

        public Pixel this[int row, int column] => Pixels[row * Width + column];
    }
}
