using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitModifier/Buff", fileName = "NewBuff")]
public class BuffModifier : ScriptableObject, IUnitModifier
{
    [SerializeField] private int id;

    [SerializeField] private float count;

    [SerializeField] private bool multiply;

    [SerializeField] private AttackStatus attackStatus;
    [SerializeField] private DefenceStatus defenceStatus;

    public int Id => id;

    public float Count => count;

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

    public void Tick(Unidad unidad)
    {

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
}
