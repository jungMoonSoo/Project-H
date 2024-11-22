using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitStatus
{
    public UnitStatus Status { get; }

    public LerpSprite HpBar { get; }

    public bool OnDamage(bool _active, DamageStatus _status, int _fd);

    public bool OnHeal(int _value);
}
