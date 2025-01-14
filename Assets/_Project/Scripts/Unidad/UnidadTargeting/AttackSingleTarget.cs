using UnityEngine;

public class AttackSingleTarget : MonoBehaviour, IUnidadAttack
{
    [SerializeField] private float skillCoefficient = 100f;
    [SerializeField] private GameObject effect;

    public void OnAttack(Unidad unidad, Unidad[] targets)
    {
        Unidad target = targets[0];

        CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(unidad.NowAttackStatus, target.NowDefenceStatus, skillCoefficient, 0, ElementType.None);
        target.OnDamage((int)callback.value, callback.type, effect);

        unidad.IncreaseMp(callback.type == DamageType.Miss ? 0.5f : 1);
    }
}
