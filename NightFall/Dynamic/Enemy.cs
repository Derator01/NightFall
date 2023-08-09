using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Nightfall.Dynamic;

public class Enemy : IDynamicTexture, IDisposable
{
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }

    public float Speed => Velocity.Length();

    public float Rotation { get; set; }
    public float Scale { get; set; }

    public int HP { get; set; }

    public Enemy(Texture2D texture, Vector2 position, int hP, float scale = 1, float rotation = 0)
    {
        Texture = texture;
        Position = position;
        Rotation = rotation;
        Scale = scale;
        HP = hP;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    ~Enemy()
    {
        Dispose();
    }
}
