using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDeploymentButton : MonoBehaviour
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

    public void BlocksmithButton() //대장장이 버튼 
    {
        if (!blocksmithSpwan)
        {
            CreateUnit(0);
            blocksmithSpwan = true;
        }
        else
        {
            DestroyUnit(0);
            blocksmithSpwan = false;

        }
    }

    public void AlchemyButton()//물약이 버튼
    {
        if (!alchemySpawn)
        {
            CreateUnit(1);
            alchemySpawn = true;
        }
        else
        {
            DestroyUnit(1);
            alchemySpawn = false;
        }
    }

    public void Nun2Button() //수녀 버튼
    {
        if (!nun2Spawn)
        {
            CreateUnit(2);
            nun2Spawn = true;
        }
        else
        {
            DestroyUnit(2);
            nun2Spawn = false;
        }
    }

    public void Cowardly_KinghtButton() //갑옷 
    {
        if (!cowardly_KinghtSpawn)
        {
            CreateUnit(3);
            cowardly_KinghtSpawn = true;
        }
        else
        {
            DestroyUnit(3);
            cowardly_KinghtSpawn = false;
        }
    }

    public void TA001Buttton() //번개궁수
    {
        if (!ta001Spawn)
        {
            CreateUnit(4);
            ta001Spawn = true;
        }
        else
        {
            DestroyUnit(4);
            ta001Spawn = false;
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
