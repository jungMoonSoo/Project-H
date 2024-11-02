using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitSkillBase : MonoBehaviour, IUnitSkill
{
    protected Unit unit;

    [Range(0f, 1f)] public float skillAttackPoint;

    public int ManaCost => unit.status.mp[0].Data;

    protected readonly List<Unit> targets = new();

    public GameObject SkillEffect { get; }

    public void Awake()
    {
        TryGetComponent(out unit);
    }

    public abstract void Execute();
}
