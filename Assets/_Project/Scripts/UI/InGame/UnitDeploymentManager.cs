using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

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

    // 스테이지 정보 -를 기준으로 앞 뒤 숫자를 받아옴
    int frontStageNumber = 1;
    int backStageNumber = 1;

    int currentUnitNumber = 1; //현재 필드에 있는 unit 수
    int maxUnitNumber = 5;     //필드 최대 unit 수

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
        UnitCreat(0);
    }
    
    public void AlchemyButton()//물약이 버튼
    {
        UnitCreat(1);
    }

    public void Nun2Button() //수녀 버튼
    {
        UnitCreat(2);
    } 

    public void Cowardly_KinghtButton() //갑옷 
    {
        UnitCreat(3);
    }

    public void TA001Buttton() //번개궁수
    {
        UnitCreat(4);
    }

    //unit  생성 & Unit 삭제에 대한 메서드 작성

    void UnitCreat(uint num) //Unit 생성 
    {
        if (currentUnitNumber < maxUnitNumber)
        {
            managers.GetComponent<UnidadSpawnManager>().Spawn(UnidadManager.Instance.GetStatus(num), 0, true);
            currentUnitNumber++;
            UnitDeployTextUpdate(currentUnitNumber);
        }
        else
        {
            StartCoroutine(WarnningTextPrint());
            Debug.Log("[Unit Deployment Manager]유닛 배치 최대입니다.");
        }
    }

    IEnumerator WarnningTextPrint() //경고 문구
    {
        warnningObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        warnningObject.SetActive(false);
    }
}
