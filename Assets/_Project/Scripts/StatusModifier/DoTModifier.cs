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

    public void Apply(StatusManager _status, int _time)
    {
        Status = _status;

        Status.UnitModifiers.Add(this, _time);
    }

    public void Check(int _count)
    {
        Status.OnDamage(damage);

        if (_count >= Count) Remove();
    }

    public void Remove()
    {
        Status.UnitModifiers.Remove(this);
    }
}
