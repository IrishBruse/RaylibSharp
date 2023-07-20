namespace Examples;

using System;

public static class ExamplesList
{
    public static readonly (string path, Func<int> example)[] Examples = {
        ("./resources/images/core_basic_window.png",            CoreBasicWindow.Example),
        ("./resources/images/core_input_keys.png",              CoreInputKeys.Example),
        ("./resources/images/core_input_mouse.png",             CoreInputMouse.Example),
        ("./resources/images/core_input_mouse_wheel.png",       CoreInputMouseWheel.Example),
        ("./resources/images/core_input_gamepad.png",           CoreInputGamepad.Example),
        ("./resources/images/core_input_multitouch.png",        CoreInputMultitouch.Example),
        ("./resources/images/core_input_gestures.png",          CoreInputGestures.Example),
        ("./resources/images/core_2d_camera.png",               Core2dCamera.Example),
        ("./resources/images/core_2d_camera_mouse_zoom.png",    Core2dCameraMouseZoom.Example),
        ("./resources/images/core_2d_camera_platformer.png",    Core2dCameraPlatformer.Example),
        ("./resources/images/core_3d_camera_mode.png",          Core3dCameraMode.Example),
        ("./resources/images/core_3d_camera_free.png",          Core3dCameraFree.Example),
        ("./resources/images/core_3d_camera_first_person.png",  Core3dCameraFirstPerson.Example),
        ("./resources/images/core_3d_picking.png",              Core3dPicking.Example),
        ("./resources/images/core_world_screen.png",            CoreWorldScreen.Example),
        ("./resources/images/core_custom_logging.png",          CoreCustomLogging.Example),
        ("./resources/images/core_window_flags.png",            CoreWindowFlags.Example),
        ("./resources/images/core_window_letterbox.png",        CoreWindowLetterbox.Example),
        ("./resources/images/core_window_should_close.png",     CoreWindowShouldClose.Example),
        ("./resources/images/core_drop_files.png",              CoreDropFiles.Example),
        ("./resources/images/core_random_values.png",           CoreRandomValues.Example),
        ("./resources/images/core_storage_values.png",          CoreStorageValues.Example),
        ("./resources/images/core_vr_simulator.png",            CoreVrSimulator.Example),
        ("./resources/images/core_loading_thread.png",          CoreLoadingThread.Example),
        ("./resources/images/core_scissor_test.png",            CoreScissorTest.Example),
        ("./resources/images/core_basic_screen_manager.png",    CoreBasicScreenManager.Example),
        ("./resources/images/core_custom_frame_control.png",    CoreCustomFrameControl.Example),
        ("./resources/images/core_smooth_pixelperfect.png",     CoreSmoothPixelperfect.Example),
        ("./resources/images/core_split_screen.png",            CoreSplitScreen.Example),
    };
}
