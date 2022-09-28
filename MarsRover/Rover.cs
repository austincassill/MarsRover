using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Interfaces;

namespace MarsRover
{
    public class Rover : IRover
    {
        private static readonly string[] ValidDirections = {"N", "S", "E", "W"};
        private readonly IPlateau _plateau;
        private readonly IConsoleWrapper _console;
        private int _longitude;
        private int _latitude;
        private string _direction;
        public Rover(IPlateau plateau, IConsoleWrapper console)
        {
            _plateau = plateau;
            _console = console;
        }

        public void DeployRover()
        {
            while (true)
            {
                _console.WriteLine("Please enter starting longitude, latitude, and direction. (f.e. 3 4 N)");
                var input = _console.ReadLine().Split(' ');
                var longitude = int.Parse(input[0]);
                var latitude = int.Parse(input[1]);
                if (_plateau.ValidCoordinates(longitude, latitude) && ValidDirections.Contains(input[2]))
                {
                    _longitude = longitude;
                    _latitude = latitude;
                    _direction = input[2];
                    break;
                }
                else
                {
                    _console.WriteLine("Invalid Input.");
                }
            }
        }
    }
}
