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
        Debug.Log("[Unit Deployment Manager]  뒤로가기 버튼을 눌렸습니다. ");
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
        Debug.Log("[Unit Deployment Manager]  게임에 입장하셨습니다. ");
        unitDeploymentManger.SetActive(false);
        StandardCoreFieldUi.SetActive(true);
    }

    //Unidad Manager에서 Unidad Statuses 배열 오류  ==> 1이상 부터는 인식을 못함 
    public void BlocksmithButton() //대장장이 버튼 
    {
        managers.GetComponent<UnidadSpawnManager>().Spawn(UnidadManager.Instance.GetStatus(0), 0, true);
        currentUnitNumber++;
        UnitDeployTextUpdate(currentUnitNumber);
    }
    
    public void AlchemyButton()//물약이 버튼
    {
        managers.GetComponent<UnidadSpawnManager>().Spawn(UnidadManager.Instance.GetStatus(1), 0, true);
        currentUnitNumber++;
        UnitDeployTextUpdate(currentUnitNumber);
    }

    public void Nun2Button() //수녀 버튼
    {
        managers.GetComponent<UnidadSpawnManager>().Spawn(UnidadManager.Instance.GetStatus(2), 0, true);
        currentUnitNumber++;
        UnitDeployTextUpdate(currentUnitNumber);
    } 

    public void Cowardly_KinghtButton() //갑옷 
    {
        managers.GetComponent<UnidadSpawnManager>().Spawn(UnidadManager.Instance.GetStatus(3), 0, true);
        currentUnitNumber++;
        UnitDeployTextUpdate(currentUnitNumber);
    }

    public void TA001Buttton() //번개궁수
    {
        managers.GetComponent<UnidadSpawnManager>().Spawn(UnidadManager.Instance.GetStatus(4), 0, true);
        currentUnitNumber++;
        UnitDeployTextUpdate(currentUnitNumber);
    }
}
