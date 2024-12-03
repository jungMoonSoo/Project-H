using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSkillButton: MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform skillGroup;
    [SerializeField]
    private ActionSkillBase playerSkill;


    private Camera MainCamera
    {
        get => Camera.main;
    }
    private RectTransform SkillGroup
    {
        get
        {
            if(skillGroup == null)
            {
                skillGroup = transform.parent.GetComponent<RectTransform>();
            }
            return skillGroup;
        }
    }
    private ActionSkillBase PlayerSkill
    {
        get
        {
            if(playerSkill == null)
            {
                playerSkill = GetComponent<ActionSkillBase>();
                if (playerSkill == null)
                {
                    Logger.Warning("스킬 버튼 조건이 완벽하지 않음.", name, playerSkill);
                }
            }
            return playerSkill;
        }
    }
    

    private bool isUsing = false;

    
    private void OnSelect()
    {
        if (!PlayerSkill.IsCooled)
        {
            isUsing = true;
        }
        PlayerSkill?.OnSelect();
    }
    private void OnMove(Vector2 screenPos)
    {
        if (isUsing)
        {
            PlayerSkill.OnDrag(MainCamera.ScreenToWorldPoint(screenPos));
        }
    }
    private void OnComplete(Vector2 screenPos)
    {
        if (isUsing)
        {
            isUsing = false;
            if (!RectTransformUtility.RectangleContainsScreenPoint(SkillGroup, screenPos))
            {
                PlayerSkill?.Execute();
            }
            PlayerSkill?.EndAction();
        }
    }

    
    private void OnClick()
    {
        //playerSkill.OnSelect();
    }


    #region ◇ UI Events ◇
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnSelect();
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnMove(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnComplete(eventData.position);
    }
    #endregion
}