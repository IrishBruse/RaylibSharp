namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Automation event </summary>
public unsafe partial struct AutomationEvent
{
    /// <summary> Event frame </summary>
    public uint Frame;
    /// <summary> Event type (AutomationEventType) </summary>
    public uint Type;
    /// <summary> Event parameters (if required) </summary>
    public fixed int Params[4];
}

/// <summary> Automation event </summary>
[StructLayout(LayoutKind.Sequential)]
unsafe struct UnmanagedAutomationEvent
{
    /// <summary> Event frame </summary>
    public uint Frame;
    /// <summary> Event type (AutomationEventType) </summary>
    public uint Type;
    /// <summary> Event parameters (if required) </summary>
    public fixed int Params[4];
}

#pragma warning restore CA1711,IDE0005
