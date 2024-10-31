using UnityEngine;

public interface ISkill
{
    public GameObject SkillEffect
    {
        get;
    }
    public void Execute();
}