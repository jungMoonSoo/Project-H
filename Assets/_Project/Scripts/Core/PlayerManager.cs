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

    public HashSet<uint> units = new();
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

        PlayerPrefs.SetString("Inventory", JsonUtility.ToJson(new Wrapper<uint>(units), true));
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

        if (PlayerPrefs.HasKey("Inventory")) units = JsonUtility.FromJson<Wrapper<uint>>(PlayerPrefs.GetString("Inventory"));
        if (PlayerPrefs.HasKey("Preset")) presets = JsonUtility.FromJson<Wrapper<DoubleValue<int, int>[]>>(PlayerPrefs.GetString("Preset"));
    }
}
