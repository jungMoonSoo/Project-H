using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadManager : Singleton<UnidadManager>
{
    [SerializeField] private UnidadStatus[] unidadStatuses;
    [SerializeField] private UnidadStatus defaultStatus;
    
    private List<Unidad> enemyUnidades = new();
    private List<Unidad> allyUnidades = new();

    private Dictionary<uint, UnidadStatus> unidadDict = new();

    public Unidad GetUnidad(int index, UnitType type)
    {
        return type switch
        {
            UnitType.Ally => allyUnidades[index],
            UnitType.Enemy => enemyUnidades[index],
            _ => default,
        };
    }

    public List<Unidad> GetUnidads(UnitType type, bool reverse = false)
    {
        return type switch
        {
            UnitType.Ally => reverse ? enemyUnidades : allyUnidades,
            UnitType.Enemy => reverse ? allyUnidades : enemyUnidades,
            _ => default,
        };
    }

    public void SetUnidad(Unidad unidad, bool add, UnitType type)
    {
        switch (type)
        {
            case UnitType.Ally:
                if (add) allyUnidades.Add(unidad);
                else allyUnidades.Remove(unidad);
                break;

            case UnitType.Enemy:
                if (add) enemyUnidades.Add(unidad);
                else enemyUnidades.Remove(unidad);
                break;

            default:
                break;
        }
    }

    void Start()
    {
        foreach (UnidadStatus status in unidadStatuses)
        {
            unidadDict.Add(status.id, status);
        }
    }

    public UnidadStatus GetStatus(uint id)
    {
        return unidadDict.GetValueOrDefault(id, defaultStatus);
    }
}
