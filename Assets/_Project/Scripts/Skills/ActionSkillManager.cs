using System;
using UnityEngine;

public class ActionSkillManager: Singleton<ActionSkillManager>
{
    [SerializeField] private Sprite[] skillAreaSprites;
    [SerializeField] private GameObject skillAreaObject;
    
    public bool IsUsingSkill
    {
        get => usingSkill != null;
    }

    private IActionSkill usingSkill = null;
    private bool hasPosition = false;

    
    void Start()
    {
        skillAreaObject.SetActive(false);
    }
    
    void Update()
    {
        if (usingSkill is not null)
        {
            TouchInfo touchInfo = TouchSystem.Instance.GetTouch(0);
            if (touchInfo.count > 0)
            {
                if (!hasPosition)
                {
                    hasPosition = true;
                }
                else
                {
                    OnDrag(touchInfo.pos);
                }
            }
            else if(hasPosition)
            {
                hasPosition = false;
                OnApplySkill(touchInfo.pos);
            }
        }
    }


    public void OnSelect(IActionSkill skill)
    {
        if(skill is null) return;
        
        InGameManager.Instance.PauseGame(PauseType.UseSkill);
        
        usingSkill = skill;
        usingSkill.OnSelect();
        
        ISkillArea skillArea = usingSkill.SkillArea;
        skillArea.GameObject = skillAreaObject;
        skillArea.SetSize(usingSkill.AreaSize);
        skillArea.SetSprite(skillAreaSprites[usingSkill.SkillArea.SpriteCode]);
    }

    private void OnApplySkill(Vector3 screenPosition)
    {
        InGameManager.Instance.ResumeGame(PauseType.UseSkill);
        
        usingSkill?.ApplyAction(screenPosition);
        usingSkill = null;
        
        skillAreaObject.SetActive(false);
    }

    private void OnDrag(Vector3 screenPosition)
    {
        if (!skillAreaObject.activeSelf)
        {
            skillAreaObject.SetActive(true);
        }
        usingSkill?.OnDrag(screenPosition);
    }
}