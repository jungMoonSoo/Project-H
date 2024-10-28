using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : Singleton<LobbyManager>
{
    //버튼을 설정하기 위해 설정 
    [SerializeField] Button recruitButton;
    [SerializeField] Button enhanceButton;
    [SerializeField] Button recoverButton;
    [SerializeField] Button missionButton;
    [SerializeField] Button storeButton;
    [SerializeField] Button fleidButton;
    [SerializeField] Button passButton;

    [SerializeField] GameObject lobby;
    [SerializeField] GameObject recreit;

    //버튼 셋팅 
    public void SettingButton() 
    {
        recruitButton.onClick.AddListener(Recruit);
        enhanceButton.onClick.AddListener(Enhance);
        recoverButton.onClick.AddListener(Recover);
        missionButton.onClick.AddListener(Mission);
        storeButton.onClick.AddListener(Store);
        fleidButton.onClick.AddListener(Fleid);
        passButton.onClick.AddListener(Pass);
    }

    void Recruit()
    {
        Debug.Log("모집");
        lobby.SetActive(false);
        recreit.SetActive(true);
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
