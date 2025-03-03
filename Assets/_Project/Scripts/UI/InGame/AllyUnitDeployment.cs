using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

public class AllyUnitDeployment : Singleton<AllyUnitDeployment>
{
    [Header("GameObject 연결")]
    [SerializeField] private GameObject maxWarnningObject;
    [SerializeField] private LocalizeStringEvent unitNumberText;

    [Header("Unit 관리 메니져")]
    [SerializeField] private UnidadSpawnManager spawnManager;

    private BindData<int> currentUnitNumber = new(); //현재 필드에 있는 unit 수
    private int maxUnitNumber = 5;     //필드 최대 unit 수

    private HashSet<uint> units = new();

    // Localize 용 변수
    public int CurrentUnitNumber => currentUnitNumber.Value;
    public int MaxUnitNumber => maxUnitNumber;

    private void Start()
    {
        currentUnitNumber.SetCallback(UnitDeployTextUpdate, SetCallbackType.Add);

        maxWarnningObject.SetActive(false);
    }

    public void UnitDeployTextUpdate(int newValue) => unitNumberText.RefreshString();

    private IEnumerator WarningTextPrint() //경고 문구
    {
        maxWarnningObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        maxWarnningObject.SetActive(false);
    }

    //Unit 생성&삭제 Button
    public void UnitButton(uint id)
    {
        if (units.Contains(id)) DestroyUnit(id);
        else CreateUnit(id);
    }

    private void CreateUnit(uint id)
    {
        if (currentUnitNumber.Value < maxUnitNumber)
        {
            spawnManager.Spawn(id);
            units.Add(id);

            currentUnitNumber.Value++;
        }
        else
        {
            StartCoroutine(WarningTextPrint());
            Debug.Log("[Ui Manager]유닛 배치 최대입니다.");
        }
    }

    private void DestroyUnit(uint id)
    {
        UnitDeployManager.Instance.RemoveSpawnUnit(id);
        units.Remove(id);

        currentUnitNumber.Value--;
    }

    public void CreateSkillButton()
    {
        List<Unidad> unidads = UnitDeployManager.Instance.GetSpawnUnits(UnitType.Ally);

        for (int i = 0; i < unidads.Count; i++)
        {
            ActionSkillManager.Instance.AddSkillButton(unidads[i]);
            Debug.Log("[Ui Manager]스킬을 배정하였습니다.");
        }
    }
}
