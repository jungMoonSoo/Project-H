using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class TestItem
{
    public string name;
    public string desicion;

    public TestItem(string _name, string _desicion)
    {
        name = _name;
        desicion = _desicion;
    }
}
public class ItemStorageManager : MonoBehaviour
{
    [SerializeField]GameObject itemInfo;

    //아이템 설명창
    [SerializeField] Text itemName;
    [SerializeField] Text itemDesicion;
    [SerializeField] Image itemImage;

    TestItem[] itemSlot = new TestItem[9];// 아이템 슬롯

    int usedItemSlotNum;    //사용 중인 아이템 슬롯 

    private void Start()
    {
        itemSlot[0] = new TestItem("테스트용 아이템", "테스트용 아이템입니다.");
    }

    public void ItemInfomation(int num)//item의 정보를 보는 창
    {
        if (itemSlot[num - 1] != null)
        {
            TestItem i = itemSlot[num - 1];
            itemInfo.SetActive(true);

            itemName.text = itemSlot[num - 1].name;
            itemDesicion.text = itemSlot[num - 1].desicion;


            usedItemSlotNum = num;
        }
        else
        {
            Debug.Log("해당 칸에 데이터가 없습니다.");
        }
    }

    public void UseItem()   //아이템 사용했을 때
    {
        if (itemSlot[usedItemSlotNum - 1] != null)
        {
            itemSlot[usedItemSlotNum - 1] = null;
            itemInfo.SetActive(false);
            Debug.Log("아이템을 사용하였습니다.");
        }
        else
        {
            Debug.LogError("해당 아이템이 null입니다.");
        }
    }

    public void CancelInfomation()  //취소를 눌렸을 때
    {
        itemInfo.SetActive(false);
        Debug.Log("아이템 창을 닫았습니다.");
    }
}
