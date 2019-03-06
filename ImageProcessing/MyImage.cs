﻿using System;
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
            for (var i = data.Length - 1; i >= Offset; i-=3)
            {
                var iPixel = (i - 54) / 3;
                Pixels[iPixel] = new Pixel(data[i - 2], data[i - 1], data[i]);
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
    }
}