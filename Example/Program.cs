namespace Example;

using System;
using System.Reflection;

using RaylibSharp;

public static class Program
{
    private static Func<int>[,] actions = {
        { CoreBasicWindow.Example, CoreInputKeys.Example },
        { CoreBasicWindow.Example, CoreInputKeys.Example },
    };

    public static void Main()
    {
        // Core2dCamera.Example();
        // Core2dCameraMouseZoom.Example();
        // CoreDropFiles.Example();
        // CoreRandomValues.Example();
        // Core2dCameraPlatformer.Example();
        // Core3dCameraFirstPerson.Example();
        CoreCustomLogging.Example();
    }
}
