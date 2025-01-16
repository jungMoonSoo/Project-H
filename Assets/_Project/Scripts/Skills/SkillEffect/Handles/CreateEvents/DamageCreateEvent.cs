using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCreateEvent : MonoBehaviour, ISkillEffectCreateEvent
{
    [SerializeField] private float skillCoefficient = 200f;

    public void OnCreate(SkillEffectHandlerBase handler)
    {
        CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(handler.AttackStatus, handler.DefenceStatus, skillCoefficient, 0, ElementType.None);

        foreach (Unidad unit in handler.Targets) unit.OnDamage((int)callback.value, callback.type);
    }
}
