using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nightfall.Dynamic;

namespace NightFall.Dynamic.Projectiles;

public class FireCircleProjectile : IProjectile
{
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }

    public float Speed => Velocity.Length();

    public float Rotation { get; set; }
    public float Scale { get; set; }

    public int LifeSpan { get; set; }

    public FireCircleProjectile(Texture2D texture, Vector2 position, Vector2 velocity, float rotation, float scale, int lifeSpan)
    {
        Texture = texture;
        Position = position;
        Velocity = velocity;
        Rotation = rotation;
        Scale = scale;
        LifeSpan = lifeSpan;
    }

    public void Update(GameTime gameTime)
    {

    }

    public void Dispose()
    {
        Texture?.Dispose();
    }

    ~FireCircleProjectile()
    {
        Dispose();
    }
}
