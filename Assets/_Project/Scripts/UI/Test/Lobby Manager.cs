using System;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] LobbyPlayer player;

    public void DrawingCards(int num)// 카드 num회번 뽑기
    {
        for (int i = 0; i < num; i++)
        {
            int cardId = UnityEngine.Random.Range(0, 10);
            player.AddItem(cardId.ToString(), cardId, 1);
        }
        Debug.Log($"{num}번 모집 완료하였습니다.");
    }

    public void Cardinfo()
    {
        string s = "";
        foreach (var i in player.inventory)
        {
           s += i.itemName.ToString() + " ";   
        }


        Debug.Log($"{s}");
    }
#if UNITY_EDITOR
    public void ResetItem() //데이터 리셋
    {
        player.ResetItem();
    }
#endif

}
