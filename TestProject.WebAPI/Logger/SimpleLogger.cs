using System;
using System.IO;

namespace TestProject.WebAPI.Logger
{
    public class SimpleLogger : ISimpleLogger
    {
        public void Log(string message)
        {
            var logMessage = $"{ DateTime.Now.ToString()}-{message}";
            File.AppendAllText("Logger/logs.txt",
                               logMessage + Environment.NewLine);
        }
    }
}
