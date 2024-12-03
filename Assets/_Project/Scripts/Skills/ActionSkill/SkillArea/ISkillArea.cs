using UnityEngine;

public interface ISkillArea
{
    public GameObject GameObject
    {
        get;
        set;
    }
    public Transform Transform
    {
        get;
    }

    public void SetSprite(Sprite sprite);
    public void SetPosition(Vector3 position);
    public void SetSize(Vector2 size);
}