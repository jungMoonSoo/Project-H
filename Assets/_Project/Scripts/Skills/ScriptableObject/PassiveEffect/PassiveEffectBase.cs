using UnityEngine;

public abstract class PassiveEffectBase: ScriptableObject
{
    /// <summary>
    /// RunPahse 시작 시점
    /// </summary>
    public abstract void OnStart(Unidad owner);
    /// <summary>
    /// RunPhase 종료 시점
    /// </summary>
    public abstract void OnExit(Unidad owner);
}