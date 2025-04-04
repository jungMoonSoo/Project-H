using System.Collections.Generic;
using UnityEngine;

public class PresetManager : MonoBehaviour
{
    [SerializeField] private PresetButtonManager buttonManager;
    [SerializeField] private UnidadSpawnManager spawnManager;

    private readonly Dictionary<int, int> spawnedUnits = new();

    private int currentPreset;

    private void Start()
    {
        buttonManager.Init();

        for (int i = 0; i < PlayerManager.Instance.presets.Count; i++) buttonManager.SetItem(i);

        if (PlayerManager.Instance.presets.Count == 0) CreatePreset();
    }

    public void GoLobby()
    {
        LoadingSceneController.LoadScene("Lobby");
    }

    /// <summary>
    /// 신규 Preset 생성
    /// </summary>
    public void CreatePreset()
    {
        DoubleValue<int, int>[] units = new DoubleValue<int, int>[UnitDeployManager.Instance.GetTiles(UnitType.Ally).Count];

        for (int i = 0; i < units.Length; i++) units[i].Set(-1, -1);

        PlayerManager.Instance.presets.Add(units);
    }

    /// <summary>
    /// Preset에 저장 된 Unit 배치
    /// </summary>
    /// <param name="index">Preset Index</param>
    public void DeployUnit(int index)
    {
        currentPreset = index;

        foreach (DoubleValue<int, int> presetInfo in PlayerManager.Instance.presets[index])
        {
            if (presetInfo.first == -1) continue;

            CreateUnit(presetInfo.first, presetInfo.second);
        }
    }

    /// <summary>
    /// 배치된 Unit을 Preset에 기록
    /// </summary>
    public void RecordPreset()
    {
        List<TileHandle> tiles = UnitDeployManager.Instance.GetTiles(UnitType.Ally);

        int unitID;

        for (int i = 0; i < tiles.Count; i++)
        {
            unitID = (int)tiles[i].Unit.Status.id;

            PlayerManager.Instance.presets[currentPreset][i].Set(unitID, spawnedUnits[unitID]);
        }
    }

    public void CreateUnit(int unitID, int skillIndex)
    {
        spawnManager.Spawn((uint)unitID);
        spawnedUnits.Add(unitID, skillIndex);
    }
}
