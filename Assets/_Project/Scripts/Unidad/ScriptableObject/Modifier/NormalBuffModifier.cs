using UnityEngine;

[CreateAssetMenu(menuName = "UnitModifier/NormalBuff", fileName = "NewNormalBuff")]
public class NormalBuffModifier : ScriptableObject, IUnitModifier
{
    [SerializeField] private int id;

    [SerializeField] private int count;

    [SerializeField] private bool multiply;

    [SerializeField] private AttackStatus attackStatus;
    [SerializeField] private DefenceStatus defenceStatus;

    public int Id => id;

    public int Count => count;

    public void Add(Unidad unidad)
    {
        if (multiply)
        {
            unidad.ModifierHandle.SetModifierMultiply(attackStatus, true);
            unidad.ModifierHandle.SetModifierMultiply(defenceStatus, true);
        }
        else
        {
            unidad.ModifierHandle.SetModifier(attackStatus, true);
            unidad.ModifierHandle.SetModifier(defenceStatus, true);
        }
    }

    public void Remove(Unidad unidad)
    {
        if (multiply)
        {
            unidad.ModifierHandle.SetModifierMultiply(attackStatus, false);
            unidad.ModifierHandle.SetModifierMultiply(defenceStatus, false);
        }
        else
        {
            unidad.ModifierHandle.SetModifier(attackStatus, false);
            unidad.ModifierHandle.SetModifier(defenceStatus, false);
        }
    }

    public virtual int Check(Unidad unidad) => 1;
}
