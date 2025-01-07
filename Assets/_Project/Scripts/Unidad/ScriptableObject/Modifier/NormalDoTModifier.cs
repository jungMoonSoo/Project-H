using UnityEngine;

[CreateAssetMenu(menuName = "UnitModifier/NormalDoT", fileName = "NewNormalDoT")]
public class NormalDoTModifier : ModifierBase
{
    [SerializeField] private int damage;

    public override int Cycle(Unidad unidad)
    {
        if (base.Cycle(unidad) == 0) return 0;

        unidad.OnDamage(damage, DamageType.Normal);

        return 1;
    }
}
