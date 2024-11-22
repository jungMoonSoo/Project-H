using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class TestEllipsePlayerSkill : PlayerSkillBase
{
    [Header("UI")]
    [SerializeField] private Image skillMaskImage;
    [SerializeField] private Text skillTimerText;

    [Header("이펙트 Prefabs")]
    [SerializeField] private GameObject areaEffectPrefab;
    [SerializeField] private GameObject skillEffectPrefab;

    [Header("기타 설정")]
    [SerializeField] private float coolDown;
    [SerializeField] private Vector2 effectRange;
    [SerializeField] private Sprite skillAreaSprite;

    #region ◇ abstract 구현 ◇
    public override GameObject AreaEffect => areaEffectPrefab;
    public override GameObject SkillEffect => skillEffectPrefab;
    public override float CoolDown => coolDown;
    public override IPlayerSkillCooldown SkillCooldown => _SkillCooldown;
    protected override ISkillArea SkillArea => _SkillArea;
    protected override Sprite SkillAreaSprite => skillAreaSprite;
    #endregion

    private readonly List<Unit> unitList = new(); 

    private IPlayerSkillCooldown _SkillCooldown = null;
    private ISkillArea _SkillArea = null;



    void Start()
    {
        _SkillCooldown = new FillingSkillCooldown(skillMaskImage, skillTimerText);
        _SkillArea = new EllipseSkillArea();
    }

    protected override void OnSkill(Vector3 position)
    {
        GameObject g = Instantiate(skillEffectPrefab, position, Quaternion.identity);
        g.GetComponent<SkillObjectBase>().EffectRange = effectRange;
    }
    protected override void SetAreaSize(ISkillArea skillArea)
    {
        skillArea.SetSize(effectRange);
    }
    protected override void CooledSkill()
    {
        Logger.Debug("스킬 쿨다운");
    }
}