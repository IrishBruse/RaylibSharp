namespace Raylib;

#pragma warning disable CA1711

/// <summary> Trace log level </summary>
public enum TraceLogLevel
{
    /// <summary> Display all logs </summary>
    LogAll = 0,
    /// <summary> Trace logging, intended for internal use only </summary>
    LogTrace = 1,
    /// <summary> Debug logging, used for internal debugging, it should be disabled on release builds </summary>
    LogDebug = 2,
    /// <summary> Info logging, used for program execution info </summary>
    LogInfo = 3,
    /// <summary> Warning logging, used on recoverable failures </summary>
    LogWarning = 4,
    /// <summary> Error logging, used on unrecoverable failures </summary>
    LogError = 5,
    /// <summary> Fatal logging, used to abort program: exit(EXIT_FAILURE) </summary>
    LogFatal = 6,
    /// <summary> Disable logging </summary>
    LogNone = 7,
}
#pragma warning restore CA1711
