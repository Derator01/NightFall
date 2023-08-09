using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Nightfall.Dynamic;

public interface IDynamicTexture : IDisposable
{
    public Texture2D Texture { get; set; }

    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public float Speed { get; }

    public float Rotation { get; set; }

    public float Scale { get; set; }
}
