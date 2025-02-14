using System.Collections.Generic;
using UnityEngine;

public class ActionSkillManager: Singleton<ActionSkillManager>
{
    [SerializeField] private SkillAreaHandler skillAreaHandler;
    
    [Header("Action Skill Settings")]
    [SerializeField] private Transform skillButtonGroup;
    [SerializeField] private SkillButton skillButtonPrefab;
    
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
            TouchInfo touchInfo = TouchSystem.GetTouch(0);

            if (touchInfo.count > 0)
            {
                if (!hasPosition)
                {
                    hasPosition = true;
                }
                else
                {
                    OnDrag(touchInfo.GetSpecificYPos(0));
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
        SkillButton skillButton = Instantiate(skillButtonPrefab, skillButtonGroup);

        skillButton.Caster = unidad;
        createdSkillButtons.Add(skillButton);
    }

    public void ClearSkillButtons()
    {
        foreach (SkillButton skillButton in createdSkillButtons) Destroy(skillButton.gameObject);

        createdSkillButtons.Clear();
    }
    
    
    public void OnSelect(Unidad caster)
    {
        if(caster is null) return;
        
        InGameManager.Instance.PauseGame(PauseType.UseSkill);
        
        CastingCaster = caster;

        skillAreaHandler.SetSize(UsingSkill.effectPrefab.GetAreaSize());

        skillAreaHandler.SetSprite(UsingSkill.areaImage);
        skillAreaHandler.SkillArea = SkillAreaHub.GetSkillArea(UsingSkill.skillAreaType);
    }
    public void OnCancel()
    {
        InGameManager.Instance.ResumeGame(PauseType.UseSkill);
        CastingCaster = null;
        hasPosition = false;
        
        skillAreaHandler.SetActive(false);
    }

    private void OnApplySkill(Vector3? target)
    {
        InGameManager.Instance.ResumeGame(PauseType.UseSkill);

        if (target is not null && CastingCaster is not null) // 타겟이 미존재거나, 시전자가 미존재면 스킬사용 실패
        {
            HitObjectBase handler = Instantiate(UsingSkill?.effectPrefab, CastingCaster.transform.position, Quaternion.identity);
            handler.Init(CastingCaster, null, (Vector3)target);

            CastingCaster.ChangeState(UnitState.Skill);
            CastingCaster.Mp.Value = 0;
        }
        
        // Skill 제거
        CastingCaster = null;
        skillAreaHandler.SetActive(false);
    }

    private void OnDrag(Vector3 target)
    {
        if (!skillAreaHandler.gameObject.activeSelf) skillAreaHandler.SetActive(true);

        skillAreaHandler.SetPosition(CastingCaster, target);
    }
}