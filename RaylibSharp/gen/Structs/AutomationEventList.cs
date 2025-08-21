namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Automation event list </summary>
public unsafe partial struct AutomationEventList
{
    /// <summary> Events max entries (MAX_AUTOMATION_EVENTS) </summary>
    public uint Capacity;
    /// <summary> Events entries count </summary>
    public uint Count;
    /// <summary> Events entries </summary>
    public AutomationEvent* Events;
}

/// <summary> Automation event list </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct UnmanagedAutomationEventList
{
    /// <summary> Events max entries (MAX_AUTOMATION_EVENTS) </summary>
    public uint Capacity;
    /// <summary> Events entries count </summary>
    public uint Count;
    /// <summary> Events entries </summary>
    public UnmanagedAutomationEvent* Events;
}

#pragma warning restore CA1711,IDE0005
