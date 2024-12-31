using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyUnitDeploymen : MonoBehaviour
{
    [Header("GameObject 연결")]
    [SerializeField] private GameObject maxWarnningObject;
    [SerializeField] private Text unitNumberText;

    [Header("Unit 관리 메니져")]
    [SerializeField] private GameObject unitManagerObject;

    [Header("Ally Tile Handle")]
    [SerializeField] private TileHandle[] tileHandler;

    private int currentUnitNumber = 1; //현재 필드에 있는 unit 수
    private int maxUnitNumber = 5;     //필드 최대 unit 수

    //Unit Spawn 여부 판단 변수(일단 아군 유닛 5종류로 설정)
    private bool[] unitSpawn = new bool[5];


    private bool blocksmithSpwan = false;
    private bool alchemySpawn = false;
    private bool nun2Spawn = false;
    private bool cowardly_KinghtSpawn = false;
    private bool ta001Spawn = false;

    public void UnitDeployTextUpdate(int num) //배치 수 업데이트 함수 
    {
        currentUnitNumber = num;
        unitNumberText.text = $"배치수 ({currentUnitNumber} / {maxUnitNumber})";
    }
    IEnumerator WarningTextPrint() //경고 문구
    {
        maxWarnningObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        maxWarnningObject.SetActive(false);
    }
    //Unit 생성&삭제 Button
    public void UnitButton(int num)
    {
        
        if (!unitSpawn[num])
        {
            CreateUnit((uint)num);
            unitSpawn[num] = true;
        }
        else
        {
            DestroyUnit((uint)num);
            unitSpawn[num] = false;
        }
    }

    //unit  생성 & Unit 삭제에 대한 메서드 작성

    void CreateUnit(uint num) //Unit 생성 
    {
        if (currentUnitNumber < maxUnitNumber)
        {
            unitManagerObject.GetComponent<UnidadSpawnManager>().Spawn(UnidadManager.Instance.GetStatus(num), 0, true);
            currentUnitNumber++;
            UnitDeployTextUpdate(currentUnitNumber);
        }
        else
        {
            StartCoroutine(WarningTextPrint());
            Debug.Log("[Ui Manager]유닛 배치 최대입니다.");
        }
    }

    void DestroyUnit(uint num) //Unit 삭제 => 삭제 메세드 
    {
        for (int i = 0; i < tileHandler.Length; i++)
        {
            if (tileHandler[i].Unit != null)
            {
                if (tileHandler[i].Unit.Status == UnidadManager.Instance.GetStatus(num))
                {
                    currentUnitNumber -= 1;
                    Destroy(tileHandler[i].Unit.gameObject);
                    tileHandler[i].RemoveUnit();
                    Debug.Log("[Ui Manager]해당 유닛을 제거하였습니다.");
                    break;
                }
            }
        }
    }
}
