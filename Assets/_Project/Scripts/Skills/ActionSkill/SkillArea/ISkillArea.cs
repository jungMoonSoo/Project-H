using UnityEngine;

public interface ISkillArea
{
    /// <summary>
    /// SkillArea의 Area 이펙트 및 이미지에 대한 Code
    /// </summary>
    public byte SpriteCode
    {
        get;
    }
    /// <summary>
    /// 이동 및 Sprite를 갖고있는 GameObject<br/>
    /// ISkillArea의 목적은 해당 Object를 받아서 조작하는 것에 중점을 둠.
    /// </summary>
    public GameObject GameObject
    {
        get;
        set;
    }
    /// <summary>
    /// 편의를 위해 존재하는 Transform으로, GameObject를 받았을 때 지정됨.
    /// </summary>
    public Transform Transform
    {
        get;
    }

    /// <summary>
    /// 해당 ISkillArea를 사용하는 Skill의 Interface.
    /// </summary>
    public IActionSkill Skill
    {
        get;
        set;
    }

    public ITargetingSystem TargetingSystem
    {
        get;
    }

    /// <summary>
    /// SkillArea의 Area 이미지 교체 Method
    /// </summary>
    /// <param name="sprite">교체할 Sprite</param>
    public void SetSprite(Sprite sprite);
    /// <summary>
    /// Drag등을 통해 포지션을 옮기는 Method
    /// </summary>
    /// <param name="worldPosition">터치된 InGame Position</param>
    public void SetPosition(Vector3 worldPosition);
    /// <summary>
    /// SkillArea 사이즈 조절 Method
    /// </summary>
    /// <param name="size">사이즈 값</param>
    public void SetSize(Vector2 size);
}