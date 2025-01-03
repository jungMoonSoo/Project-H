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

    public void Add(Unidad unidad)
    {

    }

    public void Tick(Unidad unidad)
    {
        unidad.OnDamage(damage, DamageType.Normal);
    }

    public void Remove(Unidad unidad)
    {

    }
}
