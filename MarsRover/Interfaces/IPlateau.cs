using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Interfaces
{
    public interface IPlateau
    {
        bool ValidCoordinates(int latitude, int longitude);
    }
}
