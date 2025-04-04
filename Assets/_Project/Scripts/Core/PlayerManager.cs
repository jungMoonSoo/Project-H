using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public string playerName = "Default";
    public int level = 1;
    public int exp = 0;
    public int gold = 100;
    public int diamonds = 100;
    public int energy = 100;

    public Dictionary<uint, PlayerUnitInfo> units = new();
    public List<DoubleValue<int, int>[]> presets = new();

    public void Start() => LoadData();

    private void OnApplicationQuit() => SaveData();

    public void SaveData()
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("Exp", exp);
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.SetInt("Diamonds", diamonds);
        PlayerPrefs.SetInt("Energy", energy);

        PlayerPrefs.SetString("Units", JsonUtility.ToJson(new Wrapper<PlayerUnitInfo>(units.ConvertValues()), true));
        PlayerPrefs.SetString("Preset", JsonUtility.ToJson(new Wrapper<DoubleValue<int, int>[]>(presets), true));

        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        playerName = PlayerPrefs.GetString("PlayerName", "Default");
        level = PlayerPrefs.GetInt("Level", 1);
        exp = PlayerPrefs.GetInt("Exp", 0);
        gold = PlayerPrefs.GetInt("Gold", 100);
        diamonds = PlayerPrefs.GetInt("Diamonds", 1000);
        energy = PlayerPrefs.GetInt("Energy", 100);

        if (PlayerPrefs.HasKey("Units"))
        {
            units.Clear();

            foreach (PlayerUnitInfo info in (List<PlayerUnitInfo>)JsonUtility.FromJson<Wrapper<PlayerUnitInfo>>(PlayerPrefs.GetString("Units")))
            {
                if (units.ContainsKey(info.id)) continue;

                units.Add(info.id, info);
            }
        }

        if (PlayerPrefs.HasKey("Preset")) presets = JsonUtility.FromJson<Wrapper<DoubleValue<int, int>[]>>(PlayerPrefs.GetString("Preset"));
    }
}
