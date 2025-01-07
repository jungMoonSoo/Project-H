using System;
using System.Collections.Generic;
using UnityEngine;

public class UnidadManager : Singleton<UnidadManager>
{
    [SerializeField] private UnidadStatus[] unidadStatuses;
    [SerializeField] private UnidadStatus defaultStatus;
    
    private Dictionary<UnitType, List<Unidad>> unidads = new();

    private Dictionary<uint, UnidadStatus> unidadDict = new();

    void Awake()
    {
        foreach (UnitType type in Enum.GetValues(typeof(UnitType))) unidads.Add(type, new());
    }

    void Start()
    {
        foreach (UnidadStatus status in unidadStatuses)
        {
            unidadDict.Add(status.id, status);
        }
    }
    
    public void ChangeAllUnitState(UnitState state)
    {
        foreach (List<Unidad> unidadList in unidads.Values)
        {
            foreach (Unidad unidad in unidadList) unidad.StateChange(state);
        }
    }

    public Unidad GetUnidad(int index, UnitType type)
    {
        if (unidads[type].Count > index) return unidads[type][index];
        return null;
    }

    public List<Unidad> GetUnidads(UnitType type, TargetType targetType)
    {
        return type switch
        {
            UnitType.Ally => targetType switch
            {
                TargetType.Me => unidads[UnitType.Ally],
                TargetType.We => unidads[UnitType.Ally],
                TargetType.They => unidads[UnitType.Enemy],
                _ => default,
            },
            UnitType.Enemy => targetType switch
            {
                TargetType.Me => unidads[UnitType.Enemy],
                TargetType.We => unidads[UnitType.Enemy],
                TargetType.They => unidads[UnitType.Ally],
                _ => default,
            },
            _ => default,
        };
    }

    public void SetUnidad(Unidad unidad, bool add, UnitType type)
    {
        if (add) unidads[type].Add(unidad);
        else unidads[type].Remove(unidad);
    }

    public UnidadStatus GetStatus(uint id)
    {
        return unidadDict.GetValueOrDefault(id, defaultStatus);
    }
}
