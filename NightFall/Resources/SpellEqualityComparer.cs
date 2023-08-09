using Nightfall.Dynamic;
using System.Collections.Generic;

namespace NightFall.Resources;

public class SpellEqualityComparer : IEqualityComparer<ISpell>
{
    public bool Equals(ISpell x, ISpell y)
    {
        if (x == null || y == null)
            return false;

        return x.GetType() == y.GetType();
    }

    public int GetHashCode(ISpell obj)
    {
        return obj.GetType().GetHashCode();
    }
}
