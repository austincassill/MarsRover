using MarsRover.Interfaces;
using System;

namespace MarsRover
{
    public class Plateau : IPlateau
    {
        public int Length { get; private set; }
        public int Width { get; private set; }
        private readonly IConsoleWrapper _console;

        public Plateau(IConsoleWrapper console)
        {
            _console = console;
        }

        public void Build()
        {
            string[] input;
            do
            {
                _console.WriteLine("Please enter the length and width of the plateau. (f.e. 10 12)");
                input = _console.ReadLine().Split(' ');
            } while (!ValidPlateau(input));

        }

        private bool ValidPlateau(string[] input)
        {
            var valid = true;
            if (!int.TryParse(input[0], out var length) || length <= 0)
            {
                _console.WriteLine("Length is invalid. Must be a positive integer.");
                valid = false;
            }
            else
            {
                Length = length;
            }
            if (!int.TryParse(input[1], out var width) || width <= 0)
            {
                _console.WriteLine("Width is invalid. Must be a positive integer.");
                valid = false;
            }
            else
            {
                Width = width;
            }
            return valid;
        }

        public bool ValidCoordinates(int longitude, int latitude)
        {
            return Length >= longitude && Width >= latitude;
        }


    }
}
