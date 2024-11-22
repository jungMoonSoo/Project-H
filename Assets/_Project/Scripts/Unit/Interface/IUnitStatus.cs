using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitStatus
{
    public UnitStatus Status { get; }

    public LerpSprite HpBar { get; }

    public bool OnDamage(bool _active, int _acc, int _atk, int _skp, int _cri, int _crp, int _fd);
    public bool OnDamage(bool _active, UnitStatus _status, int _fd);

    public bool OnHeal(int _value);
}
