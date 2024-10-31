using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSkillButton: Button, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private IPlayerSkill PlayerSkill
    {
        get
        {
            if(_PlayerSkill == null)
            {
                _PlayerSkill = GetComponent<IPlayerSkill>();
                if (_PlayerSkill == null)
                {
                    Logger.Warning("스킬 버튼 조건이 완벽하지 않음.", name, _PlayerSkill);
                }
            }
            return _PlayerSkill;
        }
    }
    private IPlayerSkill _PlayerSkill = null;

    private bool isUsing = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!PlayerSkill.IsCooled)
        {
            isUsing = true;
        }
        PlayerSkill?.OnSelect();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isUsing)
        {
            PlayerSkill.OnDrag(eventData.position);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isUsing)
        {
            isUsing = false;
            if (!RectTransformUtility.RectangleContainsScreenPoint(InGameManager.Instance.SkillGroup, eventData.position))
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
}