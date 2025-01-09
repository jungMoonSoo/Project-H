using System;
using UnityEngine;

[Serializable]
public class StatusManager
{
    private readonly Unidad unidad;

    [SerializeField] public BindData<int> hp = new();
    [SerializeField] public BindData<int> mp = new();

    public ModifierManager modifierManager;

    public NormalStatus NormalStatus => modifierManager.NowNormalStatus;
    public AttackStatus AttackStatus => modifierManager.NowAttackStatus;
    public DefenceStatus DefenceStatus => modifierManager.NowDefenceStatus;

    public Action DieEvent = null;

    public StatusManager(Unidad unidad)
    {
        this.unidad = unidad;

        modifierManager = new(unidad);

        hp.SetCallback(BindHpStatusBar, SetCallbackType.Add);
        hp.SetCallback(BindHp, SetCallbackType.Set);
        hp.Value = NormalStatus.maxHp;

        DieEvent += () => hp.SetCallback(BindHpStatusBar, SetCallbackType.Remove);
        DieEvent += () => unidad.StateChange(UnitState.Die);
    }
    
    public void OnDamage(int damage) => hp.Value -= damage;

    public void OnHeal(int heal) => hp.Value += heal;

    public void BindHpStatusBar(int newValue) => unidad.statusBar?.SetBar((float)newValue / NormalStatus.maxHp);

    public void BindHp(ref int currentValue, int newValue)
    {
        if (newValue <= 0)
        {
            currentValue = 0;
            DieEvent?.Invoke();
        }
        else if (newValue > NormalStatus.maxHp) currentValue = NormalStatus.maxHp;
        else currentValue = newValue;
    }
}