using UnityEngine;
using UnityEngine.UI;

public class TestPlayerSkill : PlayerSkillBase
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
    [SerializeField] private Text testText;

    public override GameObject AreaEffect => areaEffectPrefab;
    public override GameObject SkillEffect => skillEffectPrefab;
    public override float CoolDown => coolDown;
    public override IPlayerSkillCooldown SkillCooldown => _SkillCooldown;



    private IPlayerSkillCooldown _SkillCooldown = null;


    void Start()
    {
        _SkillCooldown = new RadialSkillCooldown(skillMaskImage, skillTimerText);
    }

    protected override void OnSkill(Vector3 position)
    {
        GameObject g = Instantiate(skillEffectPrefab, position, Quaternion.identity);
        g.GetComponent<SkillObjectBase>().EffectRange = effectRange;
    }
    protected override void SettingPlayerSkillArea(PlayerSkillArea skillArea)
    {
        skillArea.SetSize(effectRange);
    }
    protected override void CooledSkill()
    {
        Logger.Debug("스킬 쿨다운");
    }
}