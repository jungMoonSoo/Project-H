using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Singleton �ν��Ͻ� ����
    private static UIManager instance;
    [SerializeField] ButtonManager buttonManager;

    //�ν��Ͻ��� ���� ���� ������ 
    public static UIManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� �������� �ʾҴٸ� ã�ų� ���� ����
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

    // Awake �޼��忡�� Singleton ����
    private void Awake()
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

    private void Start()
    {
        //button �޴��� ���� �� ��ư ����
        buttonManager.SingletonMake();
        buttonManager.SettingButton();
    }
}
