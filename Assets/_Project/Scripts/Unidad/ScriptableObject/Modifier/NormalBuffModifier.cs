using UnityEngine;

[CreateAssetMenu(menuName = "UnitModifier/NormalBuff", fileName = "NewNormalBuff")]
public class NormalBuffModifier : ModifierBase
{
    [SerializeField] private bool multiply;

    [SerializeField] private AttackStatus attackStatus;
    [SerializeField] private DefenceStatus defenceStatus;

    public override void Add(Unidad unidad)
    {
        base.Add(unidad);

        SetModifier(unidad.ModifierManager, true);
    }

    public override void Remove(Unidad unidad)
    {
        base.Remove(unidad);

        SetModifier(unidad.ModifierManager, false);
    }

    private void SetModifier(ModifierManager modifierManager, bool apply)
    {
        if (multiply)
        {
            modifierManager.SetModifierMultiply(attackStatus, apply);
            modifierManager.SetModifierMultiply(defenceStatus, apply);
        }
        else
        {
            modifierManager.SetModifier(attackStatus, apply);
            modifierManager.SetModifier(defenceStatus, apply);
        }
    }
}
