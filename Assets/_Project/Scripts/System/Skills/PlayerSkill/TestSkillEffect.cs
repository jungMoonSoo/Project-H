using UnityEngine;
using UnityEngine.UI;

public class TestSkillEffect : SkillObjectBase
{
    [SerializeField]
    private float damage;
    protected override float Influence => damage;

    public override void ApplyEffect()
    {
        Unit[] units = GetEnterUnits();
        foreach (Unit unit in units)
        {
            unit.OnDamage((int)Influence);
        }

        Destroy(gameObject);
    }
}
