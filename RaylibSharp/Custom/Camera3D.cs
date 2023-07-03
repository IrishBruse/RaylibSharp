namespace RaylibSharp;

using System.Numerics;

public partial struct Camera3D
{
    /// <summary> Camera Forward Vector </summary>
    public readonly Vector3 Forward => Vector3.Normalize(Target - Position);

    /// <summary> Camera Right Vector </summary>
    public readonly Vector3 Right => Vector3.Cross(Forward, Up);

    /// <summary>
    /// Rotates the camera around its up vector <br/>
    /// Yaw is "looking left and right" <br/>
    /// If rotateAroundTarget is false, the camera rotates around its position <br/>
    /// Note: angle must be provided in radians <br/>
    /// </summary>
    public void Yaw(float angle, bool rotateAroundTarget)
    {
        Raylib.CameraYaw(ref this, angle, rotateAroundTarget);
    }

    /// <summary>
    /// Rotates the camera around its right vector <br/>
    /// Pitch is "looking up and down" <br/>
    /// - lockView prevents camera overrotation (aka "somersaults") <br/>
    /// - rotateAroundTarget defines if rotation is around target or around its position <br/>
    /// - rotateUp rotates the up direction as well (typically only usefull in CAMERA_FREE) <br/>
    /// Note: angle must be provided in radians <br/>
    /// </summary>
    public void Pitch(float angle, bool lockView, bool rotateAroundTarget, bool rotateUp)
    {
        Raylib.CameraPitch(ref this, angle, lockView, rotateAroundTarget, rotateUp);
    }

    /// <summary> Update camera position for selected mode </summary>
    public void Update(CameraMode cameraMode)
    {
        Raylib.UpdateCamera(ref this, cameraMode);
    }
}
