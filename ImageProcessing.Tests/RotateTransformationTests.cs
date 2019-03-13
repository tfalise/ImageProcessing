using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using NUnit.Framework;

namespace ImageProcessing.Tests
{
    class RotateTransformationTests
    {
        [TestCase(ImageRotation.Clockwise90)]
        [TestCase(ImageRotation.Clockwise270)]
        public void Should_switch_dimensions_when_rotation_is_90_or_270_degrees(ImageRotation rotation)
        {
            var source = new MyImage(2, 4);
            var transformation = new RotateTransformation(rotation);
            var actual = transformation.Process(source);

            Check.That(actual.Width).IsEqualTo(source.Height);
            Check.That(actual.Height).IsEqualTo(source.Width);
        }

        [TestCase(ImageRotation.None)]
        [TestCase(ImageRotation.Clockwise180)]
        public void Should_preserve_dimensions_when_rotation_is_0_or_180_degrees(ImageRotation rotation)
        {
            var source = new MyImage(2, 4);
            var transformation = new RotateTransformation(rotation);
            var actual = transformation.Process(source);

            Check.That(actual.Width).IsEqualTo(source.Width);
            Check.That(actual.Height).IsEqualTo(source.Height);
        }
    }
}
