using System;

public class StatusManager
{
    private readonly Unidad unidad;

    public BindData<int> hp = new();

    public ModifierManager modifierManager;

    public StatusManager(Unidad unidad)
    {
        this.unidad = unidad;

        modifierManager = new(unidad);

        hp.Value = modifierManager.NowNormalStatus.maxHp;
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
        else if (newValue > modifierManager.NowNormalStatus.maxHp) currentValue = modifierManager.NowNormalStatus.maxHp;
        else currentValue = newValue;
    }
}