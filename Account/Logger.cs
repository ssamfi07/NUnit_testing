using System;

namespace bank
{
public class Logger: ILogger
{
    public void Log(String message)
    {
        Console.WriteLine(message);
    }
};
} //namespace bank