using UnityEngine;

public class SingleUniAttack : MonoBehaviour, IUnidadAttack
{
    [SerializeField] private float skillCoefficient = 100f;
    [SerializeField] private EffectManager effectManager;

    private Unidad[] targets;
    public Unidad[] Targets => targets;

    private UnidadTargetingType targetingType = UnidadTargetingType.Near;

    public void SetType(UnidadTargetingType type) => targetingType = type;

    public UnitState Check(Unidad unidad)
    {
        IUnidadTargeting targeting = TargetingTypeHub.GetTargetingSystem(targetingType);

        if (targeting.TryGetTargets(out targets, unidad.Owner, unidad.attackCollider, 1))
        {
            Unidad target = targets[0];

            Flip(unidad.view.transform, target.transform.position.x - unidad.transform.position.x > 0);

            if (unidad.attackCollider.OnEllipseEnter(target.unitCollider)) return UnitState.Attack;
            else return UnitState.Move;
        }

        return UnitState.Idle;
    }

    public void Attack(Unidad unidad)
    {
        Unidad target = targets[0];

        CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(unidad.NowAttackStatus, target.NowDefenceStatus, skillCoefficient, 0, ElementType.None);

        target.OnDamage((int)callback.value, callback.type, effectManager?.GetEffect(target.transform));

        unidad.IncreaseMp(callback.type == DamageType.Miss ? 0.5f : 1);
    }

    private void Flip(Transform trans, bool right)
    {
        if (right)
        {
            if (trans.localScale.x > 0) return;
        }
        else
        {
            if (trans.localScale.x < 0) return;
        }

        trans.localScale = new Vector3(-trans.localScale.x, 1, 1);
    }
}
