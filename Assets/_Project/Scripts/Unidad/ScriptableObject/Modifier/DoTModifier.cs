using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitModifier/DoT", fileName = "NewDoT")]
public class DoTModifier : ScriptableObject, IUnitModifier
{
    [SerializeField] private int id;

    [SerializeField] private float count;
    [SerializeField] private int damage;

    public int Id => id;

    public float Count => count;

    public StatusManager Status { get; private set; }

    public void Apply(StatusManager status, float count)
    {
        Status = status;

        Status.UnitModifiers.Add(this, count);
    }

    public void Check(float count)
    {
        Status.OnDamage(damage);

        if (count >= Count) Remove();
    }

    public void Remove()
    {
        Status.UnitModifiers.Remove(this);
    }
}
