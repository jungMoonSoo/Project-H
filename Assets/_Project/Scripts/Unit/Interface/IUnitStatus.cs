using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitStatus
{
    public UnitStatus Status { get; }

    public LerpSprite HpBar { get; }

    public bool OnDamage(int _value);
    public bool OnHeal(int _value);
}
