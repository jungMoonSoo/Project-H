using Unity.VisualScripting;
using UnityEngine;

public class LinearSkillArea: ISkillArea
{
    public byte SpriteCode => 1;
    public GameObject GameObject
    {
        get => _Transform.gameObject;
        set
        {
            _Transform = value.transform;
            spriteRenderer = value.GetComponentInChildren<SpriteRenderer>();
            areaTransform = spriteRenderer.transform;
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
        Transform.position = InGameManager.Instance.PlayerTransform.position + Vector3.forward;

        float angle = VectorCalc.CalcRotation(worldPosition - Transform.position);
        Transform.eulerAngles.Set(0, 0, angle);
    }
    public void SetSize(Vector2 size)
    {
        areaTransform.localScale = size;
    }
}