using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitStatus
{
    public UnitStatus Status { get; }

    public LerpSprite HpBar { get; }

    public void SetHp(int _value);
}
