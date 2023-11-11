using System.Drawing;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public class RlLights
{
    public const int MAXLIGHTS = 4; // Max dynamic lights supported by shader
    private static int lightsCount; // Current amount of created lights

    // Create a light and get shader locations
    public static Light CreateLight(LightType type, Vector3 position, Vector3 target, Color color, Shader shader)
    {
        Light light = new();

        if (lightsCount < MAXLIGHTS)
        {
            light.enabled = true;
            light.type = type;
            light.position = position;
            light.target = target;
            light.color = color;

            // NOTE: Lighting shader naming must be the provided ones
            light.enabledLoc = GetShaderLocation(shader, $"lights[{lightsCount}].enabled");
            light.typeLoc = GetShaderLocation(shader, $"lights[{lightsCount}].type");
            light.positionLoc = GetShaderLocation(shader, $"lights[{lightsCount}].position");
            light.targetLoc = GetShaderLocation(shader, $"lights[{lightsCount}].target");
            light.colorLoc = GetShaderLocation(shader, $"lights[{lightsCount}].color");

            UpdateLightValues(shader, light);

            lightsCount++;
        }

        return light;
    }

    // Send light properties to shader
    // NOTE: Light shader locations should be available
    public static void UpdateLightValues(Shader shader, Light light)
    {
        // Send to shader light enabled state and type
        SetShaderValue(shader, light.enabledLoc, ref light.enabled, ShaderUniformDataType.ShaderUniformInt);
        SetShaderValue(shader, light.typeLoc, ref light.type, ShaderUniformDataType.ShaderUniformInt);

        // Send to shader light position values
        SetShaderValue(shader, light.positionLoc, ref light.position, ShaderUniformDataType.ShaderUniformVec3);

        // Send to shader light target position values
        Vector3 target = new(light.target.X, light.target.Y, light.target.Z);
        SetShaderValue(shader, light.targetLoc, ref target, ShaderUniformDataType.ShaderUniformVec3);

        // Send to shader light color values
        Vector4 color = new(light.color.R / 255f, light.color.G / 255f, light.color.B / 255f, light.color.A / 255f);
        SetShaderValue(shader, light.colorLoc, ref color, ShaderUniformDataType.ShaderUniformVec4);
    }

}

public struct Light
{
    public LightType type;
    public bool enabled;
    public Vector3 position;
    public Vector3 target;
    public Color color;
    public float attenuation;

    // Shader locations
    public int enabledLoc;
    public int typeLoc;
    public int positionLoc;
    public int targetLoc;
    public int colorLoc;
    public int attenuationLoc;
}

// Light type
public enum LightType
{
    LightDirectional = 0,
    LightPoint
};
