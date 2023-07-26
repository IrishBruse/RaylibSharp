using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TextDraw3d : ExampleHelper 
{

    // To make it work with the older RLGL module just comment the line below


    // Globals
private const int LETTER_BOUNDRY_SIZE = 0.25f;
private const int TEXT_MAX_LAYERS = 32;
private const int LETTER_BOUNDRY_COLOR = Violet;

    bool SHOW_LETTER_BOUNDRY = false;
    bool SHOW_TEXT_BOUNDRY = false;

    // Data Types definition

    // Configuration structure for waving the text
    typedef struct WaveTextConfig {
        Vector3 waveRange;
        Vector3 waveSpeed;
        Vector3 waveOffset;
    } WaveTextConfig;

    // Module Functions Declaration
    // Draw a codepoint in 3D space
    static static void DrawTextCodepoint3D(Font font, int codepoint, Vector3 position, float fontSize, bool backface, Color tint);
    // Draw a 2D text in 3D space
    static static void DrawText3D(Font font, string text, Vector3 position, float fontSize, float fontSpacing, float lineSpacing, bool backface, Color tint);
    // Measure a text in 3D. For some reason `MeasureText()` just doesn't seem to work so i had to use this instead.
    static Vector3 MeasureText3D(Font font, string text, float fontSize, float fontSpacing, float lineSpacing);

    // Draw a 2D text in 3D space and wave the parts that start with `~~` and end with `~~`.
    // This is a modified version of the original code by @Nighten found here https://github.com/NightenDushi/Raylib_DrawTextStyle
    static static void DrawTextWave3D(Font font, string text, Vector3 position, float fontSize, float fontSpacing, float lineSpacing, bool backface, WaveTextConfig *config, float time, Color tint);
    // Measure a text in 3D ignoring the `~~` chars.
    static Vector3 MeasureTextWave3D(Font font, string text, float fontSize, float fontSpacing, float lineSpacing);
    // Generates a nice color with a random hue
    static Color GenerateRandomColor(float s, float v);

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.Msaa4xHint|FLAG_VSYNC_HINT);
        InitWindow(screenWidth, screenHeight, "RaylibSharp - text - draw 2D text in 3D");

        bool spin = true;        // Spin the camera?
        bool multicolor = false; // Multicolor mode

        // Define the camera to look into our 3d world
        Camera camera = new();
        camera.Position = (Vector3)new(-10.0f,15.0f, -10.0f);   // Camera position
        camera.Target = (Vector3)new(0.0f,0.0f, 0.0f);          // Camera looking at point
        camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);              // Camera up vector (rotation towards target)
        camera.Fovy = 45.0f;                                    // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;                 // Camera projection type

        int camera_mode = CameraMode.Orbital;

        Vector3 cubePosition = new( 0.0f, 1.0f, 0.0f );
        Vector3 cubeSize = new( 2.0f, 2.0f, 2.0f );

        // Use the default font
        Font font = GetFontDefault();
        float fontSize = 8.0f;
        float fontSpacing = 0.5f;
        float lineSpacing = -1.0f;

        // Set the text (using markdown!)
        char text[64] = "Hello ~~World~~ in 3D!";
        Vector3 tbox = {0};
        int layers = 1;
        int quads = 0;
        float layerDistance = 0.01f;

        WaveTextConfig wcfg;
        wcfg.waveSpeed.X = wcfg.waveSpeed.Y = 3.0f; wcfg.waveSpeed.Z = 0.5f;
        wcfg.waveOffset.X = wcfg.waveOffset.Y = wcfg.waveOffset.Z = 0.35f;
        wcfg.waveRange.X = wcfg.waveRange.Y = wcfg.waveRange.Z = 0.45f;

        float time = 0.0f;

        // Setup a light and dark color
        Color light = Maroon;
        Color dark = Red;

        // Load the alpha discard shader
        Shader alphaDiscard = LoadShader(null, "resources/shaders/glsl330/alpha_discard.fs");

        // Array filled with multiple random colors (when multicolor mode is set)
        Color [] multi = new Color [TEXT_MAX_LAYERS]{0};

        DisableCursor();                    // Limit cursor to relative movement inside the window

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, camera_mode);

            // Handle font files dropped
            if (IsFileDropped())
            {
                FilePathList droppedFiles = LoadDroppedFiles();

                // NOTE: We only support first ttf file dropped
                if (IsFileExtension(droppedFiles.Paths[0], ".ttf"))
                {
                    UnloadFont(font);
                    font = LoadFont(droppedFiles.Paths[0], (int)fontSize, 0, 0);
                }
                else if (IsFileExtension(droppedFiles.Paths[0], ".fnt"))
                {
                    UnloadFont(font);
                    font = LoadFont(droppedFiles.Paths[0]);
                    fontSize = (float)font.baseSize;
                }

                UnloadDroppedFiles(droppedFiles);    // Unload filepaths from memory
            }

            // Handle Events
            if (IsKeyPressed(Key.F1)) SHOW_LETTER_BOUNDRY = !SHOW_LETTER_BOUNDRY;
            if (IsKeyPressed(Key.F2)) SHOW_TEXT_BOUNDRY = !SHOW_TEXT_BOUNDRY;
            if (IsKeyPressed(Key.F3))
            {
                // Handle camera change
                spin = !spin;
                // we need to reset the camera when changing modes
                camera = (Camera)new();
                camera.Target = (Vector3)new(0.0f,0.0f, 0.0f);          // Camera looking at point
                camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);              // Camera up vector (rotation towards target)
                camera.Fovy = 45.0f;                                    // Camera field-of-view Y
                camera.Projection = CameraProjection.Perspective;                 // Camera mode type

                if (spin)
                {
                    camera.Position = (Vector3)new(-10.0f,15.0f, -10.0f);   // Camera position
                    camera_mode = CameraMode.Orbital;
                }
                else
                {
                    camera.Position = (Vector3)new(10.0f,10.0f, -10.0f);   // Camera position
                    camera_mode = CameraMode.Free;
                }
            }

            // Handle clicking the cube
            if (IsMouseButtonPressed(MouseButton.Left))
            {
                Ray ray = GetMouseRay(GetMousePosition(), camera);

                // Check collision between ray and box
                RayCollision collision = GetRayCollisionBox(ray,
                                (BoundingBox){new( cubePosition.X - cubeSize.X/2, cubePosition.Y - cubeSize.Y/2, cubePosition.Z - cubeSize.Z/2 ),
                                              new( cubePosition.X + cubeSize.X/2, cubePosition.Y + cubeSize.Y/2, cubePosition.Z + cubeSize.Z/2 )});
                if (collision.Hit)
                {
                    // Generate new random colors
                    light = GenerateRandomColor(0.5f, 0.78f);
                    dark = GenerateRandomColor(0.4f, 0.58f);
                }
            }

            // Handle text layers changes
            if (IsKeyPressed(Key.Home)) { if (layers > 1) --layers; }
            else if (IsKeyPressed(Key.End)) { if (layers < TEXT_MAX_LAYERS) ++layers; }

            // Handle text changes
            if (IsKeyPressed(Key.Left)) fontSize -= 0.5f;
            else if (IsKeyPressed(Key.Right)) fontSize += 0.5f;
            else if (IsKeyPressed(Key.Up)) fontSpacing -= 0.1f;
            else if (IsKeyPressed(Key.Down)) fontSpacing += 0.1f;
            else if (IsKeyPressed(Key.PAGE_UP)) lineSpacing -= 0.1f;
            else if (IsKeyPressed(Key.PAGE_DOWN)) lineSpacing += 0.1f;
            else if (IsKeyDown(Key.Insert)) layerDistance -= 0.001f;
            else if (IsKeyDown(Key.Delete)) layerDistance += 0.001f;
            else if (IsKeyPressed(Key.Tab))
            {
                multicolor = !multicolor;   // Enable /disable multicolor mode

                if (multicolor)
                {
                    // Fill color array with random colors
                    for (int i = 0; i < TEXT_MAX_LAYERS; ++i)
                    {
                        multi[i] = GenerateRandomColor(0.5f, 0.8f);
                        multi[i].a = GetRandomValue(0, 255);
                    }
                }
            }

            // Handle text input
            int ch = GetCharPressed();
            if (IsKeyPressed(Key.Backspace))
            {
                // Remove last char
                int len = TextLength(text);
                if (len > 0) text[len - 1] = '\0';
            }
            else if (IsKeyPressed(Key.Enter))
            {
                // handle newline
                int len = TextLength(text);
                if (len < sizeof(text) - 1)
                {
                    text[len] = '\n';
                    text[len+1] ='\0';
                }
            }
            else
            {
                // append only printable chars
                int len = TextLength(text);
                if (len < sizeof(text) - 1)
                {
                    text[len] = ch;
                    text[len+1] ='\0';
                }
            }

            // Measure 3D text so we can center it
            tbox = MeasureTextWave3D(font, text, fontSize, fontSpacing, lineSpacing);

            quads = 0;                      // Reset quad counter
            time += GetFrameTime();         // Update timer needed by `DrawTextWave3D()`

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                BeginMode3D(camera);{
                    DrawCube(cubePosition, cubeSize, dark);
                    DrawCubeWires(cubePosition, 2.1f, 2.1f, 2.1f, light);

                    DrawGrid(10, 2.0f);

                    // Use a shader to handle the depth buffer issue with transparent textures
                    // NOTE: more info at https://bedroomcoders.co.uk/raylib-billboards-advanced-use/
                    BeginShaderMode(alphaDiscard);

                        // Draw the 3D text above the red cube
                        rlPushMatrix();
                            rlRotatef(90.0f, 1.0f, 0.0f, 0.0f);
                            rlRotatef(90.0f, 0.0f, 0.0f, -1.0f);

                            for (int i = 0; i < layers; ++i)
                            {
                                Color clr = light;
                                if (multicolor) clr = multi[i];
                                DrawTextWave3D(font, text, (Vector3)new(-tbox.X/2.0f,layerDistance*i, -4.5f), fontSize, fontSpacing, lineSpacing, true, &wcfg, time, clr);
                            }

                            // Draw the text boundry if set
                            if (SHOW_TEXT_BOUNDRY) DrawCubeWires(new( 0.0f, 0.0f, -4.5f + tbox.Z/2 ), tbox, dark);
                        rlPopMatrix();

                        // Don't draw the letter boundries for the 3D text below
                        bool slb = SHOW_LETTER_BOUNDRY;
                        SHOW_LETTER_BOUNDRY = false;

                        // Draw 3D options (use default font)
                        rlPushMatrix();
                            rlRotatef(180.0f, 0.0f, 1.0f, 0.0f);
                            char *opt = (char *)TextFormat("< SIZE: %2 == 0.1f >", fontSize);
                            quads += TextLength(opt);
                            Vector3 m = MeasureText3D(GetFontDefault(), opt, 8.0f, 1.0f, 0.0f);
                            Vector3 pos = new( -m.X/2.0f, 0.01f, 2.0f);
                            DrawText3D(GetFontDefault(), opt, pos, 8.0f, 1.0f, 0.0f, false, Blue);
                            pos.Z += 0.5f + m.Z;

                            opt = (char *)TextFormat("< SPACING: %2 == 0.1f >", fontSpacing);
                            quads += TextLength(opt);
                            m = MeasureText3D(GetFontDefault(), opt, 8.0f, 1.0f, 0.0f);
                            pos.X = -m.X/2.0f;
                            DrawText3D(GetFontDefault(), opt, pos, 8.0f, 1.0f, 0.0f, false, Blue);
                            pos.Z += 0.5f + m.Z;

                            opt = (char *)TextFormat("< LINE: %2 == 0.1f >", lineSpacing);
                            quads += TextLength(opt);
                            m = MeasureText3D(GetFontDefault(), opt, 8.0f, 1.0f, 0.0f);
                            pos.X = -m.X/2.0f;
                            DrawText3D(GetFontDefault(), opt, pos, 8.0f, 1.0f, 0.0f, false, Blue);
                            pos.Z += 1.0f + m.Z;

                            opt = (char *)TextFormat("< LBOX: %3s >", slb? "ON" : "OFF");
                            quads += TextLength(opt);
                            m = MeasureText3D(GetFontDefault(), opt, 8.0f, 1.0f, 0.0f);
                            pos.X = -m.X/2.0f;
                            DrawText3D(GetFontDefault(), opt, pos, 8.0f, 1.0f, 0.0f, false, Red);
                            pos.Z += 0.5f + m.Z;

                            opt = (char *)TextFormat("< TBOX: %3s >", SHOW_TEXT_BOUNDRY? "ON" : "OFF");
                            quads += TextLength(opt);
                            m = MeasureText3D(GetFontDefault(), opt, 8.0f, 1.0f, 0.0f);
                            pos.X = -m.X/2.0f;
                            DrawText3D(GetFontDefault(), opt, pos, 8.0f, 1.0f, 0.0f, false, Red);
                            pos.Z += 0.5f + m.Z;

                            opt = (char *)TextFormat("< LAYER DISTANCE: %.3f >", layerDistance);
                            quads += TextLength(opt);
                            m = MeasureText3D(GetFontDefault(), opt, 8.0f, 1.0f, 0.0f);
                            pos.X = -m.X/2.0f;
                            DrawText3D(GetFontDefault(), opt, pos, 8.0f, 1.0f, 0.0f, false, DarkPurple);
                        rlPopMatrix();

                        // Draw 3D info text (use default font)
                        opt = "All the text displayed here is in 3D";
                        quads += 36;
                        m = MeasureText3D(GetFontDefault(), opt, 10.0f, 0.5f, 0.0f);
                        pos = new(-m.X/2.0f, 0.01f, 2.0f);
                        DrawText3D(GetFontDefault(), opt, pos, 10.0f, 0.5f, 0.0f, false, DarkBlue);
                        pos.Z += 1.5f + m.Z;

                        opt = "press [Left]/[Right] to change the font size";
                        quads += 44;
                        m = MeasureText3D(GetFontDefault(), opt, 6.0f, 0.5f, 0.0f);
                        pos.X = -m.X/2.0f;
                        DrawText3D(GetFontDefault(), opt, pos, 6.0f, 0.5f, 0.0f, false, DarkBlue);
                        pos.Z += 0.5f + m.Z;

                        opt = "press [Up]/[Down] to change the font spacing";
                        quads += 44;
                        m = MeasureText3D(GetFontDefault(), opt, 6.0f, 0.5f, 0.0f);
                        pos.X = -m.X/2.0f;
                        DrawText3D(GetFontDefault(), opt, pos, 6.0f, 0.5f, 0.0f, false, DarkBlue);
                        pos.Z += 0.5f + m.Z;

                        opt = "press [PgUp]/[PgDown] to change the line spacing";
                        quads += 48;
                        m = MeasureText3D(GetFontDefault(), opt, 6.0f, 0.5f, 0.0f);
                        pos.X = -m.X/2.0f;
                        DrawText3D(GetFontDefault(), opt, pos, 6.0f, 0.5f, 0.0f, false, DarkBlue);
                        pos.Z += 0.5f + m.Z;

                        opt = "press [F1] to toggle the letter boundry";
                        quads += 39;
                        m = MeasureText3D(GetFontDefault(), opt, 6.0f, 0.5f, 0.0f);
                        pos.X = -m.X/2.0f;
                        DrawText3D(GetFontDefault(), opt, pos, 6.0f, 0.5f, 0.0f, false, DarkBlue);
                        pos.Z += 0.5f + m.Z;

                        opt = "press [F2] to toggle the text boundry";
                        quads += 37;
                        m = MeasureText3D(GetFontDefault(), opt, 6.0f, 0.5f, 0.0f);
                        pos.X = -m.X/2.0f;
                        DrawText3D(GetFontDefault(), opt, pos, 6.0f, 0.5f, 0.0f, false, DarkBlue);

                        SHOW_LETTER_BOUNDRY = slb;
                    EndShaderMode();

                }EndMode3D();

                // Draw 2D info text & stats
                DrawText("Drag & drop a font file to change the font!\nType something, see what happens!\n\n"
                "Press [F3] to toggle the camera", 10, 35, 10, Black);

                quads += TextLength(text)*2*layers;
                char *tmp = (char *)TextFormat("%2 == 0i layer(s) | %s camera | %4i quads (%4i verts)", layers, spin? "ORBITAL" : "FREE", quads, quads*4);
                int width = MeasureText(tmp, 10);
                DrawText(tmp, screenWidth - 20 - width, 10, 10, DarkGreen);

                tmp = "[Home]/[End] to add/remove 3D text layers";
                width = MeasureText(tmp, 10);
                DrawText(tmp, screenWidth - 20 - width, 25, 10, DarkGray);

                tmp = "[Insert]/[Delete] to increase/decrease distance between layers";
                width = MeasureText(tmp, 10);
                DrawText(tmp, screenWidth - 20 - width, 40, 10, DarkGray);

                tmp = "click the [CUBE] for a random color";
                width = MeasureText(tmp, 10);
                DrawText(tmp, screenWidth - 20 - width, 55, 10, DarkGray);

                tmp = "[Tab] to toggle multicolor mode";
                width = MeasureText(tmp, 10);
                DrawText(tmp, screenWidth - 20 - width, 70, 10, DarkGray);

                DrawFPS(10, 10);

            }EndDrawing();
        }

        // De-Initialization
        UnloadFont(font);
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }

    // Module Functions Definitions
    // Draw codepoint at specified position in 3D space
    static static void DrawTextCodepoint3D(Font font, int codepoint, Vector3 position, float fontSize, bool backface, Color tint)
    {
        // Character index position in sprite font
        // NOTE: In case a codepoint is not available in the font, index returned points to '?'
        int index = GetGlyphIndex(font, codepoint);
        float scale = fontSize/(float)font.baseSize;

        // Character destination rectangle on screen
        // NOTE: We consider charsPadding on drawing
        position.X += (float)(font.glyphs[index].offsetX - font.glyphPadding)/(float)font.baseSize*scale;
        position.Z += (float)(font.glyphs[index].offsetY - font.glyphPadding)/(float)font.baseSize*scale;

        // Character source rectangle from font texture atlas
        // NOTE: We consider chars padding when drawing, it could be required for outline/glow shader effects
        RectangleF srcRec = { font.recs[index].X - (float)font.glyphPadding, font.recs[index].Y - (float)font.glyphPadding,
                             font.recs[index].Width + 2.0f*font.glyphPadding, font.recs[index].Height + 2.0f*font.glyphPadding };

        float width = (float)(font.recs[index].Width + 2.0f*font.glyphPadding)/(float)font.baseSize*scale;
        float height = (float)(font.recs[index].Height + 2.0f*font.glyphPadding)/(float)font.baseSize*scale;

        if (font.texture.id > 0)
        {
            const float x = 0.0f;
            const float y = 0.0f;
            const float z = 0.0f;

            // normalized texture coordinates of the glyph inside the font texture (0.0f . 1.0f)
            const float tx = srcRec.X/font.texture.Width;
            const float ty = srcRec.Y/font.texture.Height;
            const float tw = (srcRec.X+srcRec.Width)/font.texture.Width;
            const float th = (srcRec.Y+srcRec.Height)/font.texture.Height;

            if (SHOW_LETTER_BOUNDRY) DrawCubeWires(new( position.X + width/2, position.Y, position.Z + height/2), new( width, LETTER_BOUNDRY_SIZE, height ), LETTER_BOUNDRY_COLOR);

            rlCheckRenderBatchLimit(4 + 4*backface);
            rlSetTexture(font.texture.id);

            rlPushMatrix();
                rlTranslatef(position.X, position.Y, position.Z);

                rlBegin(RL_QUADS);
                    rlColor4ub(tint.r, tint.g, tint.b, tint.a);

                    // Front Face
                    rlNormal3f(0.0f, 1.0f, 0.0f);                                   // Normal Pointing Up
                    rlTexCoord2f(tx, ty); rlVertex3f(x,         y, z);              // Top Left Of The Texture and Quad
                    rlTexCoord2f(tx, th); rlVertex3f(x,         y, z + height);     // Bottom Left Of The Texture and Quad
                    rlTexCoord2f(tw, th); rlVertex3f(x + width, y, z + height);     // Bottom Right Of The Texture and Quad
                    rlTexCoord2f(tw, ty); rlVertex3f(x + width, y, z);              // Top Right Of The Texture and Quad

                    if (backface)
                    {
                        // Back Face
                        rlNormal3f(0.0f, -1.0f, 0.0f);                              // Normal Pointing Down
                        rlTexCoord2f(tx, ty); rlVertex3f(x,         y, z);          // Top Right Of The Texture and Quad
                        rlTexCoord2f(tw, ty); rlVertex3f(x + width, y, z);          // Top Left Of The Texture and Quad
                        rlTexCoord2f(tw, th); rlVertex3f(x + width, y, z + height); // Bottom Left Of The Texture and Quad
                        rlTexCoord2f(tx, th); rlVertex3f(x,         y, z + height); // Bottom Right Of The Texture and Quad
                    }
                rlEnd();
            rlPopMatrix();

            rlSetTexture(0);
        }
    }

    // Draw a 2D text in 3D space
    static static void DrawText3D(Font font, string text, Vector3 position, float fontSize, float fontSpacing, float lineSpacing, bool backface, Color tint)
    {
        int length = TextLength(text);          // Total length in bytes of the text, scanned by codepoints in loop

        float textOffsetY = 0.0f;               // Offset between lines (on line break '\n')
        float textOffsetX = 0.0f;               // Offset X to next character to draw

        float scale = fontSize/(float)font.baseSize;

        for (int i = 0; i < length;)
        {
            // Get next codepoint from byte string and glyph index in font
            int codepointByteCount = 0;
            int codepoint = GetCodepoint(&text[i], &codepointByteCount);
            int index = GetGlyphIndex(font, codepoint);

            // NOTE: Normally we exit the decoding sequence as soon as a bad byte is found (and return 0x3f)
            // but we need to draw all of the bad bytes using the '?' symbol moving one byte
            if (codepoint == 0x3f) codepointByteCount = 1;

            if (codepoint == '\n')
            {
                // NOTE: Fixed line spacing of 1.5 line-height
                // TODO: Support custom line spacing defined by user
                textOffsetY += scale + lineSpacing/(float)font.baseSize*scale;
                textOffsetX = 0.0f;
            }
            else
            {
                if ((codepoint != ' ') && (codepoint != '\t'))
                {
                    DrawTextCodepoint3D(font, codepoint, new( position.X + textOffsetX, position.Y, position.Z + textOffsetY ), fontSize, backface, tint);
                }

                if (font.glyphs[index].advanceX == 0) textOffsetX += (float)(font.recs[index].Width + fontSpacing)/(float)font.baseSize*scale;
                else textOffsetX += (float)(font.glyphs[index].advanceX + fontSpacing)/(float)font.baseSize*scale;
            }

            i += codepointByteCount;   // Move text bytes counter to next codepoint
        }
    }

    // Measure a text in 3D. For some reason `MeasureText()` just doesn't seem to work so i had to use this instead.
    static Vector3 MeasureText3D(Font font, const char* text, float fontSize, float fontSpacing, float lineSpacing)
    {
        int len = TextLength(text);
        int tempLen = 0;                // Used to count longer text line num chars
        int lenCounter = 0;

        float tempTextWidth = 0.0f;     // Used to count longer text line width

        float scale = fontSize/(float)font.baseSize;
        float textHeight = scale;
        float textWidth = 0.0f;

        int letter = 0;                 // Current character
        int index = 0;                  // Index position in sprite font

        for (int i = 0; i < len; i++)
        {
            lenCounter++;

            int next = 0;
            letter = GetCodepoint(&text[i], &next);
            index = GetGlyphIndex(font, letter);

            // NOTE: normally we exit the decoding sequence as soon as a bad byte is found (and return 0x3f)
            // but we need to draw all of the bad bytes using the '?' symbol so to not skip any we set next = 1
            if (letter == 0x3f) next = 1;
            i += next - 1;

            if (letter != '\n')
            {
                if (font.glyphs[index].advanceX != 0) textWidth += (font.glyphs[index].advanceX+fontSpacing)/(float)font.baseSize*scale;
                else textWidth += (font.recs[index].Width + font.glyphs[index].offsetX)/(float)font.baseSize*scale;
            }
            else
            {
                if (tempTextWidth < textWidth) tempTextWidth = textWidth;
                lenCounter = 0;
                textWidth = 0.0f;
                textHeight += scale + lineSpacing/(float)font.baseSize*scale;
            }

            if (tempLen < lenCounter) tempLen = lenCounter;
        }

        if (tempTextWidth < textWidth) tempTextWidth = textWidth;

        Vector3 vec = new();
        vec.X = tempTextWidth + (float)((tempLen - 1)*fontSpacing/(float)font.baseSize*scale); // Adds chars spacing to measure
        vec.Y = 0.25f;
        vec.Z = textHeight;

        return vec;
    }

    // Draw a 2D text in 3D space and wave the parts that start with `~~` and end with `~~`.
    // This is a modified version of the original code by @Nighten found here https://github.com/NightenDushi/Raylib_DrawTextStyle
    static static void DrawTextWave3D(Font font, string text, Vector3 position, float fontSize, float fontSpacing, float lineSpacing, bool backface, WaveTextConfig* config, float time, Color tint)
    {
        int length = TextLength(text);          // Total length in bytes of the text, scanned by codepoints in loop

        float textOffsetY = 0.0f;               // Offset between lines (on line break '\n')
        float textOffsetX = 0.0f;               // Offset X to next character to draw

        float scale = fontSize/(float)font.baseSize;

        bool wave = false;

        for (int i = 0, k = 0; i < length; ++k)
        {
            // Get next codepoint from byte string and glyph index in font
            int codepointByteCount = 0;
            int codepoint = GetCodepoint(&text[i], &codepointByteCount);
            int index = GetGlyphIndex(font, codepoint);

            // NOTE: Normally we exit the decoding sequence as soon as a bad byte is found (and return 0x3f)
            // but we need to draw all of the bad bytes using the '?' symbol moving one byte
            if (codepoint == 0x3f) codepointByteCount = 1;

            if (codepoint == '\n')
            {
                // NOTE: Fixed line spacing of 1.5 line-height
                // TODO: Support custom line spacing defined by user
                textOffsetY += scale + lineSpacing/(float)font.baseSize*scale;
                textOffsetX = 0.0f;
                k = 0;
            }
            else if (codepoint == '~')
            {
                if (GetCodepoint(&text[i+1], &codepointByteCount) == '~')
                {
                    codepointByteCount += 1;
                    wave = !wave;
                }
            }
            else
            {
                if ((codepoint != ' ') && (codepoint != '\t'))
                {
                    Vector3 pos = position;
                    if (wave) // Apply the wave effect
                    {
                        pos.X += MathF.Sin(time*config.waveSpeed.X-k*config.waveOffset.X)*config.waveRange.X;
                        pos.Y += MathF.Sin(time*config.waveSpeed.Y-k*config.waveOffset.Y)*config.waveRange.Y;
                        pos.Z += MathF.Sin(time*config.waveSpeed.Z-k*config.waveOffset.Z)*config.waveRange.Z;
                    }

                    DrawTextCodepoint3D(font, codepoint, new( pos.X + textOffsetX, pos.Y, pos.Z + textOffsetY ), fontSize, backface, tint);
                }

                if (font.glyphs[index].advanceX == 0) textOffsetX += (float)(font.recs[index].Width + fontSpacing)/(float)font.baseSize*scale;
                else textOffsetX += (float)(font.glyphs[index].advanceX + fontSpacing)/(float)font.baseSize*scale;
            }

            i += codepointByteCount;   // Move text bytes counter to next codepoint
        }
    }

    // Measure a text in 3D ignoring the `~~` chars.
    static Vector3 MeasureTextWave3D(Font font, const char* text, float fontSize, float fontSpacing, float lineSpacing)
    {
        int len = TextLength(text);
        int tempLen = 0;                // Used to count longer text line num chars
        int lenCounter = 0;

        float tempTextWidth = 0.0f;     // Used to count longer text line width

        float scale = fontSize/(float)font.baseSize;
        float textHeight = scale;
        float textWidth = 0.0f;

        int letter = 0;                 // Current character
        int index = 0;                  // Index position in sprite font

        for (int i = 0; i < len; i++)
        {
            lenCounter++;

            int next = 0;
            letter = GetCodepoint(&text[i], &next);
            index = GetGlyphIndex(font, letter);

            // NOTE: normally we exit the decoding sequence as soon as a bad byte is found (and return 0x3f)
            // but we need to draw all of the bad bytes using the '?' symbol so to not skip any we set next = 1
            if (letter == 0x3f) next = 1;
            i += next - 1;

            if (letter != '\n')
            {
                if (letter == '~' && GetCodepoint(&text[i+1], &next) == '~')
                {
                    i++;
                }
                else
                {
                    if (font.glyphs[index].advanceX != 0) textWidth += (font.glyphs[index].advanceX+fontSpacing)/(float)font.baseSize*scale;
                    else textWidth += (font.recs[index].Width + font.glyphs[index].offsetX)/(float)font.baseSize*scale;
                }
            }
            else
            {
                if (tempTextWidth < textWidth) tempTextWidth = textWidth;
                lenCounter = 0;
                textWidth = 0.0f;
                textHeight += scale + lineSpacing/(float)font.baseSize*scale;
            }

            if (tempLen < lenCounter) tempLen = lenCounter;
        }

        if (tempTextWidth < textWidth) tempTextWidth = textWidth;

        Vector3 vec = new();
        vec.X = tempTextWidth + (float)((tempLen - 1)*fontSpacing/(float)font.baseSize*scale); // Adds chars spacing to measure
        vec.Y = 0.25f;
        vec.Z = textHeight;

        return vec;
    }

    // Generates a nice color with a random hue
    static Color GenerateRandomColor(float s, float v)
    {
        const float Phi = 0.618033988749895f; // Golden ratio conjugate
        float h = (float)GetRandomValue(0, 360);
        h = fmodf((h + h*Phi), 360.0f);
        return ColorFromHS(h, s, v);
    }
}
