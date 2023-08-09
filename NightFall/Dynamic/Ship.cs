using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NightFall.Resources;
using System;
using System.Collections.Generic;

namespace Nightfall.Dynamic;

public class Ship : IDynamicTexture
{
    public Texture2D Texture { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Vector2 FingerStartPosition { get; internal set; }
    public Vector2 Position { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Vector2 Velocity { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public float Speed => throw new System.NotImplementedException();

    public float Rotation { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float Scale { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public int HP { get; set; }

    internal bool _isMoving;

    public HashSet<ISpell> Spells { get; } = new HashSet<ISpell>(new SpellEqualityComparer());

    public Ship(Texture2D texture, Vector2 position, int hp, float rotation = 0, float scale = 1)
    {
        Texture = texture;
        Position = position;
        HP = hp;
        Rotation = rotation;
        Scale = scale;
    }

    public void Update(GameTime gameTime)
    {
        // TODO
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    ~Ship()
    {
        Dispose();
    }
}
