using MarsRover;
using MarsRover.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace MarsRoverTests
{
    [TestFixture]
    public class RoverTests
    {
        private IPlateau _plateau;
        private IConsoleWrapper _console;
        private Rover _rover;

        [SetUp]
        public void Setup()
        {
            _plateau = Substitute.For<IPlateau>();
            _console = Substitute.For<IConsoleWrapper>();
            _rover = new Rover(_plateau, _console);
        }

        [Test]
        public void DeploymentValidation_Returns_No_Errors()
        {
            _console.ReadLine().Returns("3 5 N");
            _plateau.ValidCoordinates(Arg.Any<int>(), Arg.Any<int>()).Returns(true);

            _rover.Deploy();

            _console.Received(1).WriteLine("Please enter starting longitude, latitude, and direction. (f.e. 3 4 N)");
            _console.DidNotReceive().WriteLine("Longitude is invalid. Must be a positive integer.");
            _console.DidNotReceive().WriteLine("Latitude is invalid. Must be a positive integer.");
            _console.DidNotReceive().WriteLine("Direction must be N (north), S (south), E (east), or W (west).");
            _console.DidNotReceive().WriteLine("Rover location is outside the bounds of the plateau.");

            Assert.That(_rover.Longitude, Is.EqualTo(3));
            Assert.That(_rover.Latitude, Is.EqualTo(5));
            Assert.That(_rover.Direction, Is.EqualTo("N"));
        }

        [TestCase("A 5 N", "Longitude is invalid. Must be a positive integer.", true)]
        [TestCase("-3 5 N", "Longitude is invalid. Must be a positive integer.", true)]
        [TestCase("3 B N", "Latitude is invalid. Must be a positive integer.", true)]
        [TestCase("3 -5 N", "Latitude is invalid. Must be a positive integer.", true)]
        [TestCase("3 5 C", "Direction must be N (north), S (south), E (east), or W (west).", true)]
        [TestCase("3 5 N", "Rover location is outside the bounds of the plateau (0, 0).", false)]
        public void DeploymentValidation_Returns_Error(string input, string expected, bool validCoordinates)
        {
            _console.ReadLine().Returns(input, "3 5 N");
            _plateau.ValidCoordinates(Arg.Any<int>(), Arg.Any<int>()).Returns(validCoordinates, true);

            _rover.Deploy();

            _console.Received(2).WriteLine("Please enter starting longitude, latitude, and direction. (f.e. 3 4 N)");
            _console.Received(1).WriteLine(expected);
        }

        [Test]
        public void DeploymentValidation_Returns_AllErrors()
        {
            _console.ReadLine().Returns("A B C", "3 5 N");
            _plateau.ValidCoordinates(Arg.Any<int>(), Arg.Any<int>()).Returns(false, true);

            _rover.Deploy();

            _console.Received(2).WriteLine("Please enter starting longitude, latitude, and direction. (f.e. 3 4 N)");
            _console.Received(1).WriteLine("Longitude is invalid. Must be a positive integer.");
            _console.Received(1).WriteLine("Latitude is invalid. Must be a positive integer.");
            _console.Received(1).WriteLine("Direction must be N (north), S (south), E (east), or W (west).");
            _console.Received(1).WriteLine("Rover location is outside the bounds of the plateau (0, 0).");

            Assert.That(_rover.Longitude, Is.EqualTo(3));
            Assert.That(_rover.Latitude, Is.EqualTo(5));
            Assert.That(_rover.Direction, Is.EqualTo("N"));
        }
    }
}
