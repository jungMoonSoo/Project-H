using System.Collections.Generic;
using UnityEngine;

public class ArcHitObject : HitObjectBase
{
    [SerializeField] private ArcCollider coll;

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

            coll.Direction = TargetPos - CreatePos;

            foreach (Unidad unidad in UnidadManager.Instance.GetUnidads(Caster.Owner, TargetType))
            {
                if (coll.OnEnter(unidad.unitCollider)) targets.Add(unidad);
            }

            return TargetingFilter.GetFilteredTargets(targets, TargetPos);
        }
    }

    public override Vector2 GetAreaSize() => coll.Radius * 2;
}
