using NLog;

namespace LoggerNLog;

public static class Logging
{
    private static readonly Logger userLogger = LogManager.GetLogger("UserActions");
    private static readonly Logger errorLogger = LogManager.GetLogger("Errors");
    
    public static void LogUserAction(string message)
    {
        userLogger.Info(message);
    }

    public static void LogError(Exception ex, string message = "")
    {
        errorLogger.Error(ex, message);
    }
}