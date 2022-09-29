using System;
using MarsRover.Interfaces;

namespace MarsRover
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteLine(string input)
        {
            Console.WriteLine(input);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
