using System;
using MarsRover.Interfaces;

namespace MarsRover
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var missionControl = new MissionControl();
            missionControl.Launch();
        }
    }
}
