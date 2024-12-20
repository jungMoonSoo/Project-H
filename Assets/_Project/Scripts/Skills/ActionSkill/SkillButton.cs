using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI Elements")]
    [SerializeField] private Slider mpSlider;
    [SerializeField] private Image skillImage;

    public IActionSkill ActionSkill = new DefaultActionSkill();


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
            ActionSkillManager.Instance.OnSelect(ActionSkill);
        }
    }
}