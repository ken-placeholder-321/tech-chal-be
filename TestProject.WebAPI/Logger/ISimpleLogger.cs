using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.WebAPI.Logger
{
    public interface ISimpleLogger
    {
        public void Log(string message);
    }
}
