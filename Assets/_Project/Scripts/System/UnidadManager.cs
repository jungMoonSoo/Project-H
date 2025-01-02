using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnidadManager : Singleton<UnidadManager>
{
    [SerializeField] private UnidadStatus[] unidadStatuses;
    [SerializeField] private UnidadStatus defaultStatus;
    
    private Dictionary<UnitType, List<Unidad>> unidades = new();

    private Dictionary<uint, UnidadStatus> unidadDict = new();

    public Unidad GetUnidad(int index, UnitType type)
    {
        if (unidades[type].Count > index) return unidades[type][index];
        return null;
    }

    public List<Unidad> GetUnidads(UnitType type, TargetType targetType)
    {
        return type switch
        {
            UnitType.Ally => targetType switch
            {
                TargetType.Me => unidades[UnitType.Ally],
                TargetType.We => unidades[UnitType.Ally],
                TargetType.They => unidades[UnitType.Enemy],
                _ => default,
            },
            UnitType.Enemy => targetType switch
            {
                TargetType.Me => unidades[UnitType.Enemy],
                TargetType.We => unidades[UnitType.Enemy],
                TargetType.They => unidades[UnitType.Ally],
                _ => default,
            },
            _ => default,
        };
    }

    public void SetUnidad(Unidad unidad, bool add, UnitType type)
    {
        if (add) unidades[type].Add(unidad);
        else unidades[type].Remove(unidad);
    }

    void Start()
    {
        foreach (UnidadStatus status in unidadStatuses)
        {
            unidadDict.Add(status.id, status);
        }

        foreach (UnitType type in Enum.GetValues(typeof(UnitType)).Cast<UnitType>()) unidades.Add(type, new());
    }

    public UnidadStatus GetStatus(uint id)
    {
        return unidadDict.GetValueOrDefault(id, defaultStatus);
    }
}
