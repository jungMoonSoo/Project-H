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
            unidad.ModifierManager.SetModifierMultiply(attackStatus, true);
            unidad.ModifierManager.SetModifierMultiply(defenceStatus, true);
        }
        else
        {
            unidad.ModifierManager.SetModifier(attackStatus, true);
            unidad.ModifierManager.SetModifier(defenceStatus, true);
        }
    }

    public void Remove(Unidad unidad)
    {
        if (multiply)
        {
            unidad.ModifierManager.SetModifierMultiply(attackStatus, false);
            unidad.ModifierManager.SetModifierMultiply(defenceStatus, false);
        }
        else
        {
            unidad.ModifierManager.SetModifier(attackStatus, false);
            unidad.ModifierManager.SetModifier(defenceStatus, false);
        }
    }

    public virtual int Cycle(Unidad unidad) => 1;
}
