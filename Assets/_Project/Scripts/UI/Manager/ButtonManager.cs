using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    //버튼을 설정하기 위해 설정 
    [SerializeField] Button RecruitButton;
    [SerializeField] Button EnhanceButton;
    [SerializeField] Button RecoverButton;
    [SerializeField] Button MissionButton;
    [SerializeField] Button StoreButton;
    [SerializeField] Button FleidButton;


    private static ButtonManager instance;


    //인스턴스에 대한 공용 접근자 
    public static ButtonManager Instance
    {
        get
        {
            // 인스턴스가 아직 생성되지 않았다면 찾거나 새로 생성
            if(instance == null)
            {
                instance = FindObjectOfType<ButtonManager>();

                if(instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<ButtonManager>();
                }
            }
            return instance;
        }
    }

    // singleton 생성 및 설정 
    public void SingletonMake() 
    {
        // 이미 인스턴스가 존재하는 경우 새로 생성된 인스턴스를 파괴
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    //버튼 셋팅 
    public void SettingButton() 
    {
        RecruitButton.onClick.AddListener(Recruit);
        EnhanceButton.onClick.AddListener(Enhance);
        RecoverButton.onClick.AddListener(Recover);
        MissionButton.onClick.AddListener(Mission);
        StoreButton.onClick.AddListener(Store);
        FleidButton.onClick.AddListener(Fleid);



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
}
