using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUnit
{
    public string name;
    public int grade;
    public int attack;
    public int magicPower;
    public int attackSpeed;
    public int defense;
    public string skillName1;
    public string skillDesc1;
    public string skillName2;
    public string skillDesc2;

    public TestUnit(string name, int grade, int attack, int magicPower, int attackSpeed, int defense, string skillName1, string skillDesc1, string skillName2, string skillDesc2)
    {
        this.name = name;
        this.grade = grade;
        this.attack = attack;
        this.magicPower = magicPower;
        this.attackSpeed = attackSpeed;
        this.defense = defense;
        this.skillName1 = skillName1;
        this.skillDesc1 = skillDesc1;
        this.skillName2 = skillName2;
        this.skillDesc2 = skillDesc2;
    }
}
public class UnitStorageManager : Singleton<UnitStorageManager>
{
    [SerializeField] GameObject unitInfo;
    //imfomation 관련
    [SerializeField] GameObject[] gradeImg;
    [SerializeField] GameObject roleSprite;
    [SerializeField] GameObject[] skillSelect;
    [SerializeField] Text charNameText;
    [SerializeField] Text[] statText;
    [SerializeField] Text skillName;
    [SerializeField] Text skillDesc;

    //unit 칸에 적용되는 class
    TestUnit[] unitSlot = new TestUnit[4];

    private void Start()
    {
        unitSlot[0] = new TestUnit("테스트용 유닛", 2, 100, 100, 100, 100, "테스트용 스킬1", "테스트용입니다.", "테스트용 스킬2", "테스트용입니다."); 
    }

    public void UnitInfomation(int num)//Unit의 정보를 보는 창
    {
        if (unitSlot[num - 1] != null)
        {
            TestUnit u = unitSlot[num - 1];
            unitInfo.SetActive(true); 
            for (int i = 0; i < u.grade; i++)
            {
                gradeImg[i].SetActive(true);
            }
            charNameText.text = u.name;
            //역할군 이미지 넣기
            statText[0].text = $"{u.attack}";
            statText[1].text = $"{u.magicPower}";
            statText[2].text = $"{u.attackSpeed}";
            statText[3].text = $"{u.defense}";
            skillName.text = u.skillName1;
            skillDesc.text = u.skillDesc1;

        }
        else
        {
            Debug.Log("해당 칸에 데이터가 없습니다.");
        }
    } 

    public void CloseUnitInfo()//창 닫기
    {
        unitInfo.SetActive(false);
    }
}
