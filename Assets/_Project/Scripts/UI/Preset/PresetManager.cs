using System.Collections.Generic;
using UnityEngine;

public class PresetManager : MonoBehaviour
{
    [SerializeField] private PresetButtonManager buttonManager;
    [SerializeField] private UnidadSpawnManager spawnManager;

    private HashSet<uint> spawnedUnits = new();

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

    public void CreatePreset()
    {
        int[] units = new int[UnitDeployManager.Instance.GetTiles(UnitType.Ally).Count];

        for (int i = 0; i < units.Length; i++) units[i] = -1;

        PlayerManager.Instance.presets.Add(units);
    }

    public void DeployUnit(int index)
    {
        currentPreset = index;

        foreach (int unitID in PlayerManager.Instance.presets[index])
        {
            if (unitID == -1) continue;

            CreateUnit((uint)unitID);
        }
    }

    public void SetPreset()
    {
        List<TileHandle> tiles = UnitDeployManager.Instance.GetTiles(UnitType.Ally);

        for (int i = 0; i < tiles.Count; i++) PlayerManager.Instance.presets[currentPreset][i] = (int)tiles[i].Unit.Status.id;
    }

    private void CreateUnit(uint id)
    {
        spawnManager.Spawn(id);
        spawnedUnits.Add(id);
    }
}
