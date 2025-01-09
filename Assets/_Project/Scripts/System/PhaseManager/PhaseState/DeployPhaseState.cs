using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployPhaseState : MonoBehaviour, IPhaseState
{
    [SerializeField] private GameObject standardCoreFieldUiObject;
    [SerializeField] private GameObject waveObject;
    [SerializeField] private GameObject unitDeploymentObject;
    [SerializeField] private Text stageText;
    [SerializeField] private GameObject TilesObject;

    private int frontStageNumber = 0;
    private int backStageNumber = 0;
    public void OnEnter()
    {
        TilesObject.SetActive(true);
        UnidadManager.Instance.ChangeAllUnitState(UnitState.Ready);
        standardCoreFieldUiObject.SetActive(false);
        unitDeploymentObject.SetActive(true);
        StageTextUpdate(1, 1);
    }

    public void OnUpdate()
    {
    }

    public void OnExit()
    {
        standardCoreFieldUiObject.SetActive(true);
        unitDeploymentObject.SetActive(false);
    }
    public void BackWindowButton() //뒤로 가기 버튼
    {
        Debug.Log("[Ui Manager]뒤로가기 버튼을 눌렸습니다. ");
    }

    public void StageTextUpdate(int front, int back) //Stage 정보 업데이트 함수 
    {
        frontStageNumber = front;
        backStageNumber = back;

        stageText.text = $"{frontStageNumber} - {backStageNumber}";
    }
}
