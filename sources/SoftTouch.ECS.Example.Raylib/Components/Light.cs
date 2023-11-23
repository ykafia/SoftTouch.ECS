using System.Numerics;
using Raylib_cs;

namespace SoftTouch.ECS.Example.Rlib;


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
