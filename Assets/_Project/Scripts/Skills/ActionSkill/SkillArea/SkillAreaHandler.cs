using System;
using UnityEngine;

public class SkillAreaHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [NonSerialized] public Vector3? LastPosition;
    public ISkillArea SkillArea;

    public void SetSize(Vector2 size)
    {
        spriteRenderer.transform.localScale = size;
    }
    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public void SetPosition(TargetType targetType, Unidad caster, Vector3 target)
    {
        transform.eulerAngles = new Vector3(90, 0, 0);

        if (SkillArea is not null)
        {
            if (targetType == TargetType.Me)
            {
                LastPosition = caster.transform.position;
                transform.position = LastPosition.Value;
            }
            else LastPosition = SkillArea.SetPosition(transform, targetType, caster, target);
        }
    }
    
    public void SetActive(bool active) => gameObject.SetActive(active);
}