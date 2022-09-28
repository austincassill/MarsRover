using MarsRover;
using NUnit.Framework;

namespace MarsRoverTests
{
    [TestFixture]
    public class Tests
    {
        private Plateau _plateau;

        [SetUp]
        public void Setup()
        {
            _plateau = new Plateau(5, 7); 
        }

        [TestCase(3, 4, true)]
        [TestCase(5, 4, true)]
        [TestCase(3, 7, true)]
        [TestCase(6, 8, false)]
        [TestCase(3, 8, false)]
        [TestCase(6, 4, false)]
        public void ValidCoordinates_Validates_Correctly(int longitude, int latitude, bool expected)
        {
            var result = _plateau.ValidCoordinates(longitude, latitude);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}