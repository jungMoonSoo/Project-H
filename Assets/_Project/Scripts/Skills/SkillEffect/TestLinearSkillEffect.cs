using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestLinearSkillEffect : SkillEffectBase
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
    
    private readonly List<Unidad> hitUnits = new();
    private EllipseCollider ellipseCollider = null;

    public override void ApplyEffect()
    {
        Unidad[] units = Targets.Where(x => !hitUnits.Contains(x)).ToArray();
        foreach (Unidad unit in units)
        {
            // unit.OnDamage((int)Influence);
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

    public override Unidad[] GetTargets()
    {
        SetCollider();
        return UnidadManager.Instance.GetUnidads(UnitType.Enemy).Where(x => ellipseCollider.OnEllipseEnter(x.skillCollider)).ToArray();
    }

    private void SetCollider()
    {
        if (ellipseCollider != null) return; // 콜라이더가 없을 때, 생성하는 Method임.
        
        ellipseCollider = GetComponent<EllipseCollider>();
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
