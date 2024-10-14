using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : Singleton<LobbyManager>
{
    //버튼을 설정하기 위해 설정 
    [SerializeField] Button RecruitButton;
    [SerializeField] Button EnhanceButton;
    [SerializeField] Button RecoverButton;
    [SerializeField] Button MissionButton;
    [SerializeField] Button StoreButton;
    [SerializeField] Button FleidButton;
    [SerializeField] Button PassButton;

    //버튼 셋팅 
    public void SettingButton() 
    {
        RecruitButton.onClick.AddListener(Recruit);
        EnhanceButton.onClick.AddListener(Enhance);
        RecoverButton.onClick.AddListener(Recover);
        MissionButton.onClick.AddListener(Mission);
        StoreButton.onClick.AddListener(Store);
        FleidButton.onClick.AddListener(Fleid);
        PassButton.onClick.AddListener(Pass);


    }

    void Recruit()
    {
        Debug.Log("모집");
    }

    void Enhance()
    {
        Debug.Log("강회");
    }

    void Recover() 
    {
        Debug.Log("강화");
    }
    void Mission()
    {
        Debug.Log("회복");
    }
    void Store()
    {
        Debug.Log("임무");
    }

    void Fleid()
    {
        Debug.Log("필드 돌아가기");
    }

    void Pass()
    {
        Debug.Log("넘기기");
    }
}
