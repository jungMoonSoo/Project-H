using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;
using UnityEngine.UI;

public class DeployPhaseState : MonoBehaviour, IPhaseState
{
    [SerializeField] private GameObject deployUi;
    [SerializeField] private Text stageText;

    [SerializeField] private SpawnButtonHandle spawnButtonPrefab;
    [SerializeField] private Transform spawnParent;

    private List<SpawnButtonHandle> spawnButtons = new();
    private IObjectPool<SpawnButtonHandle> spawnButtonPool;

    private int frontStageNumber = 0;
    private int backStageNumber = 0;

    private void Awake()
    {
        spawnButtonPool = new ObjectPool<SpawnButtonHandle>(CreateObject, OnGetObject, OnReleseObject, OnDestroyObject);
    }

    public void OnEnter()
    {
        deployUi.SetActive(true);
        UnitDeployManager.Instance.SetAllTileActive(true);

        StageTextUpdate(1, 1);

        foreach (uint id in PlayerManager.Instance.units)
        {
            SpawnButtonHandle button = spawnButtonPool.Get();

            button.gameObject.SetActive(true);

            button.SetID(id);

            spawnButtons.Add(button);
        }
    }

    public void OnUpdate()
    {
    }

    public void OnExit()
    {
        deployUi.SetActive(false);

        foreach (SpawnButtonHandle button in spawnButtons) spawnButtonPool.Release(button);

        spawnButtons.Clear();
    }

    public void BackWindowButton() => LoadingSceneController.LoadScene("Lobby");

    public void StageTextUpdate(int front, int back) //Stage 정보 업데이트 함수 
    {
        frontStageNumber = front;
        backStageNumber = back;

        stageText.text = $"{frontStageNumber} - {backStageNumber}";
    }

    private SpawnButtonHandle CreateObject()
    {
        SpawnButtonHandle spawnButtonHandle = Instantiate(spawnButtonPrefab, spawnParent);

        spawnButtonHandle.SetPool(spawnButtonPool);

        return spawnButtonHandle;
    }

    private void OnGetObject(SpawnButtonHandle spawnButtonHandle) => spawnButtonHandle.gameObject.SetActive(true);

    private void OnReleseObject(SpawnButtonHandle spawnButtonHandle) => spawnButtonHandle.gameObject.SetActive(false);

    private void OnDestroyObject(SpawnButtonHandle spawnButtonHandle) => Destroy(spawnButtonHandle.gameObject);
}
