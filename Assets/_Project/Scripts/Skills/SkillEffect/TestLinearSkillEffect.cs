using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestLinearSkillEffect : SkillObjectBase
{
    public float MoveSpeed
    {
        get;
        set;
    } = 2f;

    public int HitCount
    {
        get;
        set;
    } = 1;
    
    private readonly List<Unit> hitUnits = new();
    private EllipseCollider ellipseCollider = null;

    public override void ApplyEffect()
    {
        Unit[] units = Targets.Where(x => !hitUnits.Contains(x)).ToArray();
        foreach (Unit unit in units)
        {
            unit.OnDamage((int)Influence);
        }


        if (HitCount > 0)
        {
            hitUnits.AddRange(units);
            if (hitUnits.Count >= HitCount)
            {
                Destroy(gameObject);
            }
        }
    }

    public override void SetPosition(Vector2 position)
    {
        StartCoroutine(MoveSkill(position));
    }

    public override Unit[] GetTargets()
    {
        SetCollider();
        return UnitManager.Instance.units.Where(x => ellipseCollider.OnEllipseEnter(transform.position, x.EllipseCollider, EllipseType.Unit, EllipseType.Unit) <= 1f).ToArray();
    }

    private void SetCollider()
    {
        if (ellipseCollider != null) return; // 콜라이더가 없을 때, 생성하는 Method임.
        
        ellipseCollider = GetComponent<EllipseCollider>();
        if (ellipseCollider != null)
        {
            ellipseCollider.ranges = new List<Vector2>() { EffectRange * 0.5f };
            ellipseCollider.SetArea(UnitManager.Instance.mapPos, UnitManager.Instance.mapSize);
        }
    }

    private IEnumerator MoveSkill(Vector2 position)
    {
        while (Vector2.Distance(transform.position, position) > 0.03125f)
        {
            transform.position = Vector2.MoveTowards(transform.position, position, MoveSpeed * Time.deltaTime);
            if (Targets.Length > 0)
            {
                ApplyEffect();
            }

            yield return null;
        }
        
        Destroy(gameObject);
    }
}