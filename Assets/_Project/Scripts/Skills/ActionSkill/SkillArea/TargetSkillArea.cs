using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TargetSkillArea: ISkillArea
{
    public byte SpriteCode => 0;
    public GameObject GameObject
    {
        get => _Transform.gameObject;
        set
        {
            _Transform = value.transform;
            spriteRenderer = value.GetComponentInChildren<SpriteRenderer>();
            areaTransform = spriteRenderer.transform;
            areaTransform.localScale = new Vector2(2, 1);
        }
    }
    public Transform Transform => _Transform;
    private Transform _Transform = null;

    public IActionSkill Skill
    {
        get;
        set;
    }

    private Transform areaTransform = null;
    private SpriteRenderer spriteRenderer = null;

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
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
    public void SetSize(Vector2 size)
    {
        
    }
}