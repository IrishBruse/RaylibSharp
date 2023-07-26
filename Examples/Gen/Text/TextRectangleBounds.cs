using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TextRectangleBounds : ExampleHelper 
{

    static static void DrawTextBoxed(Font font, string text, RectangleF rec, float fontSize, float spacing, bool wordWrap, Color tint);   // Draw text using font inside rectangle limits
    static static void DrawTextBoxedSelectable(Font font, string text, RectangleF rec, float fontSize, float spacing, bool wordWrap, Color tint, int selectStart, int selectLength, Color selectTint, Color selectBackTint);    // Draw text using font inside rectangle limits with support for text selection

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - text - draw text inside a rectangle");

        const char text[] = "Text cannot escape\tthis container\t...word wrap also works when active so here's \
    a long text for testing.\n\nLorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod \
    tempor incididunt ut labore et dolore magna aliqua. Nec ullamcorper sit amet risus nullam eget felis eget.";

        bool resizing = false;
        bool wordWrap = true;

        RectangleF container = new( 25.0f, 25.0f, screenWidth - 50.0f, screenHeight - 250.0f );
        RectangleF resizer = new( container.X + container.Width - 17, container.Y + container.Height - 17, 14, 14 );

        // Minimum width and heigh for the container rectangle
        const float minWidth = 60;
        const float minHeight = 60;
        const float maxWidth = screenWidth - 50.0f;
        const float maxHeight = screenHeight - 160.0f;

        Vector2 lastMouse = new( 0.0f, 0.0f ); // Stores last mouse coordinates
        Color borderColor = Maroon;         // Container border color
        Font font = GetFontDefault();       // Get default system font

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            if (IsKeyPressed(Key.Space)) wordWrap = !wordWrap;

            Vector2 mouse = GetMousePosition();

            // Check if the mouse is inside the container and toggle border color
            if (CheckCollisionPoint(mouse, container)) borderColor = Fade(Maroon, 0.4f);
            else if (!resizing) borderColor = Maroon;

            // Container resizing logic
            if (resizing)
            {
                if (IsMouseButtonReleased(MouseButton.Left)) resizing = false;

                float width = container.Width + (mouse.X - lastMouse.X);
                container.Width = (width > minWidth)? ((width < maxWidth)? width : maxWidth) : minWidth;

                float height = container.Height + (mouse.Y - lastMouse.Y);
                container.Height = (height > minHeight)? ((height < maxHeight)? height : maxHeight) : minHeight;
            }
            else
            {
                // Check if we're resizing
                if (IsMouseButtonDown(MouseButton.Left) && CheckCollisionPoint(mouse, resizer)) resizing = true;
            }

            // Move resizer rectangle properly
            resizer.X = container.X + container.Width - 17;
            resizer.Y = container.Y + container.Height - 17;

            lastMouse = mouse; // Update mouse

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawRectangleLines(container, 3, borderColor);    // Draw container border

                // Draw text in container (add some padding)
                DrawTextBoxed(font, text, new( container.X + 4, container.Y + 4, container.Width - 4, container.Height - 4 ), 20.0f, 2.0f, wordWrap, Gray);

                DrawRectangle(resizer, borderColor);             // Draw the resize box

                // Draw bottom info
                DrawRectangle(0, screenHeight - 54, screenWidth, 54, Gray);
                DrawRectangle(new( 382.0f, screenHeight - 34.0f, 12.0f, 12.0f ), Maroon);

                DrawText("Word Wrap: ", 313, screenHeight-115, 20, Black);
                if (wordWrap) DrawText("ON", 447, screenHeight - 115, 20, Red);
                else DrawText("OFF", 447, screenHeight - 115, 20, Black);

                DrawText("Press [SPACE] to toggle word wrap", 218, screenHeight - 86, 20, Gray);

                DrawText("Click hold & drag the    to resize the container", 155, screenHeight - 38, 20, RayWhite);

            }EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }

    // Module functions definition

    // Draw text using font inside rectangle limits
    static static void DrawTextBoxed(Font font, string text, RectangleF rec, float fontSize, float spacing, bool wordWrap, Color tint)
    {
        DrawTextBoxedSelectable(font, text, rec, fontSize, spacing, wordWrap, tint, 0, 0, White, White);
    }

    // Draw text using font inside rectangle limits with support for text selection
    static static void DrawTextBoxedSelectable(Font font, string text, RectangleF rec, float fontSize, float spacing, bool wordWrap, Color tint, int selectStart, int selectLength, Color selectTint, Color selectBackTint)
    {
        int length = TextLength(text);  // Total length in bytes of the text, scanned by codepoints in loop

        float textOffsetY = 0;          // Offset between lines (on line break '\n')
        float textOffsetX = 0.0f;       // Offset X to next character to draw

        float scaleFactor = fontSize/(float)font.baseSize;     // Character rectangle scaling factor

        // Word/character wrapping mechanism variables
        enum { MEASURE_STATE = 0, DRAW_STATE = 1 };
        int state = wordWrap? MEASURE_STATE : DRAW_STATE;

        int startLine = -1;         // Index where to begin drawing (where a line begins)
        int endLine = -1;           // Index where to stop drawing (where a line ends)
        int lastk = -1;             // Holds last value of the character position

        for (int i = 0, k = 0; i < length; i++, k++)
        {
            // Get next codepoint from byte string and glyph index in font
            int codepointByteCount = 0;
            int codepoint = GetCodepoint(&text[i], &codepointByteCount);
            int index = GetGlyphIndex(font, codepoint);

            // NOTE: Normally we exit the decoding sequence as soon as a bad byte is found (and return 0x3f)
            // but we need to draw all of the bad bytes using the '?' symbol moving one byte
            if (codepoint == 0x3f) codepointByteCount = 1;
            i += (codepointByteCount - 1);

            float glyphWidth = 0;
            if (codepoint != '\n')
            {
                glyphWidth = (font.glyphs[index].advanceX == 0) ? font.recs[index].Width*scaleFactor : font.glyphs[index].advanceX*scaleFactor;

                if (i + 1 < length) glyphWidth = glyphWidth + spacing;
            }

            // NOTE: When wordWrap is ON we first measure how much of the text we can draw before going outside of the rec container
            // We store this info in startLine and endLine, then we change states, draw the text between those two variables
            // and change states again and again recursively until the end of the text (or until we get outside of the container).
            // When wordWrap is OFF we don't need the measure state so we go to the drawing state immediately
            // and begin drawing on the next line before we can get outside the container.
            if (state == MEASURE_STATE)
            {
                // TODO: There are multiple types of spaces in UNICODE, maybe it's a good idea to add support for more
                // Ref: http://jkorpela.fi/chars/spaces.html
                if ((codepoint == ' ') || (codepoint == '\t') || (codepoint == '\n')) endLine = i;

                if ((textOffsetX + glyphWidth) > rec.Width)
                {
                    endLine = (endLine < 1)? i : endLine;
                    if (i == endLine) endLine -= codepointByteCount;
                    if ((startLine + codepointByteCount) == endLine) endLine = (i - codepointByteCount);

                    state = !state;
                }
                else if ((i + 1) == length)
                {
                    endLine = i;
                    state = !state;
                }
                else if (codepoint == '\n') state = !state;

                if (state == DRAW_STATE)
                {
                    textOffsetX = 0;
                    i = startLine;
                    glyphWidth = 0;

                    // Save character position when we switch states
                    int tmp = lastk;
                    lastk = k - 1;
                    k = tmp;
                }
            }
            else
            {
                if (codepoint == '\n')
                {
                    if (!wordWrap)
                    {
                        textOffsetY += (font.baseSize + font.baseSize/2)*scaleFactor;
                        textOffsetX = 0;
                    }
                }
                else
                {
                    if (!wordWrap && ((textOffsetX + glyphWidth) > rec.Width))
                    {
                        textOffsetY += (font.baseSize + font.baseSize/2)*scaleFactor;
                        textOffsetX = 0;
                    }

                    // When text overflows rectangle height limit, just stop drawing
                    if ((textOffsetY + font.baseSize*scaleFactor) > rec.Height) break;

                    // Draw selection background
                    bool isGlyphSelected = false;
                    if ((selectStart >= 0) && (k >= selectStart) && (k < (selectStart + selectLength)))
                    {
                        DrawRectangle(new( rec.X + textOffsetX - 1, rec.Y + textOffsetY, glyphWidth, (float)font.baseSize*scaleFactor ), selectBackTint);
                        isGlyphSelected = true;
                    }

                    // Draw current character glyph
                    if ((codepoint != ' ') && (codepoint != '\t'))
                    {
                        DrawTextCodepoint(font, codepoint, new( rec.X + textOffsetX, rec.Y + textOffsetY ), fontSize, isGlyphSelected? selectTint : tint);
                    }
                }

                if (wordWrap && (i == endLine))
                {
                    textOffsetY += (font.baseSize + font.baseSize/2)*scaleFactor;
                    textOffsetX = 0;
                    startLine = endLine;
                    endLine = -1;
                    glyphWidth = 0;
                    selectStart += lastk - k;
                    k = lastk;

                    state = !state;
                }
            }

            if ((textOffsetX != 0) || (codepoint != ' ')) textOffsetX += glyphWidth;  // avoid leading spaces
        }
    }
}
