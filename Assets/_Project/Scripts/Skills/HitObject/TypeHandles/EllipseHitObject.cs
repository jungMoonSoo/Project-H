using System.Collections.Generic;
using UnityEngine;

public class EllipseHitObject : HitObjectBase
{
    [SerializeField] private EllipseCollider coll;

    private readonly List<Unidad> targets = new();

    public override Unidad[] Targets
    {
        get
        {
            targets.Clear();

            if (TargetType == TargetType.Me)
            {
                targets.Add(Caster);

                return targets.ToArray();
            }

            foreach (Unidad unidad in UnidadManager.Instance.GetUnidads(Caster.Owner, TargetType))
            {
                if (coll.OnEnter(unidad.unitCollider)) targets.Add(unidad);
            }

            return TargetingFilter.GetFilteredTargets(targets, TargetPos);
        }
    }

    public override Vector2 GetAreaSize() => coll.size;
}
