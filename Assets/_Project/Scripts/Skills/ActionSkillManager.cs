using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ActionSkillManager: Singleton<ActionSkillManager>
{
    [SerializeField] private SkillAreaHandler skillAreaHandler;
    
    [Header("Action Skill Settings")]
    [SerializeField] private Transform skillButtonGroup;
    [SerializeField] private GameObject skillButtonPrefab;
    
    public bool IsUsingSkill => CastingCaster is not null;
    public Unidad CastingCaster { get; private set; }
    public ActionSkillInfo UsingSkill => CastingCaster.Status.skillInfo;
    
    private bool hasPosition = false;
    private List<SkillButton> createdSkillButtons = new();

    
    void Start()
    {
        skillAreaHandler.SetActive(false);
    }
    
    void Update()
    {
        if (IsUsingSkill)
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
                OnApplySkill(skillAreaHandler.LastPosition);
            }
        }
    }


    public void AddSkillButton(Unidad unidad)
    {
        GameObject instance = Instantiate(skillButtonPrefab, skillButtonGroup);
        
        SkillButton skillButton = instance.GetComponent<SkillButton>();
        skillButton.Caster = unidad;
        createdSkillButtons.Add(skillButton);
    }

    public void ClearSkillButtons()
    {
        foreach (SkillButton skillButton in createdSkillButtons)
        {
            Destroy(skillButton.gameObject);
        }
        createdSkillButtons.Clear();
    }
    
    
    public void OnSelect(Unidad caster)
    {
        if(caster is null) return;
        
        InGameManager.Instance.PauseGame(PauseType.UseSkill);
        
        CastingCaster = caster;
        skillAreaHandler.SetSprite(UsingSkill.skillArea.areaImage);
        skillAreaHandler.SetSize(UsingSkill.skillArea.areaSize);
        skillAreaHandler.SkillArea = UsingSkill.skillArea.SkillArea;
    }
    public void OnCancel()
    {
        InGameManager.Instance.ResumeGame(PauseType.UseSkill);
        CastingCaster = null;
        
        skillAreaHandler.SetActive(false);
    }

    private void OnApplySkill(Vector2? target)
    {
        InGameManager.Instance.ResumeGame(PauseType.UseSkill);

        if (target is not null) // 미존재면 스킬사용 실패
        {
            SkillEffectHandler handler = Instantiate(UsingSkill?.effectPrefab, CastingCaster.transform.position, Quaternion.identity).GetComponent<SkillEffectHandler>();
            handler.Init(CastingCaster, (Vector2)target);
        }
        
        // Skill 제거
        CastingCaster = null;
        skillAreaHandler.SetActive(false);
    }

    private void OnDrag(Vector2 target)
    {
        if (!skillAreaHandler.gameObject.activeSelf)
        {
            skillAreaHandler.SetActive(true);
        }
        skillAreaHandler.SetPosition(UsingSkill.targetType, CastingCaster, target);
    }
}