using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : Singleton<InGameUIManager>
{
    bool isAutoUsing = false;               //Auto 플레이를 하는지 아닌
    float gameSpeed = 1;                    //배속
    int stage = 1;                          //스테이지 정보

    [SerializeField] Slider hpSlider;       //Hp 슬라이더

    public void Option()    //옵션창 띄우기
    {
        Debug.Log("옵션창 열기");
    }

    public void SkillLock1()    //1번 스킬 잠금 
    {
        Debug.Log("Skill 1번이 잠겨있습니다.");
    }

    public void SkillLock2()    //2번 스킬 잠금 
    {
        Debug.Log("Skill 2번이 잠겨있습니다.");
    }

    public void SkillLock3()    //3번 스킬 잠금 
    {
        Debug.Log("Skill 3번이 잠겨있습니다.");
    }

    public void LeaderSkill1()  //1번 리더 스킬
    {
        Debug.Log("Leader Skill 1번을 사용하였습니다");
    }

    public void LeaderSkill2()  //2번 리더 스킬
    {
        Debug.Log("Leader Skill 2번을 사용하였습니다");
    }

    public void LeaderSkill3()  //3번 리더 스킬
    {
        Debug.Log("Leader Skill 3번을 사용하였습니다");
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
}
