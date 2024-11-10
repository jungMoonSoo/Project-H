using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreTouch : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]GameObject chat;
    public void OnPointerDown(PointerEventData eventData)
    {
        TouchChat();
    }

    //터치했을 때 대화창이 바뀌도록
    void TouchChat()
    {
        string storeText;
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            storeText = "안녕하세요. 상점입니다.";
        }
        else 
        {
            storeText = "원하시는 물건이 있나요?";
        }
        chat.GetComponentInChildren<Text>().text = storeText;
    }
}
