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
        private const int BitmapHeaderSize = 14;
        private const int BitmapInformationHeaderSize = 40;
        private readonly int _rowByteSize;

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

            _rowByteSize = ComputeRowByteSize();

            ReadImageData(data);
        }

        private void ReadImageData(byte[] data)
        {
            Pixels = new Pixel[Width * Height];

            for (var row = Height - 1; row >= 0; row--)
            {
                for (var column = 0; column < Width; column++)
                {
                    var pixelOffset = Offset + (Height - row - 1) * _rowByteSize + column * 3;
                    Pixels[Width * row + column] = new Pixel(data[pixelOffset], data[pixelOffset + 1], data[pixelOffset + 2]);
                }
            }
        }

        private int ComputeRowByteSize()
        {
            var byteWidth = Width * 3;
            if (byteWidth % 4 != 0)
            {
                byteWidth += 4 - byteWidth % 4;
            }

            return byteWidth;
        }

        private static int ReadLittleEndian(byte[] data, int index)
        {
            return data[index] 
                 | data[index + 1] << 8 
                 | data[index + 2] << 16 
                 | data[index + 3] << 24;
        }

        private static ImageType ReadImageType(byte[] data)
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

        public byte[] GetBytes()
        {
            var bytes = new byte[BitmapHeaderSize + BitmapInformationHeaderSize + Height * _rowByteSize];

            WriteImageHeader(bytes);
            WriteImageInformationHeader(bytes);
            WriteImageData(bytes);

            return bytes;
        }

        private void WriteImageData(byte[] bytes)
        {
            for (int row = 0; row < Height; row++)
            {
                for (int column = 0; column < Width; column++)
                {
                    var pixelOffset = Offset + (Height - row - 1) * _rowByteSize + column * 3;
                    bytes[pixelOffset] = (byte) this[row, column].Blue;
                    bytes[pixelOffset + 1] = (byte) this[row, column].Green;
                    bytes[pixelOffset + 2] = (byte) this[row, column].Red;
                }
            }
        }

        private void WriteImageInformationHeader(byte[] bytes)
        {
            bytes[BitmapHeaderSize] = BitmapInformationHeaderSize;
            WriteLittleEndian(bytes, 18, Width);
            WriteLittleEndian(bytes, 22, Height);
            bytes[26] = 0x01;
            WriteLittleEndian(bytes, 28, ColorDepth);

            WriteLittleEndian(bytes, 34, _rowByteSize * Height);
        }

        private void WriteImageHeader(byte[] bytes)
        {
            bytes[0] = 0x42;
            bytes[1] = 0x4D;

            WriteLittleEndian(bytes, 2, Size);
            WriteLittleEndian(bytes, 10, Offset);
        }

        private void WriteLittleEndian(byte[] data, int index, int value)
        {
            data[index] = (byte)(value & 0xff);
            data[index + 1] = (byte)(value >> 8 & 0xff);
            data[index + 2] = (byte)(value >> 16 & 0xff);
            data[index + 3] = (byte)(value >> 24 & 0xff);
        }

        public void WriteToFile(string path)
        {
            File.WriteAllBytes(path, GetBytes());
        }
    }
}
