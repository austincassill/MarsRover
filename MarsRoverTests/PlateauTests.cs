using MarsRover;
using MarsRover.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace MarsRoverTests
{
    [TestFixture]
    public class Tests
    {
        private IConsoleWrapper _console;
        private Plateau _plateau;

        [SetUp]
        public void Setup()
        {
            _console = Substitute.For<IConsoleWrapper>();
            _plateau = new Plateau(_console); 
        }

        [Test]
        public void BuildPlateau_Returns_NoErrors()
        {
            _console.ReadLine().Returns("5 7");

            _plateau.BuildPlateau();

            _console.Received(1).WriteLine("Please enter the length and width of the plateau. (f.e. 10 12)");
            _console.DidNotReceive().WriteLine("Length is invalid. Must be a positive integer.");
            _console.DidNotReceive().WriteLine("Width is invalid. Must be a positive integer.");

            Assert.That(_plateau.Length, Is.EqualTo(5));
            Assert.That(_plateau.Width, Is.EqualTo(7));
        }

        [TestCase("A 7", "Length is invalid. Must be a positive integer.", "Width is invalid. Must be a positive integer.")]
        [TestCase("-2 7", "Length is invalid. Must be a positive integer.", "Width is invalid. Must be a positive integer.")]
        [TestCase("5 B", "Width is invalid. Must be a positive integer.","Length is invalid. Must be a positive integer.")]
        [TestCase("5 -3", "Width is invalid. Must be a positive integer.", "Length is invalid. Must be a positive integer." )]
        public void BuildPlateau_Returns_Error(string badInput, string expected, string notExpected)
        {
            _console.ReadLine().Returns(badInput, "5 7");

            _plateau.BuildPlateau();

            _console.Received(2).WriteLine("Please enter the length and width of the plateau. (f.e. 10 12)");
            _console.Received(1).WriteLine(expected);
            _console.DidNotReceive().WriteLine(notExpected);
        }

        [TestCase("A B")]
        [TestCase("-2 -3")]
        public void BuildPlateau_Returns_AllErrors(string badInput)
        {
            _console.ReadLine().Returns(badInput, "5 7");

            _plateau.BuildPlateau();

            _console.Received(2).WriteLine("Please enter the length and width of the plateau. (f.e. 10 12)");
            _console.Received(1).WriteLine("Length is invalid. Must be a positive integer.");
            _console.Received(1).WriteLine("Width is invalid. Must be a positive integer.");
        }





        [TestCase(3, 4, true)]
        [TestCase(5, 4, true)]
        [TestCase(3, 7, true)]
        [TestCase(6, 8, false)]
        [TestCase(3, 8, false)]
        [TestCase(6, 4, false)]
        public void ValidCoordinates_Validates_Correctly(int longitude, int latitude, bool expected)
        {
            _console.ReadLine().Returns("5 7");
            _plateau.BuildPlateau();

            var result = _plateau.ValidCoordinates(longitude, latitude);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}