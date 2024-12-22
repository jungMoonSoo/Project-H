using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Unit 배치에 관련한 UI 관리 manager  
/// </summary>
public class UnitDeploymentManager : Singleton<UnitDeploymentManager>
{
    [Header("GameObject 연결")]
    [SerializeField] GameObject unitDeploymentManger;
    [SerializeField] GameObject StandardCoreFieldUi;
    [SerializeField] GameObject warnningObject;
    [SerializeField] Text stageNumber;
    [SerializeField] Text unitNumber;

    [Header("TEST")]
    [SerializeField] GameObject managers;

    [Header("Ally Tile Handle")]
    [SerializeField] TileHandle[] tileInfo;

    // 스테이지 정보 -를 기준으로 앞 뒤 숫자를 받아옴
    int frontStageNumber = 1;
    int backStageNumber = 1;

    int currentUnitNumber = 1; //현재 필드에 있는 unit 수
    int maxUnitNumber = 5;     //필드 최대 unit 수

    //임시 버튼  => List 변경 => unit값으로 관리  
    bool blocksmithSpwan = false;
    bool alchemySpawn =false;
    bool nun2Spawn = false;
    bool cowardly_KinghtSpawn = false;
    bool ta001Spawn = false;
    private void Start()
    {
        StageTextUpdate(1, 2);
        UnitDeployTextUpdate(1);
    }
    public void BackWindowButton() //뒤로 가기 버튼
    {
        Debug.Log("[Unit Deployment Manager]뒤로가기 버튼을 눌렸습니다. ");
    }

    public void StageTextUpdate(int front, int back)
    {
        frontStageNumber = front;
        backStageNumber = back;

        stageNumber.text = $"{frontStageNumber} - {backStageNumber}";
    }
    
    public void UnitDeployTextUpdate(int num) //배치 수 업데이트 함수 
    {
        currentUnitNumber = num;
        unitNumber.text = $"배치수 ({currentUnitNumber} / {maxUnitNumber})";
    }

    public void GameEntrance()
    {
        Debug.Log("[Unit Deployment Manager]게임에 입장하셨습니다. ");
        unitDeploymentManger.SetActive(false);
        StandardCoreFieldUi.SetActive(true);
    }

    //Unidad Manager에서 Unidad Statuses 배열 오류  ==> 1이상 부터는 인식을 못함 
    public void BlocksmithButton() //대장장이 버튼 
    {
        if (!blocksmithSpwan)
        {
            CreateUnit(0);
            blocksmithSpwan = true;
        }
        else
        {
            DestroyUnit(0);
            blocksmithSpwan = false;

        }
    }
    
    public void AlchemyButton()//물약이 버튼
    {
        if (!alchemySpawn)
        {
            CreateUnit(1);
            alchemySpawn = true;
        }
        else
        {
            DestroyUnit(1);
            alchemySpawn = false;
        }
    }

    public void Nun2Button() //수녀 버튼
    {
        if (!nun2Spawn)
        {
            CreateUnit(2);
            nun2Spawn = true;
        }
        else
        {
            DestroyUnit(2);
            nun2Spawn = false;
        }
    } 

    public void Cowardly_KinghtButton() //갑옷 
    {
        if (!cowardly_KinghtSpawn)
        {
            CreateUnit(3);
            cowardly_KinghtSpawn = true;
        }
        else
        {
            DestroyUnit(3);
            cowardly_KinghtSpawn = false;
        }
    }

    public void TA001Buttton() //번개궁수
    {
        if (!ta001Spawn)
        {
            CreateUnit(4);
            ta001Spawn = true;
        }
        else
        {
            DestroyUnit(4);
            ta001Spawn = false;
        }
    }

    //unit  생성 & Unit 삭제에 대한 메서드 작성

    void CreateUnit(uint num) //Unit 생성 
    {
        if (currentUnitNumber < maxUnitNumber)
        {
            managers.GetComponent<UnidadSpawnManager>().Spawn(UnidadManager.Instance.GetStatus(num), 0, true);
            currentUnitNumber++;
            UnitDeployTextUpdate(currentUnitNumber);
        }
        else
        {
            StartCoroutine(WarningTextPrint());
            Debug.Log("[Unit Deployment Manager]유닛 배치 최대입니다.");
        }
    }

    void DestroyUnit(uint num) //Unit 삭제 => 삭제 메세드 
    {
        for (int i = 0; i < tileInfo.Length; i++)
        {
            if (tileInfo[i].Unit != null)
            {
                if (tileInfo[i].Unit.Status == UnidadManager.Instance.GetStatus(num))
                {
                    currentUnitNumber -= 1;
                    Destroy(tileInfo[i].Unit.gameObject);
                    tileInfo[i].RemoveUnit();
                    Debug.Log("[Unit Deployment Manager]해당 유닛을 제거하였습니다.");
                    break;
                }
            }
        }

    }

    IEnumerator WarningTextPrint() //경고 문구
    {
        warnningObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        warnningObject.SetActive(false);
    }
}
