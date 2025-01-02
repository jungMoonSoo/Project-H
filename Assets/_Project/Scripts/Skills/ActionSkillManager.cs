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
                OnApplySkill(skillAreaHandler.LastPosition);
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