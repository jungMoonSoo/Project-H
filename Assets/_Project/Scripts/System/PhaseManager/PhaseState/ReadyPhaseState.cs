using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyPhaseState : MonoBehaviour, IPhaseState
{
    [Header("GameObject 연결")]
    [SerializeField] private GameObject unitDeploymentObject;
    [SerializeField] private Text stageText;

    // 스테이지 정보 -를 기준으로 앞 뒤 숫자를 받아옴
    private int frontStageNumber = 0;
    private int backStageNumber = 0;
    public void OnEnter()
    {
        unitDeploymentObject.SetActive(true);
    }

    public void OnUpdate()
    {
    }

    public void OnExit()
    {
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
