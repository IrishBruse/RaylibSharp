namespace Examples;

using System;
using System.Drawing;

public static class ExamplesList
{
    public static readonly Example[] Examples = new Example[]
    {
        new("./resources/images/core_basic_window.png",            CoreBasicWindow.Example,            RaylibSharp.Raylib.Gray,    1),
        new("./resources/images/core_input_keys.png",              CoreInputKeys.Example,              RaylibSharp.Raylib.Gray,    1),
        new("./resources/images/core_input_mouse.png",             CoreInputMouse.Example,             RaylibSharp.Raylib.Gray,    1),
        new("./resources/images/core_input_mouse_wheel.png",       CoreInputMouseWheel.Example,        RaylibSharp.Raylib.Gray,    1),
        new("./resources/images/core_input_gamepad.png",           CoreInputGamepad.Example,           RaylibSharp.Raylib.Gray,    1),
        new("./resources/images/core_input_multitouch.png",        CoreInputMultitouch.Example,        RaylibSharp.Raylib.Gray,    1),
        new("./resources/images/core_input_gestures.png",          CoreInputGestures.Example,          RaylibSharp.Raylib.Gray,    2),
        new("./resources/images/core_2d_camera.png",               Core2dCamera.Example,               RaylibSharp.Raylib.Red,     2),
        new("./resources/images/core_2d_camera_mouse_zoom.png",    Core2dCameraMouseZoom.Example,      RaylibSharp.Raylib.Black,   2),
        new("./resources/images/core_2d_camera_platformer.png",    Core2dCameraPlatformer.Example,     RaylibSharp.Raylib.Gray,    3),
        new("./resources/images/core_3d_camera_mode.png",          Core3dCameraMode.Example,           RaylibSharp.Raylib.Gray,    1),
        new("./resources/images/core_3d_camera_free.png",          Core3dCameraFree.Example,           RaylibSharp.Raylib.Gray,    1),
        new("./resources/images/core_3d_camera_first_person.png",  Core3dCameraFirstPerson.Example,    RaylibSharp.Raylib.Gray,    2),
        new("./resources/images/core_3d_picking.png",              Core3dPicking.Example,              RaylibSharp.Raylib.Gray,    2),
        new("./resources/images/core_world_screen.png",            CoreWorldScreen.Example,            RaylibSharp.Raylib.Gray,    2),
        new("./resources/images/core_custom_logging.png",          CoreCustomLogging.Example,          RaylibSharp.Raylib.Gray,    3),
        new("./resources/images/core_window_flags.png",            CoreWindowFlags.Example,            RaylibSharp.Raylib.Gray,    3),
        new("./resources/images/core_window_letterbox.png",        CoreWindowLetterbox.Example,        RaylibSharp.Raylib.Black,   2),
        new("./resources/images/core_window_should_close.png",     CoreWindowShouldClose.Example,      RaylibSharp.Raylib.Gray,    1),
        new("./resources/images/core_drop_files.png",              CoreDropFiles.Example,              RaylibSharp.Raylib.Gray,    2),
        new("./resources/images/core_random_values.png",           CoreRandomValues.Example,           RaylibSharp.Raylib.Gray,    1),
        new("./resources/images/core_storage_values.png",          CoreStorageValues.Example,          RaylibSharp.Raylib.Gray,    2),
        new("./resources/images/core_vr_simulator.png",            CoreVrSimulator.Example,            RaylibSharp.Raylib.Gray,    3),
        new("./resources/images/core_loading_thread.png",          CoreLoadingThread.Example,          RaylibSharp.Raylib.Gray,    3),
        new("./resources/images/core_scissor_test.png",            CoreScissorTest.Example,            RaylibSharp.Raylib.Gray,    1),
        new("./resources/images/core_basic_screen_manager.png",    CoreBasicScreenManager.Example,     RaylibSharp.Raylib.Gray,    1),
        new("./resources/images/core_custom_frame_control.png",    CoreCustomFrameControl.Example,     RaylibSharp.Raylib.Gray,    4),
        new("./resources/images/core_smooth_pixelperfect.png",     CoreSmoothPixelperfect.Example,     RaylibSharp.Raylib.Gray,    3),
        new("./resources/images/core_split_screen.png",            CoreSplitScreen.Example,            RaylibSharp.Raylib.Gray,    4),

        new("./resources/images/audio_mixed_processor.png",        AudioMixedProcessor.Example,        RaylibSharp.Raylib.Yellow,  1),
        new("./resources/images/audio_module_playing.png",         AudioModulePlaying.Example,         RaylibSharp.Raylib.Yellow,  1),
        new("./resources/images/audio_music_stream.png",           AudioMusicStreams.Example,          RaylibSharp.Raylib.Yellow,  1),
        new("./resources/images/audio_raw_stream.png",             AudioRawStreams.Example,            RaylibSharp.Raylib.Yellow,  1),
        new("./resources/images/audio_sound_loading.png",          AudioSoundLoading.Example,          RaylibSharp.Raylib.Yellow,  1),
        new("./resources/images/audio_stream_effects.png",         AudioStreamEffects.Example,         RaylibSharp.Raylib.Yellow,  1),
    };
}

public record Example(string Path, Func<int> Entry, Color Color, int Stars);
