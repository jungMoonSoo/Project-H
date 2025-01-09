using System.Collections.Generic;

public class NearFirstTarget : IUnidadTargeting
{
    public bool TryGetTargets(out Unidad[] targets, UnitType type, EllipseCollider collider, int count)
    {
        List<Unidad> result = new(UnidadManager.Instance.GetUnidads(type, TargetType.They));

        result.Sort((x, y) => collider.OnEllipseDepth(x.unitCollider) > collider.OnEllipseDepth(y.unitCollider) ? 1 : -1);

        targets = null;

        if (result.Count != 0)
        {
            targets = result.GetRange(0, count).ToArray();

            return true;
        }

        return false;
    }

    public void OnEvent(Unidad unidad, Unidad target)
    {
        CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(unidad.NowAttackStatus, target.NowDefenceStatus, 100, 0, ElementType.None);
        target.OnDamage((int)callback.value, callback.type);

        unidad.IncreaseMp(callback.type == DamageType.Miss ? 0.5f : 1);
    }
}