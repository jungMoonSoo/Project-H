using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public void Awake()
    {
        DontDestroyOnLoad(this);

        units.Add(0);
        units.Add(1);
        units.Add(2);
        units.Add(3);
        units.Add(4);
    }

    public HashSet<uint> units = new();
}
