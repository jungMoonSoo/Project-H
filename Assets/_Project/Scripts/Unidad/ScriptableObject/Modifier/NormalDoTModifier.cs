using UnityEngine;

[CreateAssetMenu(menuName = "UnitModifier/NormalDoT", fileName = "NewNormalDoT")]
public class NormalDoTModifier : ScriptableObject, IUnitModifier
{
    [SerializeField] private int id;

    [SerializeField] private int count;
    [SerializeField] private int damage;

    public int Id => id;

    public int Count => count;

    public void Add(Unidad unidad) { }

    public void Remove(Unidad unidad) { }

    public virtual int Check(Unidad unidad)
    {
        unidad.OnDamage(damage, DamageType.Normal);

        return 1;
    }
}
