using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameUIManager : Singleton<MainGameUIManager>
{
    bool isAutoUsing = false;               //Auto 플레이를 하는지 아닌
    float gameSpeed = 1;                    //배속
    int stage = 1;                          //스테이지 정보

    bool isUnitStorageOpening = false;      //유닛 배낭이 열였는지
    bool isItemStorageOpening = false;      //아이템 배낭이 열렸는지

    [SerializeField] Slider hpSlider;       //Hp 슬라이더

    [SerializeField] GameObject unitStorage;
    [SerializeField] GameObject itemStorage;

    [Header("Skill 착용")]
    [SerializeField] ActionSkillBase[] LeaderSkillSlot;//스킬 종류 착용
    [SerializeField] GameObject[] skillSlotObj;        //스킬 오브젝트
    public void Option()    //옵션창 띄우기
    {
        Debug.Log("옵션창 열기");
    }

    public void LeaderSkillLock(int num)
    {
        Debug.Log($"Skill {num}번이 잠겨있습니다.");
    }

    public void LeaderSkillUse(int num)//리더스킬 적용 부분
    {
    }
    public void GameStartButton()   //게임 시작 버튼
    {
        Debug.Log("Game Start!!");
    }

    public void AutoButton()        //Auto 버튼
    {
        if (isAutoUsing)
        {
            Debug.Log("auto OFF");
            isAutoUsing = false;
        }
        else
        {
            Debug.Log("auto ON");
            isAutoUsing = true;
        }
    }

    public void SpeedButton()        //배속 버튼
    {
        if (gameSpeed == 1)
        {
            Debug.Log("1.5 배속을 합니다.");
            gameSpeed = 1.5f;
        }
        else if (gameSpeed == 1.5f)
        {
            Debug.Log("2배속을 합니다.");
            gameSpeed = 2;
        }
        else if (gameSpeed == 2f)
        {
            Debug.Log("1배속을 합니다.");
            gameSpeed = 1;
        }
    }

    public void UnitStorageOpenClose() //유닛 배낭 열고 닫기
    {
        if (!isUnitStorageOpening)
        {
            unitStorage.SetActive(true);
            isUnitStorageOpening = true;
        }
        else
        {
            unitStorage.SetActive(false);
            isUnitStorageOpening = false;
        }
    }

    public void ItemStorageOpenClose() //아이템 배낭 열고 닫기
    {
        if (!isItemStorageOpening)
        {
            itemStorage.SetActive(true);
            isItemStorageOpening = true;
        }
        else
        {
            itemStorage.SetActive(false);
            isItemStorageOpening = false;
        }
    }
}
