namespace RaylibSharp;

/// <summary> Logging: Redirect trace log messages </summary>
public delegate void TraceLogCallback(int /* int */ logLevel, string /* const char * */ text, params object[] /* va_list */ args);

/// <summary> FileIO: Load binary data </summary>
public unsafe delegate byte* LoadFileDataCallback(string /* const char * */ fileName, uint* /* unsigned int * */ bytesRead);

/// <summary> FileIO: Save binary data </summary>
public delegate bool SaveFileDataCallback(string /* const char * */ fileName, IntPtr /* void * */ data, uint /* unsigned int */ bytesToWrite);

/// <summary> FileIO: Load text data </summary>
public delegate IntPtr LoadFileTextCallback(string /* const char * */ fileName);

/// <summary> FileIO: Save text data </summary>
public delegate bool SaveFileTextCallback(string /* const char * */ fileName, IntPtr /* char * */ text);

/// <summary>  </summary>
public delegate void AudioCallback(IntPtr /* void * */ bufferData, uint /* unsigned int */ frames);
