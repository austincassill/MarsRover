using System.Linq;
using MarsRover.Interfaces;

namespace MarsRover
{
    public class Rover : IRover
    {
        private static readonly string[] ValidDirections = { "N", "S", "E", "W" };
        private readonly IConsoleWrapper _console;
        private readonly IPlateau _plateau;
        private string _direction;
        private int _latitude;
        private int _longitude;

        public Rover(IPlateau plateau, IConsoleWrapper console)
        {
            _plateau = plateau;
            _console = console;
        }

        public void DeploymentValidation()
        {
            while (true)
            {
                _console.WriteLine("Please enter starting longitude, latitude, and direction. (f.e. 3 4 N)");
                var input = _console.ReadLine().Split(' ');

                if (!int.TryParse(input[0], out var longitude))
                {
                    _console.WriteLine("Longitude is invalid. Must be an integer.");
                    continue;
                }

                if (!int.TryParse(input[1], out var latitude))
                {
                    _console.WriteLine("Latitude is invalid. Must be an integer.");
                    continue;
                }

                if (!ValidDirections.Contains(input[2]))
                    _console.WriteLine("Direction must be N (north), S (south), E (east), or W (west).");

                if (!_plateau.ValidCoordinates(longitude, latitude))
                {
                    _console.WriteLine("Rover location is outside the bounds of the plateau.");
                    continue;
                }

                _longitude = longitude;
                _latitude = latitude;
                _direction = input[2];
                break;
            }
        }
    }
}