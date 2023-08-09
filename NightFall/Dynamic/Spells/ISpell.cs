using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Nightfall.Dynamic;

public interface ISpell
{
    public IProjectile ExampleProjectile { get; }

    public int CastDelay { get; set; }

    public List<IProjectile> Projectiles { get; }

    public void Update(GameTime gameTime);
}