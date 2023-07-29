clang ..\raylib\parser\raylib_parser.c -o ..\raylib\parser\raylib_parser.exe

..\raylib\parser\raylib_parser.exe -i ../raylib/src/raylib.h -o RaylibSharpGenerator/api/raylib.json -f JSON -d RLAPI
..\raylib\parser\raylib_parser.exe -i ../raylib/src/rlgl.h -o RaylibSharpGenerator/api/rlgl.json -f JSON -d RLAPI  -t "RLGL IMPLEMENTATION"
..\raylib\parser\raylib_parser.exe -i ../raygui/src/raygui.h -o RaylibSharpGenerator/api/raygui.json -f JSON -d RAYGUIAPI -t "RAYGUI IMPLEMENTATION"
