using UnityEngine;

public abstract class SkillAreaBase
{
    public GameObject GameObject
    {
        get => _Transform.gameObject;
        set
        {
            _Transform = value.transform;
            SpriteRenderer = value.GetComponentInChildren<SpriteRenderer>();
            AreaTransform = SpriteRenderer.transform;
        }
    }
    public Transform Transform => _Transform;
    private Transform _Transform = null;

    public IActionSkill Skill
    {
        get;
        set;
    }

    protected Transform AreaTransform = null;
    protected SpriteRenderer SpriteRenderer = null;

    public virtual void SetSprite(Sprite sprite)
    {
        SpriteRenderer.sprite = sprite;
    }
    public virtual void SetSize(Vector2 size)
    {
        AreaTransform.localScale = size;
    }
}