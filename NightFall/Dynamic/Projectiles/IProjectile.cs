using Microsoft.Xna.Framework;

namespace Nightfall.Dynamic;

public interface IProjectile : IDynamicTexture
{
    int LifeSpan { get; }

    public void Update(GameTime gameTime);
}