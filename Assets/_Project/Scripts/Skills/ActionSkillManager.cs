using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어가 직접 사용하는 스킬 및 공용 마나를 관리하기 위한 Class
/// </summary>
public class ActionSkillManager: Singleton<ActionSkillManager>
{
    [SerializeField] private SkillAreaHandler skillAreaHandler;
    
    [Header("Action Skill Settings")]
    [SerializeField] private Transform skillButtonGroup;
    [SerializeField] private SkillButton skillButtonPrefab;
    
    [Header("Mana Settings")]
    [SerializeField] private float maxMana; // 플레이어의 최대 마나
    [SerializeField] private Slider sldMana; // 마나를 표시할 슬라이더 UI
    [SerializeField] private TextMeshProUGUI txtMana; // 마나를 표시할 슬라이더 UI
    
    
    public bool IsUsingSkill => CastingCaster is not null;
    public Unidad CastingCaster { get; private set; }
    public ActionSkillInfo UsingSkill => CastingCaster.Status.skillInfo;
    
    private bool hasPosition = false;
    private List<SkillButton> createdSkillButtons = new(); // 스킬 버튼 리스트, 생성했을 때 Stack하여 버튼을 삭제할 때 사용하기 위한 리스트
    
    private readonly BindData<float> nowMana = new(); // 플레이어의 현재 마나

    
    void Start()
    {
        skillAreaHandler.SetActive(false);
        nowMana.SetCallback(BindMana);
        nowMana.Value = maxMana;
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


    /// <summary>
    /// 캐릭터 스킬 버튼 생성
    /// </summary>
    /// <param name="unidad">스킬을 가진 캐릭터</param>
    public void AddSkillButton(Unidad unidad)
    {
        SkillButton skillButton = Instantiate(skillButtonPrefab, skillButtonGroup);

        skillButton.Caster = unidad;
        createdSkillButtons.Add(skillButton);
    }

    /// <summary>
    /// 캐릭터 스킬 버튼 전체 삭제
    /// </summary>
    public void ClearSkillButtons()
    {
        foreach (SkillButton skillButton in createdSkillButtons) Destroy(skillButton.gameObject);

        createdSkillButtons.Clear();
    }
    
    
    /// <summary>
    /// 사용을 위한 스킬 선택
    /// </summary>
    /// <param name="caster">스킬을 가진 캐릭터</param>
    public void OnSelect(Unidad caster)
    {
        if(caster is null) return;
        if(nowMana.Value < caster.Status.skillInfo.manaCost) return;
        
        InGameManager.Instance.PauseGame(PauseType.UseSkill);
        
        CastingCaster = caster;

        skillAreaHandler.SetSize(UsingSkill.effectPrefab.GetAreaSize());

        skillAreaHandler.SetSprite(UsingSkill.areaImage);
        skillAreaHandler.SkillArea = SkillAreaHub.GetSkillArea(UsingSkill.skillAreaType);
    }
    /// <summary>
    /// 스킬 사용 취소
    /// </summary>
    public void OnCancel()
    {
        InGameManager.Instance.ResumeGame(PauseType.UseSkill);
        CastingCaster = null;
        hasPosition = false;
        
        skillAreaHandler.SetActive(false);
    }

    /// <summary>
    /// 스킬 사용
    /// </summary>
    /// <param name="target">스킬을 사용하는 위치</param>
    private void OnApplySkill(Vector3? target)
    {
        InGameManager.Instance.ResumeGame(PauseType.UseSkill);

        if (target is not null && // 타겟이 존재하지 않거나 = 스킬 사용이 취소
            CastingCaster is not null && // 스킬 시전자가 존재하지 않거나 = 시전자 사망 
            nowMana.Value >= CastingCaster.Status.skillInfo.manaCost) // 마나가 없으면
        {
            HitObjectBase handler = Instantiate(UsingSkill?.effectPrefab);

            handler.SetTargetPos((Vector3)target);
            handler.Init(CastingCaster, null, CastingCaster.transform.position);

            CastingCaster.ChangeState(UnitState.Skill);
            //CastingCaster.Mp.Value = 0;
            nowMana.Value -= CastingCaster.Status.skillInfo.manaCost;
        }
        
        // Skill 제거
        CastingCaster = null;
        skillAreaHandler.SetActive(false);
    }

    /// <summary>
    /// 스킬 드래그
    /// </summary>
    /// <param name="target"></param>
    private void OnDrag(Vector3 target)
    {
        if (!skillAreaHandler.gameObject.activeSelf) skillAreaHandler.SetActive(true);

        skillAreaHandler.SetPosition(CastingCaster, target);
    }
    
    
    
    private void BindMana(ref float currentValue, float newValue)
    {
        if (newValue <= 0) currentValue = 0;
        else if (newValue > maxMana) currentValue = maxMana;
        else currentValue = newValue;
        
        sldMana.maxValue = maxMana;
        sldMana.value = currentValue;
        txtMana.text = $"{currentValue} / {maxMana}";
    }
}