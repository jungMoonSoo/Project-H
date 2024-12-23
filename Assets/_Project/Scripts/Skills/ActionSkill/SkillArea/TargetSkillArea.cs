using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TargetSkillArea: SkillAreaBase, ISkillArea
{
    public byte SpriteCode => 0;

    public void SetPosition(Vector3 worldPosition)
    {
        Unidad[] enemies = UnidadManager.Instance.unidades.Where(x => x.Owner != UnitType.Ally).ToArray();
        Unidad target = enemies[0];
        float minRange = float.MaxValue;
            
        foreach (Unidad enemy in enemies)
        {
            Vector2 dir = enemy.transform.position - Skill.Caster.transform.position;
            float range = dir.magnitude;
            
            if (range < minRange)
            {
                minRange = range;
                target = enemy;
            }
        }
        
        Transform.position = target.transform.position + Vector3.forward;
    }
    public override void SetSize(Vector2 size)
    {
        AreaTransform.localScale = new Vector2(2, 1);
    }
}