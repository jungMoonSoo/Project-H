using System;
using UnityEngine;

public class ActionSkillManager: Singleton<ActionSkillManager>
{
    public bool IsUsingSkill
    {
        get => usingSkill != null;
    }
    
    private IActionSkill usingSkill = null;
    private bool hasPosition = false;

    
    void Update()
    {
        if (usingSkill is not null)
        {
            if (Input.GetMouseButton(0))
            {
                hasPosition = true;
            }
            else if(hasPosition)
            {
                hasPosition = false;
                UseSkill(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }

    
    public void SelectSkill(IActionSkill skill)
    {
        usingSkill = skill;
    }

    private void UseSkill(Vector3 worldPosition)
    {
        usingSkill?.ApplyAction(worldPosition);
        usingSkill = null;
    }
}