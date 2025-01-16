using UnityEngine;

[CreateAssetMenu(menuName = "Skill/PassiveEffect/TriggerPassiveEffect", fileName = "NewTriggerPassiveEffect")]
public class TriggerPassiveEffect: PassiveEffectBase
{
    [Header("Trigger Settings")]
    [SerializeField] private PassiveTriggerType triggerType;
    [SerializeField] private float triggerNum;
    
    [Header("Effect Settings")]
    [SerializeField] private PassiveEffectType effectType;
    [SerializeField] private NormalStatus normalStatus;
    [SerializeField] private AttackStatus attackStatus;
    [SerializeField] private DefenceStatus defenceStatus;
    [SerializeField] private GameObject effectPrefab;
    
    
    public override void OnStart(Unidad owner)
    {
        
    }

    public override void OnExit(Unidad owner)
    {
        
    }


    private void OnEffect(Unidad owner)
    {
        switch (effectType)
        {
            case PassiveEffectType.EffectPrefab:
                Instantiate(effectPrefab, owner.transform.position, Quaternion.identity);
                break;
            case PassiveEffectType.SelfHeal:
                owner.OnHeal(attackStatus.magicDamage, DamageType.Normal);
                break;
        }
    }
}