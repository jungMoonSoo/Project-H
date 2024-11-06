public class UnitBind
{
    private Unit unit;

    public void Init(Unit _unit)
    {
        unit = _unit;

        unit.HpBar.action = UnitManager.Instance.lerpAction;

        unit.Status.hp[0].SetBind(HpBind);
        unit.Status.mp[0].SetBind(MpBind);
    }

    private void HpBind(ref int _current, int _change)
    {
        if (_change < 0) _change = 0;
        else if (_change > unit.Status.hp[1].Data) _change = unit.Status.hp[1].Data;

        if (_current > _change) unit.Status.mp[0].Data += (int)(unit.Status.mpRegen * 0.5f); // 마나 회복

        _current = _change;

        unit.HpBar.SetData((float)_current / unit.Status.hp[1].Data);

        if (_current == 0) unit.StateChange(UnitState.Die);
    }

    private void MpBind(ref int _current, int _change)
    {
        if (_change < 0) _change = 0;
        else if (_change > unit.Status.mp[1].Data) _change = unit.Status.mp[1].Data;

        _current = _change;

        // mpBar.SetData((float)_current / status.mp[1].Data);
    }
}
