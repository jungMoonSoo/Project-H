using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/DoT", fileName = "NewDoTEffect")]
public class DoTEffect : ScriptableObject, IStatusEffect
{
    [SerializeField] private int id;

    [SerializeField] private int count;
    [SerializeField] private int damage;

    public int Id => id;

    public int Count => count;

    public StatusManager Status { get; private set; }

    public void Apply(StatusManager _status)
    {
        Status = _status;

        Status.StatusEffects.Add(this);
    }

    public void Check(int _count)
    {
        Status.OnDamage(damage);

        if (_count >= Count) Remove();
    }

    public void Remove()
    {
        Status.StatusEffects.Remove(this);
    }
}
