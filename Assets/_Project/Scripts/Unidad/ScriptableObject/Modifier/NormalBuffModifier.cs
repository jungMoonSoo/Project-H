using UnityEngine;

[CreateAssetMenu(menuName = "UnitModifier/NormalBuff", fileName = "NewNormalBuff")]
public class NormalBuffModifier : ScriptableObject, IUnitModifier
{
    [SerializeField] private int id;

    [SerializeField] private int cycleCount;
    [SerializeField] private float intervalTime;

    [SerializeField] private bool multiply;

    [SerializeField] private AttackStatus attackStatus;
    [SerializeField] private DefenceStatus defenceStatus;

    public int Id => id;

    public int CycleCount => cycleCount;

    private float applyTime;

    public virtual void Add(Unidad unidad)
    {
        applyTime = Time.time;

        SetModifier(unidad.ModifierManager, true);
    }

    public virtual void Remove(Unidad unidad) => SetModifier(unidad.ModifierManager, false);

    public virtual int Cycle(Unidad unidad)
    {
        if (applyTime > Time.time) return 0;

        applyTime = Time.time + intervalTime;

        return 1;
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
