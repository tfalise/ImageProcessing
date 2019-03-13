using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;

namespace ImageProcessing.Tests
{
    public class MyImageTests
    {
        [Test]
        public void Should_read_bitmap_header_when_creating_a_new_image_from_file_data()
        {
            var image = new MyImage(TestImages.BlackAndWhiteSquare);

            Check.That(image.ImageType).IsEqualTo(ImageType.Bitmap);
            Check.That(image.Size).IsEqualTo(TestImages.BlackAndWhiteSquare.Length);
            Check.That(image.Offset).IsEqualTo(54);
            Check.That(image.Width).IsEqualTo(20);
            Check.That(image.Height).IsEqualTo(20);
            Check.That(image.ColorDepth).IsEqualTo(24);
        }

        [Test]
        public void Should_read_image_data_when_creating_a_new_image_from_file_data()
        {
            var image = new MyImage(TestImages.BlackAndWhiteSquare);

            Check.That(image.Pixels).HasSize(image.Width * image.Height);
            Check.That(image.Pixels).Contains(new Pixel(255, 255, 255));
        }

        [Test]
        public void Should_read_image_data_correctly_when_image_data_contains_padding_bytes()
        {
            var image = new MyImage(TestImages.ImageWithRowPaddingBytes);

            Check.That(image.Pixels).HasSize(4);
            Check.That(image.Pixels).ContainsExactly(
                new Pixel(255, 0, 0),
                new Pixel(0, 255, 0),
                new Pixel(0, 0, 255),
                new Pixel(255, 255, 255));
        }

        [Test]
        public void Should_access_image_pixels_when_using_indexer()
        {
            var image = new MyImage(TestImages.ImageWithRowPaddingBytes);

            Check.That(image[1, 0]).IsEqualTo(image.Pixels[2]);
        }

        [Test]
        public void Should_write_bitmap_header_correctly_when_writing_image()
        {
            var image = new MyImage(TestImages.ImageWithRowPaddingBytes);
            var bytes = image.GetBytes();

            Check.That(bytes.Take(14)).ContainsExactly(0x42, 0x4d, 0x46, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x36, 0x00, 0x00, 0x00);
            Check.That(bytes.Skip(14).Take(40)).ContainsExactly(0x28, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x18, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00);
        }

        [Test]
        public void Should_write_image_data_when_writing_image()
        {
            var image = new MyImage(TestImages.ImageWithRowPaddingBytes);
            var bytes = image.GetBytes();

            Check.That(bytes.Skip(54)).ContainsExactly(
                0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00,
                0xFF, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00);
        }
    }
}
