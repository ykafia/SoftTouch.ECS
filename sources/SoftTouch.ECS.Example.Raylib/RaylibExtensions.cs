using System.Numerics;
using Raylib_cs;

namespace SoftTouch.ECS.Example.RaylibUtilities;


// Light data
public struct Light
{
    public int Type { get; set; }
    public bool Enabled { get; set; }
    public Vector3 Position { get; set; }
    public Vector3 Target { get; set; }
    public Color Color { get; set; }
    public float Attenuation { get; set; }

    // Shader locations
    public int EnabledLoc { get; set; }
    public int TypeLoc { get; set; }
    public int PositionLoc { get; set; }
    public int TargetLoc { get; set; }
    public int ColorLoc { get; set; }
    public int AttenuationLoc { get; set; }
};

// Light type
public enum LightType : int
{
    LIGHT_DIRECTIONAL = 0,
    LIGHT_POINT
}

public static class Util
{
    public static int LightsCount = 0;
    static int MaxLights = 1000;
    public static Light CreateLight(LightType type, Vector3 position, Vector3 target, Color color, Shader shader)
    {
        Light light = new();

        if (LightsCount < MaxLights)
        {
            light = new()
            {
                Enabled = true,
                Type = (int)type,
                Position = position,
                Target = target,
                Color = color,
                // NOTE: Lighting shader naming must be the provided ones
                EnabledLoc = Raylib.GetShaderLocation(shader, $"lights[{LightsCount}].enabled"),
                TypeLoc = Raylib.GetShaderLocation(shader, $"lights[{LightsCount}].type"),
                PositionLoc = Raylib.GetShaderLocation(shader, $"lights[{LightsCount}].position"),
                TargetLoc = Raylib.GetShaderLocation(shader, $"lights[{LightsCount}].target"),
                ColorLoc = Raylib.GetShaderLocation(shader, $"lights[{LightsCount}].color")
            };
            UpdateLightValues(shader, light);

            LightsCount++;
        }

        return light;
    }

    // Send light properties to shader
    // NOTE: Light shader locations should be available 
    public static void UpdateLightValues(Shader shader, Light light)
    {
        // Send to shader light enabled state and type
        Raylib.SetShaderValue(shader, light.EnabledLoc, light.Enabled, ShaderUniformDataType.SHADER_UNIFORM_INT);
        Raylib.SetShaderValue(shader, light.TypeLoc, light.Type, ShaderUniformDataType.SHADER_UNIFORM_INT);

        // Send to shader light position values
        float[] position = [light.Position.X, light.Position.Y, light.Position.Z];
        Raylib.SetShaderValue(shader, light.PositionLoc, position, ShaderUniformDataType.SHADER_UNIFORM_VEC3);

        // Send to shader light target position values
        float[] target= [light.Target.X, light.Target.Y, light.Target.Z];
        Raylib.SetShaderValue(shader, light.TargetLoc, target, ShaderUniformDataType.SHADER_UNIFORM_VEC3);

        // Send to shader light color values
        float[] color = [ (float)light.Color.R/255, light.Color.G/(float)255,
                       (float)light.Color.B/255, light.Color.A/(float)255 ];
        Raylib.SetShaderValue(shader, light.ColorLoc, color, ShaderUniformDataType.SHADER_UNIFORM_VEC4);
    }
}