using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;
using UnityEngine.UI;

// TODO: 코드 정돈작업 필요, BackWindowButton Method는 다른 Class로 옮겨야 함.
public class DeployPhaseState : MonoBehaviour, IPhaseState
{
    [Header("Visible UI Group")]
    [SerializeField] private GameObject deployUi;
    [SerializeField] private Text txtStage;

    [Header("Spawn Button Settings")]
    [SerializeField] private SpawnButtonHandle spawnButtonPrefab;
    [SerializeField] private Transform spawnParent;

    
    private readonly List<SpawnButtonHandle> spawnButtons = new();
    private IObjectPool<SpawnButtonHandle> spawnButtonPool;

    private int frontStageNumber = 0;
    private int backStageNumber = 0;

    
    #region ◇ Unity Events ◇
    private void Awake()
    {
        spawnButtonPool = new ObjectPool<SpawnButtonHandle>(CreateObject, OnGetObject, OnReleseObject, OnDestroyObject);
    }
    #endregion
    
    
    public void OnEnter()
    {
        // Stage text 수정 및 UI 초기화
        deployUi.SetActive(true);
        UnitDeployManager.Instance.SetAllTileActive(true);

        StageTextUpdate(1, 1);

        
        // 캐릭터 Button 생성
        foreach (uint id in PlayerManager.Instance.units.Keys)
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
        // 스폰 버튼 제거
        deployUi.SetActive(false);

        foreach (SpawnButtonHandle button in spawnButtons) spawnButtonPool.Release(button);

        spawnButtons.Clear();
    }
    

    /// <summary>
    /// 스테이지 텍스트 작성 Method<br/>
    /// ex) "<b>{MainStage}</b>-<b>{SubStage}</b>"
    /// </summary>
    /// <param name="front">MainStage</param>
    /// <param name="back">SubStage</param>
    private void StageTextUpdate(int front, int back) //Stage 정보 업데이트 함수 
    {
        frontStageNumber = front;
        backStageNumber = back;

        txtStage.text = $"{frontStageNumber} - {backStageNumber}";
    }

    /// <summary>
    /// 전 Scean으로 돌아가기 위한 Method
    /// </summary>
    public void BackWindowButton() => LoadingSceneController.LoadScene("Lobby");

    
    #region ◇ SpawnButton Events ◇
    private SpawnButtonHandle CreateObject()
    {
        SpawnButtonHandle spawnButtonHandle = Instantiate(spawnButtonPrefab, spawnParent);

        spawnButtonHandle.SetPool(spawnButtonPool);

        return spawnButtonHandle;
    }
    private void OnGetObject(SpawnButtonHandle spawnButtonHandle) => spawnButtonHandle.gameObject.SetActive(true);
    private void OnReleseObject(SpawnButtonHandle spawnButtonHandle) => spawnButtonHandle.gameObject.SetActive(false);
    private void OnDestroyObject(SpawnButtonHandle spawnButtonHandle) => Destroy(spawnButtonHandle.gameObject);
    #endregion
}
