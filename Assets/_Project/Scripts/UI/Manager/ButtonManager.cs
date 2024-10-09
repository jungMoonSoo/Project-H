using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    //��ư�� �����ϱ� ���� ���� 
    [SerializeField] Button RecruitButton;
    [SerializeField] Button EnhanceButton;
    [SerializeField] Button RecoverButton;
    [SerializeField] Button MissionButton;
    [SerializeField] Button StoreButton;
    [SerializeField] Button FleidButton;


    private static ButtonManager instance;


    //�ν��Ͻ��� ���� ���� ������ 
    public static ButtonManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� �������� �ʾҴٸ� ã�ų� ���� ����
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

    // singleton ���� �� ���� 
    public void SingletonMake() 
    {
        // �̹� �ν��Ͻ��� �����ϴ� ��� ���� ������ �ν��Ͻ��� �ı�
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    //��ư ���� 
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
        Debug.Log("����");
    }

    void Enhance()
    {
        Debug.Log("��ȸ");
    }

    void Recover() 
    {
        Debug.Log("��ȭ");
    }
    void Mission()
    {
        Debug.Log("ȸ��");
    }
    void Store()
    {
        Debug.Log("�ӹ�");
    }

    void Fleid()
    {
        Debug.Log("�ʵ� ���ư���");
    }
}
