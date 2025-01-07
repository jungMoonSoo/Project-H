using UnityEngine;

public abstract class PassiveRequireBase: ScriptableObject
{
    public abstract PassiveRequireType RequireType { get; }
    public abstract float RequireValue { get; }


    public abstract bool IsRequired(Unidad owner);
}