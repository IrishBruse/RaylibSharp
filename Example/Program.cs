namespace Example;

using System.Drawing;

using Raylib;


public class Program
{
    public static void Main()
    {
        RL.InitWindow(800, 450, "Raylib [core] example - basic window");
        RL.SetTargetFPS(60);

        while (!RL.WindowShouldClose())
        {
            RL.BeginDrawing();
            {
                RL.ClearBackground(Color.White);
                RL.DrawText("Congrats! You created your first window!", 190, 200, 20, Color.LightGray);
            }
            RL.EndDrawing();
        }

        RL.CloseWindow();
    }
}
