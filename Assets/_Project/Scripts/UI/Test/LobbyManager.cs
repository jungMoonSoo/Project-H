using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private LobbyPlayer player;

    public void DrawingCards(int num)// 카드 num회번 뽑기
    {
        for (int i = 0; i < num; i++)
        {
            int cardId = Random.Range(0, 10);
            player.AddItem(cardId.ToString(), cardId, 1);
        }
        Debug.Log($"{num}번 모집 완료하였습니다.");
    }

    public void Cardinfo() //카드 정보
    {
        string s = "";
        foreach (var i in player.inventory)
        {
           s += i.itemName.ToString() + " ";   
        }
        Debug.Log($"{s}");
    }

    public void ChangeScene() // 씬체인지
    {
        LoadingSceneController.LoadScene("InGame");
    }
#if UNITY_EDITOR
    public void ResetItem() //데이터 리셋
    {
        player.ResetItem();
    }
#endif

}
