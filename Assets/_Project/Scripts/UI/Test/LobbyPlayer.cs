using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class TestItem //테스트용 아이템 생성
{
    public string itemName;
    public int itemId;
    public int quantity;
    public TestItem(string name, int id, int qty)
    {
        itemName = name;
        itemId = id;
        this.quantity = qty;
    }
}

[System.Serializable]
public class ItemListWrapper { public List<TestItem> items; public ItemListWrapper(List<TestItem> list) => items = list; }

public class LobbyPlayer : MonoBehaviour
{
    public string playerName = "TEST id";
    public int level = 1;
    public int exp = 0;
    public int gold = 100;
    public int diamonds = 100;
    public int energy = 100;

    public List<TestItem> inventory = new();

    void Start()
    {
        LoadAllData();
        ResetItem();
    }
    private void OnApplicationQuit() => SaveAllData();

    //데이터 저장
    public void SaveAllData()
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("exp", exp);
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.SetInt("diamonds", diamonds);
        PlayerPrefs.SetInt("energy", energy);
        PlayerPrefs.SetString("PlayerInventory", JsonUtility.ToJson(new ItemListWrapper(inventory), true));
        PlayerPrefs.Save();
    }

    //데이터 로드
    public void LoadAllData()
    {
        playerName = PlayerPrefs.GetString("PlayerName", "DefaultHero");
        level = PlayerPrefs.GetInt("level", 1);
        exp = PlayerPrefs.GetInt("exp", 0);
        gold = PlayerPrefs.GetInt("gold", 100);
        diamonds = PlayerPrefs.GetInt("diamonds", 1000);
        PlayerPrefs.GetInt("energy", energy);

        if (PlayerPrefs.HasKey("PlayerInventory"))
        {
            inventory = JsonUtility.FromJson<ItemListWrapper>(PlayerPrefs.GetString("PlayerInventory")).items;
            Debug.Log($"불러온 인벤토리: {inventory.Count}개 아이템");
        }
    }

    //카드 획득
    public void AddItem(string name, int id, int qty)
    {
        LoadAllData();
        TestItem existingItem = inventory.Find(item => item.itemId == id);
        inventory.Add(new TestItem(name, id, qty));
        //if (existingItem != null) existingItem.quantity += qty;
        //else inventory.Add(new TestItem(name, id, qty));
        SaveAllData();
    }
    public void ResetItem()
    {
        inventory.Clear();
        SaveAllData() ;
    }
}