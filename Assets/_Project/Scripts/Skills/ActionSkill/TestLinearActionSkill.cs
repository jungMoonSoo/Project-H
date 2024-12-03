using UnityEngine;
using UnityEngine.UI;

public class TestLinearActionSkill : ActionSkillBase
{
    [Header("성능")]
    [SerializeField] private float damage = 5;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int hitCount = 1;
    
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

    private IPlayerSkillCooldown _SkillCooldown = null;
    private ISkillArea _SkillArea = null;



    void Start()
    {
        _SkillCooldown = new FillingSkillCooldown(skillMaskImage, skillTimerText);
        _SkillArea = new LinearSkillArea();
    }

    protected override void OnSkill(Vector3 position)
    {
        Vector3 dirRange = ((Vector2)position - (Vector2)InGameManager.Instance.PlayerTransform.position).normalized * effectRange.x;
        Debug.Log(dirRange);
        Vector2 target = InGameManager.Instance.PlayerTransform.position + dirRange;
        
        GameObject g = Instantiate(skillEffectPrefab, InGameManager.Instance.PlayerTransform.position, Quaternion.identity);
        TestLinearSkillEffect skillEffect = g.GetComponent<TestLinearSkillEffect>();
        skillEffect.EffectRange = Vector2.one * effectRange.y;
        skillEffect.Influence = damage;
        skillEffect.MoveSpeed = speed;
        skillEffect.HitCount = hitCount;
        skillEffect.SetPosition(target);
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