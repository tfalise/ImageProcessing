using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;

namespace ImageProcessing.Tests
{
    class BlackAndWhiteTransformationTests
    {
        [Test]
        public void Should_set_pixel_color_to_black_and_white_when_transforming_an_image()
        {
            var image = new MyImage(TestImages.ImageWithRowPaddingBytes);
            var transformation = new BlackAndWhiteTransformation();
            var actual = transformation.Process(image);
            Check.That(actual.Pixels).ContainsOnlyElementsThatMatch(pixel => pixel.Red == pixel.Blue  && pixel.Red == pixel.Green);
        }

        [Test]
        public void Should_use_mean_of_colors_when_calculating_black_and_white_pixel_value()
        {
            var image = new MyImage(TestImages.ImageWithRowPaddingBytes);
            var transformation = new BlackAndWhiteTransformation();
            var actual = transformation.Process(image);

            for (var i = 0; i < image.Pixels.Length; i++)
            {
                var expectedGrayLevel = (image.Pixels[i].Red + image.Pixels[i].Blue + image.Pixels[i].Green) / 3;
                Check.That(actual.Pixels[i].Red).IsEqualTo(expectedGrayLevel);
                Check.That(actual.Pixels[i].Blue).IsEqualTo(expectedGrayLevel);
                Check.That(actual.Pixels[i].Green).IsEqualTo(expectedGrayLevel);
            }
        }

        [Test]
        public void Should_set_correct_headers_when_transforming_an_image()
        {
            var image = new MyImage(TestImages.ImageWithRowPaddingBytes);
            var transformation = new BlackAndWhiteTransformation();
            var actual = transformation.Process(image);
            Check.That(actual.ColorDepth).IsEqualTo(24);
            Check.That(actual.Size).IsEqualTo(image.Size);
            Check.That(actual.Offset).IsEqualTo(image.Offset);
        }

        [Test]
        public void Should_use_average_gray_method_when_specified_in_black_and_white_conversion()
        {
            var image = new MyImage(TestImages.ImageWithAllDifferentPixels);
            var transformation = new BlackAndWhiteTransformation(BlackAndWhiteAlgorithm.Average);
            var actual = transformation.Process(image);
            
            for (var i = 0; i < image.Pixels.Length; i++)
            {
                var expectedGrayLevel = (image.Pixels[i].Red + image.Pixels[i].Blue + image.Pixels[i].Green) / 3;
                Check.That(actual.Pixels[i].Red).IsEqualTo(expectedGrayLevel);
                Check.That(actual.Pixels[i].Blue).IsEqualTo(expectedGrayLevel);
                Check.That(actual.Pixels[i].Green).IsEqualTo(expectedGrayLevel);
            }
        }
    }
}
