namespace RaylibSharp;

public static unsafe partial class Raylib
{
    /// <summary> Just calls C# Random function used mostly for examples <br/> Random.Shared.Next(min, max + 1) </summary>
    public static int GetRandomValue(int min, int max)
    {
        return Random.Shared.Next(min, max + 1);
    }

    /// <summary> Just calls C# Random function used mostly for examples <br/> Random.Shared.NextSingle() * (max - min) + min </summary>
    public static float GetRandomValue(float min, float max)
    {
        return (Random.Shared.NextSingle() * (max - min)) + min;
    }
}
