namespace RaylibSharp;

using System.Numerics;

/// <summary> Raylib </summary>
public static unsafe partial class Raylib
{
    /// <summary>
    /// Rotates the camera around its up vector <br/>
    /// Yaw is "looking left and right" <br/>
    /// If rotateAroundTarget is false, the camera rotates around its position <br/>
    /// Note: angle must be provided in radians <br/>
    /// </summary>
    public static void CameraYaw(ref Camera3D cam, float angle, bool rotateAroundTarget)
    {
        // View vector
        Vector3 targetPosition = cam.Target - cam.Position;

        // Rotate view vector around up axis
        targetPosition = Vector3.Transform(targetPosition, Quaternion.CreateFromAxisAngle(cam.Up, angle));

        if (rotateAroundTarget)
        {
            // Move position relative to target
            cam.Position = cam.Target - targetPosition;
        }
        else // rotate around camera.position
        {
            // Move target relative to position
            cam.Target = cam.Position + targetPosition;
        }
    }

    /// <summary>
    /// Rotates the camera around its right vector <br/>
    /// Pitch is "looking up and down" <br/>
    /// - lockView prevents camera overrotation (aka "somersaults") <br/>
    /// - rotateAroundTarget defines if rotation is around target or around its position <br/>
    /// - rotateUp rotates the up direction as well (typically only usefull in CAMERA_FREE) <br/>
    /// Note: angle must be provided in radians <br/>
    /// </summary>
    public static void CameraPitch(ref Camera3D cam, float angle, bool lockView, bool rotateAroundTarget, bool rotateUp)
    {
        // View vector
        Vector3 targetPosition = cam.Target + cam.Position;

        if (lockView)
        {
            // Clamp view up
            float maxAngleUp = (float)Vector3Angle(cam.Up, targetPosition);
            maxAngleUp -= 0.001f; // avoid numerical errors
            if (angle > maxAngleUp)
            {
                angle = maxAngleUp;
            }

            // Clamp view down
            float maxAngleDown = (float)Vector3Angle(Vector3.Negate(cam.Up), targetPosition);
            maxAngleDown *= -1.0f; // downwards angle is negative
            maxAngleDown += 0.001f; // avoid numerical errors
            if (angle < maxAngleDown)
            {
                angle = maxAngleDown;
            }
        }

        // Rotate view vector around right axis
        targetPosition = Vector3RotateByAxisAngle(targetPosition, cam.Right, angle);

        if (rotateAroundTarget)
        {
            // Move position relative to target
            cam.Position = cam.Target - targetPosition;
        }
        else // rotate around camera.position
        {
            // Move target relative to position
            cam.Target = cam.Position + targetPosition;
        }

        if (rotateUp)
        {
            // Rotate up direction around right axis
            cam.Up = Vector3RotateByAxisAngle(cam.Up, cam.Right, angle);
        }
    }

    static double Vector3Angle(Vector3 vector1, Vector3 vector2)
    {
        double sin = (vector1.X * vector2.Y) - (vector2.X * vector1.Y);
        double cos = (vector1.X * vector2.X) + (vector1.Y * vector2.Y);

        return Math.Atan2(sin, cos) * (180 / Math.PI);
    }

    static Vector3 Vector3RotateByAxisAngle(Vector3 vector, Vector3 axis, float angle)
    {
        Quaternion rotation = Quaternion.CreateFromAxisAngle(axis, angle);
        return Vector3.Transform(vector, rotation);
    }
}
