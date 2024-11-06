using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitStatus
{
    public UnitStatus Status { get; }

    public LerpSprite HpBar { get; }

    public void OnDamage(int _value);
    public void OnHeal(int _value);
}
