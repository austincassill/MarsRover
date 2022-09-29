using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using MarsRover.Interfaces;

namespace MarsRover
{
    public class Rover : IRover
    {
        private static readonly string[] ValidDirections = { "N", "S", "E", "W" };
        private readonly IConsoleWrapper _console;
        private readonly IPlateau _plateau;
        public string Direction { get; private set; }
        public int Latitude { get; private set; }
        public int Longitude { get; private set; }

        public Rover(IPlateau plateau, IConsoleWrapper console)
        {
            _plateau = plateau;
            _console = console;
        }

        public void Deploy()
        {
            string[] input;
            do
            {
                _console.WriteLine("Please enter starting longitude, latitude, and direction. (f.e. 3 4 N)");
                input = _console.ReadLine().Split(' ');
            } while (!ValidDeployment(input));
            
        }

        private bool ValidDeployment(string[] input)
        {
            var valid = true;

            if (!int.TryParse(input[0], out var longitude) || longitude <= 0)
            {
                _console.WriteLine("Longitude is invalid. Must be a positive integer.");
                valid = false;
            }

            if (!int.TryParse(input[1], out var latitude) || latitude <= 0)
            {
                _console.WriteLine("Latitude is invalid. Must be a positive integer.");
                valid = false;
            }

            if (!ValidDirections.Contains(input[2].ToUpper()))
            {
                _console.WriteLine("Direction must be N (north), S (south), E (east), or W (west).");
                valid = false;
            }

            if (!_plateau.ValidCoordinates(longitude, latitude))
            {
                _console.WriteLine($"Rover location is outside the bounds of the plateau ({_plateau.Length}, {_plateau.Width}).");
                valid = false;
            }

            Longitude = longitude;
            Latitude = latitude;
            Direction = input[2];
            return valid;
        }
    }
}