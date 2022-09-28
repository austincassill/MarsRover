using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Interfaces
{
    public interface IConsoleWrapper
    {
        void WriteLine(string input);
        string ReadLine();
    }
}
