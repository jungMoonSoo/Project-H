using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployPhaseState : MonoBehaviour, IPhaseState
{
    [SerializeField] private GameObject deployUi;
    [SerializeField] private Text stageText;

    [SerializeField] private Button spawnButton;
    [SerializeField] private Transform spawnParent;

    private List<Button> spawnButtons = new();
    private ObjectPool<Button> spawnButtonsPool;

    private int frontStageNumber = 0;
    private int backStageNumber = 0;

    private void Awake()
    {
        spawnButtonsPool = new(spawnButton);
    }

    public void OnEnter()
    {
        deployUi.SetActive(true);
        UnitDeployManager.Instance.SetAllTileActive(true);

        StageTextUpdate(1, 1);

        foreach (uint id in DataManager.Instance.units)
        {
            Button button = spawnButtonsPool.Dequeue(spawnParent);

            button.gameObject.SetActive(true);
            button.transform.localScale = Vector3.one;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => AllyUnitDeployment.Instance.UnitButton(id));

            spawnButtons.Add(button);
        }
    }

    public void OnUpdate()
    {
    }

    public void OnExit()
    {
        deployUi.SetActive(false);

        foreach (Button button in spawnButtons)
        {
            button.gameObject.SetActive(false);
            spawnButtonsPool.Enqueue(button);
        }

        spawnButtons.Clear();
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
