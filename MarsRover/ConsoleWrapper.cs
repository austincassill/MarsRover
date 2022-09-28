using System;
namespace MarsRover
{
    public class ConsoleWrapper
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
