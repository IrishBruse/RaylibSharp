namespace RaylibSharp;

using System.Runtime.InteropServices;

/// <summary> Logging: Redirect trace log messages </summary>
public unsafe delegate void TraceLogCallback(TraceLogLevel msgType, [MarshalAs(UnmanagedType.LPStr)] string text);

/// <summary> FileIO: Load binary data </summary>
public unsafe delegate byte* LoadFileDataCallback([MarshalAs(UnmanagedType.LPStr)] string fileName, uint* bytesRead);

/// <summary> FileIO: Save binary data </summary>
public delegate bool SaveFileDataCallback([MarshalAs(UnmanagedType.LPStr)] string fileName, IntPtr data, uint bytesToWrite);

/// <summary> FileIO: Load text data </summary>
public delegate IntPtr LoadFileTextCallback([MarshalAs(UnmanagedType.LPStr)] string fileName);

/// <summary> FileIO: Save text data </summary>
public delegate bool SaveFileTextCallback([MarshalAs(UnmanagedType.LPStr)] string fileName, IntPtr text);

/// <summary> Audio: Audio callback </summary>
public delegate void AudioCallback(IntPtr bufferData, uint frames);
