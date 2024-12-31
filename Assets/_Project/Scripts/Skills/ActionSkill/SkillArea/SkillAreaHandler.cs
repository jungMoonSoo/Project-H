using UnityEngine;

public class SkillAreaHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    
    public ISkillArea SkillArea;

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public void SetPosition(TargetType targetType, Unidad caster, Vector2 target)
    {
        SkillArea?.SetPosition(transform, targetType, caster, target);
    }
    
    public void SetActive(bool active) => gameObject.SetActive(active);
}