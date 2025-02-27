using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Skill Info")]
    [SerializeField] private Image imgIcon;
    [SerializeField] private TextMeshProUGUI txtCost;
    
    [Header("CoolTime UI")]
    [SerializeField] private Image imgCoolMask;
    [SerializeField] private TextMeshProUGUI txtTimer;

    /// <summary>
    /// SkillButton은 Caster만 지정해주면, 나머지는 자동으로 할당 됨
    /// </summary>
    public Unidad Caster
    {
        get => _Caster;
        set
        {
            Clear();
            
            _Caster = value;
            skillInfo = value.Status.skillInfo;
            imgIcon.sprite = skillInfo.sprite;
            txtCost.text = $"{skillInfo.manaCost}";

            _Caster.DieEvent += OnCasterDie;
        }
    }
    private Unidad _Caster;
    
    
    private ActionSkillInfo skillInfo = null;
    private bool cooled = false;


    void OnDestroy()
    {
        Clear();
    }


    #region ◇ UI Events ◇
    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Select();
    }
    public void OnDrag(PointerEventData eventData) {}
    public void OnEndDrag(PointerEventData eventData) {}
    #endregion


    /// <summary>
    /// 해당 스킬 버튼이 선택됐을 때 동작하는 Method
    /// </summary>
    private void Select()
    {
        // 사용가능 조건 확인
        if (cooled) return; // 쿨 상태인지
        if (Caster is null) return; // 스킬 주인이 존재하는지
        if (Caster.Hp.Value <= 0) return;// 스킬 주인의 체력이 1이상인지
        
        
        if (!ActionSkillManager.Instance.IsUsingSkill)
        {
            // 스킬이 미선택된 상태일 때는 해당 스킬을 선택되게
            ActionSkillManager.Instance.OnSelect(Caster, OnUse);
        }
        else if (ActionSkillManager.Instance.CastingCaster == Caster)
        {
            // 스킬이 재선택 됐을 때는 취소되게
            ActionSkillManager.Instance.OnCancel();
        }
    }

    private void OnCasterDie()
    {
        imgIcon.color = Color.red;
        if (ActionSkillManager.Instance.CastingCaster == Caster) ActionSkillManager.Instance.OnCancel();
    }
    
    private void OnUse()
    {
        CoolingAsync();
    }

    private void Clear()
    {
        if (Caster is not null)
        {
            Caster.DieEvent -= OnCasterDie;
        }
    }

    
    private async Awaitable CoolingAsync()
    {
        cooled = true;
        float timeCount = 0;
        imgCoolMask.gameObject.SetActive(true);
        
        while (timeCount < skillInfo.cooltime)
        {
            timeCount += Time.deltaTime;
            float leftTime = skillInfo.cooltime - timeCount;
            
            txtTimer.text = $"{leftTime:00}";
            imgCoolMask.fillAmount = leftTime / skillInfo.cooltime;
            
            await Awaitable.NextFrameAsync();
        }
        
        cooled = false;
        imgCoolMask.gameObject.SetActive(false);
    }
}