using UnityEngine;

public abstract class ModifierBase : ScriptableObject
{
    [SerializeField] private int id;

    [SerializeField] private int cycleCount;
    [SerializeField] private float intervalTime;

    public int Id => id;

    public int CycleCount => cycleCount;

    public float IntervalTime => intervalTime;

    public virtual void Add(Unidad unidad) { }

    public virtual void Remove(Unidad unidad) { }

    public virtual int Cycle(Unidad unidad) => 1;
}

