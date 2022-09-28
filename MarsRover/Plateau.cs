using MarsRover.Interfaces;

namespace MarsRover
{
    public class Plateau : IPlateau
    {
        private readonly int _length;
        private readonly int _width;

        public Plateau(int length, int width)
        {
            _length = length;
            _width = width;
        }

        public bool ValidCoordinates(int longitude, int latitude)
        {
            return _length >= longitude && _width >= latitude;
        }


    }
}
