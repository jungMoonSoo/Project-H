using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI Elements")]
    [SerializeField] private Slider mpSlider;
    [SerializeField] private Image skillImage;

    public IActionSkill ActionSkill = new EllipseActionSkill();


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!ActionSkillManager.Instance.IsUsingSkill)
        {
            ActionSkillManager.Instance.SelectSkill(ActionSkill);
            Debug.Log("Click");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!ActionSkillManager.Instance.IsUsingSkill)
        {
            ActionSkillManager.Instance.SelectSkill(ActionSkill);
            Debug.Log("Begin Drag");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}