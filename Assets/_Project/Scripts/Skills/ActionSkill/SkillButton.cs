using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI Elements")]
    [SerializeField] private Slider mpSlider;
    [SerializeField] private Image skillImage;

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
            actionSkill = value.Status.skillInfo;
            skillImage.sprite = actionSkill.sprite;

            mpSlider.maxValue = _Caster.NowNormalStatus.maxMp;
            _Caster.Mp.SetCallback(OnMpChanged, SetCallbackType.Add);

            _Caster.DieEvent += OnCasterDie;
        }
    }
    private Unidad _Caster;
    
    
    private ActionSkillInfo actionSkill = null;


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


    private void Select()
    {
        if (_Caster.Mp.Value == _Caster.NowNormalStatus.maxMp && _Caster.Hp.Value > 0)
        {
            if (!ActionSkillManager.Instance.IsUsingSkill)
            {
                ActionSkillManager.Instance.OnSelect(Caster);
            }
            else if (ActionSkillManager.Instance.CastingCaster == Caster)
            {
                ActionSkillManager.Instance.OnCancel();
            }
        }
    }
    
    private void OnMpChanged(int currentMp)
    {
        mpSlider.value = currentMp;
    }

    private void OnCasterDie()
    {
        skillImage.color = Color.red;
        if (ActionSkillManager.Instance.CastingCaster == Caster) ActionSkillManager.Instance.OnCancel();
    }

    private void Clear()
    {
        mpSlider.value = 0;
        
        if (_Caster is not null)
        {
            _Caster.Mp.SetCallback(OnMpChanged, SetCallbackType.Remove);
            _Caster.DieEvent -= OnCasterDie;
        }
    }
}