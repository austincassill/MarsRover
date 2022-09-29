using System.Linq;
using System.Runtime.CompilerServices;
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
        public char Direction { get; private set; }
        public int Latitude { get; private set; }
        public int Longitude { get; private set; }

        public Rover(IPlateau plateau, IConsoleWrapper console)
        {
            _plateau = plateau;
            _console = console;
        }

        public void Execute()
        {
            string input;
            do
            {
                _console.WriteLine("Please enter a command string. (f.e. MMRMLMMR)");
                input = _console.ReadLine();
            } while (!ValidCommand(input));
        }

        private bool ValidCommand(string input)
        {
            var valid = true;
            var tempRover = new Rover(_plateau, _console);

            foreach (var command in input)
            {
                if (command == 'M' || command == 'R' || command == 'L')
                {
                    tempRover = ProcessCommand(command);
                    continue;
                }
                _console.WriteLine($"{command} is not a valid command. Please enter a string of M, R, or L.");
                valid = false;
                break;
            }

            if (tempRover.Longitude > _plateau.Length || tempRover.Latitude > _plateau.Width)
            {
                _console.WriteLine($"Rover location ({tempRover.Longitude}, {tempRover.Latitude}) is out of bounds of the plateau ({_plateau.Length}, {_plateau.Width})");
                valid = false;
            }
            else
            {
                Longitude = tempRover.Longitude;
                Latitude = tempRover.Latitude;
                _console.WriteLine($"Rover has successfully driven to ({Longitude}, {Latitude})");
                _console.ReadLine();
            }

            return valid;
        }

        // TODO: fix bug of properties getting reset every run
        private Rover ProcessCommand(char command)
        {
            var tempRover = new Rover(_plateau, _console);
            tempRover.Direction = Direction;
            tempRover.Latitude = Latitude;
            tempRover.Longitude = Longitude;

            switch (tempRover.Direction)
            {
                case 'N':
                    switch (command)
                    {
                        case 'R':
                            tempRover.Direction = 'E';
                            break;
                        case 'L':
                            tempRover.Direction = 'W';
                            break;
                        default:
                            tempRover.Latitude += 1;
                            break;
                    }
                    break;
                case 'S':
                    switch (command)
                    {
                        case 'R':
                            tempRover.Direction = 'W';
                            break;
                        case 'L':
                            tempRover.Direction = 'E';
                            break;
                        default:
                            tempRover.Latitude -= 1;
                            break;
                    }
                    break;
                case 'E':
                    switch (command)
                    {
                        case 'R':
                            tempRover.Direction = 'S';
                            break;
                        case 'L':
                            tempRover.Direction = 'N';
                            break;
                        default:
                            tempRover.Longitude += 1;
                            break;
                    }
                    break;
                default:
                    switch (command)
                    {
                        case 'R':
                            tempRover.Direction = 'N';
                            break;
                        case 'L':
                            tempRover.Direction = 'S';
                            break;
                        default:
                            tempRover.Longitude -= 1;
                            break;
                    }
                    break;
            }

            return tempRover;
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
            Direction = input[2][0];
            return valid;
        }
    }
}