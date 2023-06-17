public static class Utility
{
    public static int GetRandomValue(int min, int max)
    {
        return Random.Shared.Next(min, max + 1);
    }

    public static float GetRandomValue(float min, float max)
    {
        return (Random.Shared.NextSingle() * (max - min)) - min;
    }
}
