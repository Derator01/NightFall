using Microsoft.Xna.Framework;
using Nightfall.Dynamic;
using NightFall.Dynamic.Projectiles;
using System.Collections.Generic;

namespace NightFall.Dynamic.Spells;

public sealed class FireCircleSpell : ISpell
{
    public IProjectile ExampleProjectile { get; }

    public int CastDelay { get; set; }

    public List<IProjectile> Projectiles { get; }

    public FireCircleSpell(int castDelay)
    {
        CastDelay = castDelay;

        ExampleProjectile = new FireCircleProjectile();
    }

    public void Update(GameTime gameTime)
    {

    }
}
