using UnityEngine;

[CreateAssetMenu(menuName = "UnitModifier/NormalDoT", fileName = "NewNormalDoT")]
public class NormalDoTModifier : ScriptableObject, IUnitModifier
{
    [SerializeField] private int id;

    [SerializeField] private int cycleCount;
    [SerializeField] private float intervalTime;

    [SerializeField] private int damage;

    public int Id => id;

    public int CycleCount => cycleCount;

    private float applyTime;

    public virtual void Add(Unidad unidad) => applyTime = Time.time;

    public virtual void Remove(Unidad unidad) { }

    public virtual int Cycle(Unidad unidad)
    {
        if (applyTime > Time.time) return 0;

        applyTime = Time.time + intervalTime;

        unidad.OnDamage(damage, DamageType.Normal);

        return 1;
    }
}
