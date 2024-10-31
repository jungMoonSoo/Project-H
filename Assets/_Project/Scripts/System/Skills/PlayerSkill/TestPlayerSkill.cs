using System;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayerSkill : PlayerSkillBase
{
    [SerializeField]
    private GameObject areaEffectPrefab;
    [SerializeField]
    private GameObject skillEffectPrefab;
    [SerializeField]
    private float coolDown;
    [SerializeField]
    private Text testText;



    public override Camera MainCamera => InGameManager.Instance.MainCamera;
    public override GameObject AreaEffect => areaEffectPrefab;
    public override GameObject SkillEffect => skillEffectPrefab;
    public override float CoolDown => coolDown;


    protected override void OnSkill(Vector3 position)
    {
        Logger.Debug("플레이어 스킬 테스트", position);
        Instantiate(skillEffectPrefab, position, Quaternion.identity);
    }
    protected override void CooledSkill()
    {
        Logger.Debug("스킬 쿨다운");
    }

    protected override void BeginCoolDown()
    {
        testText.text = "쿨다운 시작";
    }

    protected override void AfterCoolDown()
    {
        testText.text = "쿨다운 종료";
    }
}