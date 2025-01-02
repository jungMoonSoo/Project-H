using System;
using UnityEngine;

public class SkillAreaHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [NonSerialized] public Vector2? LastPosition;
    public ISkillArea SkillArea;

    public void SetSize(Vector2 size)
    {
        spriteRenderer.transform.localScale = size;
    }
    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public void SetPosition(TargetType targetType, Unidad caster, Vector2 target)
    {
        if (SkillArea is not null)
        {
            LastPosition = SkillArea.SetPosition(transform, targetType, caster, target);
        }
    }
    
    public void SetActive(bool active) => gameObject.SetActive(active);
}