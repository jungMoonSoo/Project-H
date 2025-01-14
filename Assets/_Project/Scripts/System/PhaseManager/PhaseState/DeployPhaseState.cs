using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployPhaseState : MonoBehaviour, IPhaseState
{
    [SerializeField] private GameObject deployUi;
    [SerializeField] private Text stageText;

    private int frontStageNumber = 0;
    private int backStageNumber = 0;
    public void OnEnter()
    {
        deployUi.SetActive(true);
        UnitDeployManager.Instance.SetAllTileActive(true);

        StageTextUpdate(1, 1);
    }

    public void OnUpdate()
    {
    }

    public void OnExit()
    {
        deployUi.SetActive(false);
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
