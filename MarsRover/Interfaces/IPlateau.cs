using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Interfaces
{
    public interface IPlateau
    {
        int Length { get; }
        int Width { get; }
        void Build();
        bool ValidCoordinates(int latitude, int longitude);
    }
}
