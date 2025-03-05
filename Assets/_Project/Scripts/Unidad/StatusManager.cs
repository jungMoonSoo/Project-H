using System;

public class StatusManager
{
    private readonly Unidad unidad;

    public BindData<int> hp = new();

    public ModifierManager modifierManager;

    public NormalStatus NormalStatus => modifierManager.NowNormalStatus;
    public AttackStatus AttackStatus => modifierManager.NowAttackStatus;
    public DefenceStatus DefenceStatus => modifierManager.NowDefenceStatus;

    public StatusManager(Unidad unidad)
    {
        this.unidad = unidad;

        modifierManager = new(unidad);

        hp.Value = NormalStatus.maxHp;
        hp.SetCallback(BindHp, SetCallbackType.Set);
    }
    
    public void OnDamage(int damage) => hp.Value -= damage;

    public void OnHeal(int heal) => hp.Value += heal;

    public void BindHp(ref int currentValue, int newValue)
    {
        if (newValue <= 0)
        {
            currentValue = 0;
            unidad.ChangeState(UnitState.Die);
        }
        else if (newValue > NormalStatus.maxHp) currentValue = NormalStatus.maxHp;
        else currentValue = newValue;
    }
}