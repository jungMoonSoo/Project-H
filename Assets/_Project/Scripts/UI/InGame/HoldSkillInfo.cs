using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldSkillInfo : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject skillinfo;

    bool isHolding = false;
    float holdStartTime;
    const float pressedSkiil = 1.0f;
    private void Update()
    {
        if (isHolding && Time.time - holdStartTime >= pressedSkiil)
        {
            Debug.Log("[HoldSkillInfo]스킬창을 열었습니다.");
            skillinfo.SetActive(true);
            skillinfo.GetComponentInChildren<Text>().text = "testSkill";
            isHolding = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        holdStartTime = Time.time;
        isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (skillinfo.activeSelf)
        {
            skillinfo.SetActive(false);
        }

        isHolding = false;
    }
}
