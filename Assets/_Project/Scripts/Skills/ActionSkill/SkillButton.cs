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
            _Caster = value;
            actionSkill = value.Status.skillInfo;
            skillImage.sprite = actionSkill.sprite;

            mpSlider.maxValue = _Caster.NowNormalStatus.maxMp;
            _Caster.Mp.SetCallback(UpdateMpSlider, SetCallbackType.Add);
            UpdateMpSlider(_Caster.Mp.Value);
        }
    }
    private Unidad _Caster;
    
    
    private ActionSkillInfo actionSkill = null;

    private void UpdateMpSlider(int currentMp)
    {
        mpSlider.value = currentMp;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelect();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnSelect();
    }
    public void OnDrag(PointerEventData eventData) {}
    public void OnEndDrag(PointerEventData eventData) {}

    
    private void OnSelect()
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