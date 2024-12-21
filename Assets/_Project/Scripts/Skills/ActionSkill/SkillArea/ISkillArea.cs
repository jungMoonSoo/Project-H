using UnityEngine;

public interface ISkillArea
{
    public byte SpriteCode
    {
        get;
    }
    public GameObject GameObject
    {
        get;
        set;
    }
    public Transform Transform
    {
        get;
    }

    public IActionSkill Skill
    {
        get;
        set;
    }

    public void SetSprite(Sprite sprite);
    public void SetPosition(Vector3 worldPosition);
    public void SetSize(Vector2 size);
}