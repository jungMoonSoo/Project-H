using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RecruitManager : Singleton<RecruitManager>
{
    [SerializeField] GameObject character1;
    [SerializeField] GameObject character2;
    [SerializeField] GameObject character3;

    public void SettingButton()//버튼 세팅
    {
        character1.GetComponentInChildren<Button>().onClick.AddListener(Character1Choice);
    }

    void Character1Choice()//캐릭터1를 선택했을 때 상호작용
    {
        if(character1.GetComponentInChildren<GameObject>().activeSelf == false)
        {
            character1.GetComponentInChildren<GameObject>().SetActive(true);
        }
        else
        {
            character1.GetComponentInChildren<GameObject>().SetActive(false );
        }
    }
}
