using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Nightfall.Dynamic;

public class Asteroid : IDynamicTexture
{
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }

    public float Speed => Velocity.Length();

    public float Scale { get; set; }
    public float Rotation { get; set; }

    public Asteroid(Texture2D texture, Vector2 position, float scale = 1, float rotation = 0)
    {
        Texture = texture;
        Position = position;
        Velocity = Vector2.Zero;
        Scale = scale;
        Rotation = rotation;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    ~Asteroid()
    {
        Dispose();
    }
}
