using System;
using MarsRover.Interfaces;

namespace MarsRover
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO: reorganize all this into their own classes - maybe the plateau class?
            Console.WriteLine("Please enter the length and width of the plateau. (f.e. 10 12)");
            var input = Console.ReadLine().Split(' ');
            var plateau = new Plateau(int.Parse(input[0]), int.Parse(input[1]));
            var rover = new Rover(plateau, new ConsoleWrapper());
            rover.DeploymentValidation();

        }
    }
}
