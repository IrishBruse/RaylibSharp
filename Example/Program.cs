namespace Example;

using RaylibSharp;

public static class Program
{
    private const int screenWidth = 800;
    private const int screenHeight = 450;

    public static void Main()
    {
        Raylib.InitWindow(screenWidth, screenHeight, "Hello, World!");

        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            {
                Raylib.ClearBackground(Color.RayWhite);

                Raylib.DrawText("Congrats! You created your first window!", 190, 200, 20, Color.LightGray);
            }
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
