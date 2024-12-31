using System;
using System.Threading.Tasks;
using UnityEngine;

public class ActionSkillManager: Singleton<ActionSkillManager>
{
    [SerializeField] private SkillAreaHandler skillAreaHandler;
    
    [Header("Action Skill Settings")]
    [SerializeField] private Transform skillButtonGroup;
    [SerializeField] private GameObject skillButtonPrefab;
    [SerializeField] private Unidad unidad;
    
    public bool IsUsingSkill => CastingCaster is not null;
    public Unidad CastingCaster { get; private set; }
    public ActionSkillInfo UsingSkill => CastingCaster.Status.skillInfo;
    
    private bool hasPosition = false;

    
    void Start()
    {
        skillAreaHandler.SetActive(false);
        AddSkillButton(unidad);
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
                OnApplySkill(touchInfo.pos);
            }
        }
    }


    public void AddSkillButton(Unidad unidad)
    {
        Vector2 skillButtonPosition = Vector2.zero;
        
        GameObject instance = Instantiate(skillButtonPrefab, skillButtonGroup);
        instance.transform.localPosition = skillButtonPosition;
        
        SkillButton skillButton = instance.GetComponent<SkillButton>();
        skillButton.Caster = unidad;
    }
    
    public void OnSelect(Unidad caster)
    {
        if(caster is null) return;
        
        InGameManager.Instance.PauseGame(PauseType.UseSkill);
        
        CastingCaster = caster;
        skillAreaHandler.SetSprite(UsingSkill.skillArea.areaImage);
        skillAreaHandler.SkillArea = UsingSkill.skillArea.SkillArea;
    }
    public void OnCancel()
    {
        InGameManager.Instance.ResumeGame(PauseType.UseSkill);
        CastingCaster = null;
        
        skillAreaHandler.SetActive(false);
    }

    private void OnApplySkill(Vector3 target)
    {
        InGameManager.Instance.ResumeGame(PauseType.UseSkill);
        
        // TODO: 스킬 적용 코드 필요
        //UsingSkill?.ApplyAction(screenPosition);
        CastingCaster = null;
        
        skillAreaHandler.SetActive(false);
    }

    private void OnDrag(Vector3 target)
    {
        if (!skillAreaHandler.gameObject.activeSelf)
        {
            skillAreaHandler.SetActive(true);
        }
        // TODO: 스킬 드래그로 이벤트 필요
        skillAreaHandler.SetPosition(UsingSkill.targetType, CastingCaster, target);
        //UsingSkill?.OnDrag(screenPosition);
    }
}