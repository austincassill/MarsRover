using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Interfaces;

namespace MarsRover
{
    public class MissionControl
    {
        private readonly IPlateau _plateau;
        private readonly IRover _rover;

        public MissionControl()
        {
            var console = new ConsoleWrapper();
            _plateau = new Plateau(console);
            _rover = new Rover(_plateau, console);
        }

        public MissionControl(IPlateau plateau, IRover rover)
        {
            _plateau = plateau;
            _rover = rover;
        }

        public void Launch()
        {
            _plateau.Build();
            _rover.Deploy();
        }
    }
}
