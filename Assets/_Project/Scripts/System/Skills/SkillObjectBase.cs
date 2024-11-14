using System.Collections.Generic;
using UnityEngine;

public abstract class SkillObjectBase: MonoBehaviour, ISkillObject
{
    public Vector2 EffectRange
    {
        get;
        set;
    }

    protected abstract float Influence
    {
        get;
    }

    private EllipseCollider ellipseCollider = null;

    void Start()
    {
        ellipseCollider = GetComponent<EllipseCollider>();
        ellipseCollider.ranges = new() { EffectRange * 0.5f };
        ellipseCollider.SetArea(UnitManager.Instance.mapPos, UnitManager.Instance.mapSize);
    }

    protected virtual Unit[] GetEnterUnits()
    {
        List<Unit> result = new();
        Unit[] units = GameObject.FindObjectsOfType<Unit>();
        foreach (Unit unit in units)
        {
            if(ellipseCollider.OnEllipseEnter(transform.position, unit.EllipseCollider, EllipseType.Unit, EllipseType.Unit) <= 1f)
            {
                result.Add(unit);
            }
        }

        return result.ToArray();
    }


    public abstract void ApplyEffect();
}