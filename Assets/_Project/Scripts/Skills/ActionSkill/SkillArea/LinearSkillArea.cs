using Unity.VisualScripting;
using UnityEngine;

public class LinearSkillArea: ISkillArea
{
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

    private Transform areaTransform = null;
    private SpriteRenderer spriteRenderer = null;

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public void SetPosition(Vector3 position)
    {
        Transform.position = InGameManager.Instance.PlayerTransform.position;

        float angle = VectorCalc.CalcRotation(position - Transform.position);
        Transform.eulerAngles = new Vector3(0, 0, angle);
    }
    public void SetSize(Vector2 size)
    {
        areaTransform.localScale = size;
    }
}