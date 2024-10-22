public class UnitBind
{
    private Unit unit;

    private readonly LerpAction lerpAction = new();

    public void Init(Unit _unit)
    {
        unit = _unit;

        unit.hpBar.action = lerpAction;

        unit.status.hp[0].SetBind(HpBind);
        unit.status.mp[0].SetBind(MpBind);
    }

    public void Action()
    {
        lerpAction.actions?.Invoke();
    }

    private void HpBind(ref int _current, int _change)
    {
        if (_change < 0) _change = 0;
        else if (_change > unit.status.hp[1].Data) _change = unit.status.hp[1].Data;

        if (_current > _change) unit.status.mp[0].Data += (int)(unit.status.mpRegen.Data * 0.5f); // 마나 회복

        _current = _change;

        unit.hpBar.SetData((float)_current / unit.status.hp[1].Data);

        if (_current == 0) unit.StateChange(UnitState.Die);
    }

    private void MpBind(ref int _current, int _change)
    {
        if (_change < 0) _change = 0;
        else if (_change > unit.status.mp[1].Data) _change = unit.status.mp[1].Data;

        _current = _change;

        if (_current == unit.status.mp[1].Data)
        {
            unit.skills?.Invoke();

            _current = 0;
        }

        // mpBar.SetData(lerpAction, (float)_current / status.mp[1].Data);
    }
}
