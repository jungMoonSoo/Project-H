using UnityEngine;

[CreateAssetMenu(menuName = "Skill/PassiveEffect/ModifierPassiveEffect", fileName = "NewModifierPassiveEffect")]
public class ModifierPassiveEffect: PassiveEffectBase
{
    // [SerializeField] private ModifierBase modifier;
    
    public override void OnStart(Unidad owner)
    {
        // owner.ModifierManager.Add(modifier);
    }

    public override void OnExit(Unidad owner)
    {
        
    }
}