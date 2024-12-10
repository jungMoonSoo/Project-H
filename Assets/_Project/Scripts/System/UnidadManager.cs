using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadManager : Singleton<UnidadManager>
{
    [SerializeField] private UnidadStatus[] unidadStatuses;
    [SerializeField] private UnidadStatus defaultStatus;
    
    
    [NonSerialized] public List<Unidad> unidades = new();

    private Dictionary<uint, UnidadStatus> unidadDict = new();
    
    
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
