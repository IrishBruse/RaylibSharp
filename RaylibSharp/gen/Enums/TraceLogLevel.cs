namespace RaylibSharp;

#pragma warning disable CA1711

/// <summary> Trace log level </summary>
public enum TraceLogLevel
{
    /// <summary> Display all logs </summary>
    All = 0,
    /// <summary> Trace logging, intended for internal use only </summary>
    Trace = 1,
    /// <summary> Debug logging, used for internal debugging, it should be disabled on release builds </summary>
    Debug = 2,
    /// <summary> Info logging, used for program execution info </summary>
    Info = 3,
    /// <summary> Warning logging, used on recoverable failures </summary>
    Warning = 4,
    /// <summary> Error logging, used on unrecoverable failures </summary>
    Error = 5,
    /// <summary> Fatal logging, used to abort program: exit(EXIT_FAILURE) </summary>
    Fatal = 6,
    /// <summary> Disable logging </summary>
    None = 7,
}

#pragma warning restore CA1711
