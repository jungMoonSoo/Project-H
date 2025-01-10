using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AllyUnitDeploymen : Singleton<AllyUnitDeploymen>
{
    [Header("GameObject 연결")]
    [SerializeField] private GameObject maxWarnningObject;
    [SerializeField] private Text unitNumberText;

    [Header("Unit 관리 메니져")]
    [SerializeField] private GameObject unitManagerObject;

    private int currentUnitNumber = 0; //현재 필드에 있는 unit 수
    private int maxUnitNumber = 5;     //필드 최대 unit 수

    //Unit Spawn 여부 판단 변수(일단 아군 유닛 5종류로 설정)
    private bool[] unitSpawn = new bool[5];
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
            unitManagerObject.GetComponent<UnidadSpawnManager>().SpawnAllyUnit(num);
            currentUnitNumber++;
            UnitDeployTextUpdate(currentUnitNumber);
        }
        else
        {
            StartCoroutine(WarningTextPrint());
            Debug.Log("[Ui Manager]유닛 배치 최대입니다.");
        }
    }

    void DestroyUnit(uint num) => UnitDeployManager.Instance.RemoveSpawnUnit(num);

    public void SkillConnect()
    {
        List<Unidad> unidads = UnitDeployManager.Instance.GetSpawnUnits(UnitType.Ally);

        for (int i = 0; i < unidads.Count; i++)
        {
            ActionSkillManager.Instance.AddSkillButton(unidads[i]);
            Debug.Log("[Ui Manager]스킬을 배정하였습니다.");
        }
    }
}
