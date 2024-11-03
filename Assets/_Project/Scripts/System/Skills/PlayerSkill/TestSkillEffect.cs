using UnityEngine;

public class TestSkillEffect : SkillObjectBase
{
    [SerializeField]
    private float damage;
    protected override float Influence => damage;

    public override void ApplyEffect()
    {
        Unit[] units = GameObject.FindObjectsOfType<Unit>();
        foreach (Unit unit in units)
        {
            unit.status.hp[0].Data -= (int)Influence;
        }

        Destroy(gameObject);
    }
}
