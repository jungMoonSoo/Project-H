using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitModifier/DoT", fileName = "NewDoT")]
public class DoTModifier : ScriptableObject, IUnitModifier
{
    [SerializeField] private int id;

    [SerializeField] private int count;
    [SerializeField] private int damage;

    public int Id => id;

    public int Count => count;

    public StatusManager Status { get; private set; }

    public void Apply(StatusManager status, int time)
    {
        Status = status;

        Status.UnitModifiers.Add(this, time);
    }

    public void Check(int count)
    {
        Status.OnDamage(damage);

        if (count >= Count) Remove();
    }

    public void Remove()
    {
        Status.UnitModifiers.Remove(this);
    }
}
