using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetManager : MonoBehaviour
{
    [SerializeField] private PresetButtonManager buttonManager;
    [SerializeField] private PresetUnitManager unitManager;
    [SerializeField] private PresetSkillManager skillManager;

    [SerializeField] private UnidadSpawnManager spawnManager;

    private readonly Dictionary<int, int[]> spawnedUnits = new();

    public int CurrentPreset { get; private set; }

    private void Start()
    {
        buttonManager.Init(this);
        unitManager.Init(this);
        skillManager.Init(this);

        StartCoroutine(WaitForDeployManager());
    }

    public void GoLobby()
    {
        RecordPreset();

        LoadingSceneController.LoadScene("Lobby");
    }

    private IEnumerator WaitForDeployManager()
    {
        yield return null;

        if (PlayerManager.Instance.presets.Count == 0) CreatePreset();
        else DeployUnit(0, false);
    }

    #region Preset
    /// <summary>
    /// 신규 Preset 생성
    /// </summary>
    public void CreatePreset()
    {
        int[] units = new int[UnitDeployManager.Instance.GetTiles(UnitType.Ally).Count * 3];

        for (int i = 0; i < units.Length; i++) units[i] = -1;

        PlayerManager.Instance.presets.Add(units);
        buttonManager.SetItem(PlayerManager.Instance.presets.Count - 1);

        DeployUnit(0);
    }

    /// <summary>
    /// 배치된 Unit을 Preset에 기록
    /// </summary>
    public void RecordPreset()
    {
        List<TileHandle> tiles = UnitDeployManager.Instance.GetTiles(UnitType.Ally);

        int unitID;
        int currentIndex;

        for (int i = 0; i < tiles.Count; i++)
        {
            currentIndex = 3 * i;

            if (tiles[i].Unit == null)
            {
                PlayerManager.Instance.presets[CurrentPreset][currentIndex] = -1;
                PlayerManager.Instance.presets[CurrentPreset][currentIndex + 1] = -1;
                PlayerManager.Instance.presets[CurrentPreset][currentIndex + 2] = -1;
            }
            else
            {
                unitID = (int)tiles[i].Unit.Status.id;

                PlayerManager.Instance.presets[CurrentPreset][currentIndex] = spawnedUnits[unitID][0];
                PlayerManager.Instance.presets[CurrentPreset][currentIndex + 1] = i;
                PlayerManager.Instance.presets[CurrentPreset][currentIndex + 2] = spawnedUnits[unitID][2];
            }
        }

        PlayerManager.Instance.SaveData();
    }

    /// <summary>
    /// Preset에 저장 된 Unit 배치
    /// </summary>
    /// <param name="index">Preset Index</param>
    public void DeployUnit(int index, bool record = true)
    {
        if (record) RecordPreset();

        ClearUnits();

        CurrentPreset = index;

        int length = UnitDeployManager.Instance.GetTiles(UnitType.Ally).Count;
        int currentIndex;

        for (int i = 0; i < length; i++)
        {
            currentIndex = 3 * i;

            if (PlayerManager.Instance.presets[index][currentIndex] == -1) continue;
            else CreateUnit(PlayerManager.Instance.presets[index][currentIndex..(currentIndex + 3)]);
        }
    }
    #endregion

    #region Unit
    /// <summary>
    /// Preset에 추가하기 위한 유닛 생성
    /// </summary>
    /// <param name="skillIndex">Skill 번호</param>
    /// <returns>유닛 ID</returns>
    public int CreateUnit(int skillIndex)
    {
        int id = unitManager.SelectUnitID;

        if (id == -1) return -1;

        int[] infos = new int[3];

        infos[0] = id;
        infos[1] = spawnManager.Spawn((uint)id);
        infos[2] = skillIndex;

        spawnedUnits.Add(infos[0], infos);

        unitManager.SelectUnitID = -1;

        return id;
    }

    /// <summary>
    /// Preset에 기록된 유닛 생성
    /// </summary>
    /// <param name="infos">배치된 유닛 정보</param>
    public void CreateUnit(int[] infos)
    {
        spawnManager.Spawn((uint)infos[0], infos[1], UnitType.Ally);
        spawnedUnits.Add(infos[0], infos);

        skillManager.SetInfo(infos[0], infos[2]);
    }

    /// <summary>
    /// 스폰 된 유닛 제거
    /// </summary>
    /// <param name="unitID">유닛 ID</param>
    public void RemoveUnit(int unitID)
    {
        skillManager.SetInfo(-1, spawnedUnits[unitID][2]);

        UnitDeployManager.Instance.RemoveSpawnUnit((uint)unitID);
        spawnedUnits.Remove(unitID);
    }

    /// <summary>
    /// 스폰 된 모든 유닛 제거
    /// </summary>
    public void ClearUnits()
    {
        UnitDeployManager.Instance.ClearUnits();

        skillManager.Clear();
        spawnedUnits.Clear();
    }

    /// <summary>
    /// 유닛 스폰 여부 확인
    /// </summary>
    /// <param name="unitID">유닛 ID</param>
    /// <returns>스폰 여부</returns>
    public bool CheckSpawn(int unitID) => spawnedUnits.ContainsKey(unitID);
    #endregion
}
