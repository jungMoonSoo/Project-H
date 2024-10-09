using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Singleton 인스턴스 생성
    private static UIManager instance;
    [SerializeField] ButtonManager buttonManager;

    //인스턴스에 대한 공용 접근자 
    public static UIManager Instance
    {
        get
        {
            // 인스턴스가 아직 생성되지 않았다면 찾거나 새로 생성
            if(instance == null)
            {
                instance = FindObjectOfType<UIManager>();

                if(instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }

    // Awake 메서드에서 Singleton 설정
    private void Awake()
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

    private void Start()
    {
        //button 메니져 선언 및 버튼 선언
        buttonManager.SingletonMake();
        buttonManager.SettingButton();
    }
}
