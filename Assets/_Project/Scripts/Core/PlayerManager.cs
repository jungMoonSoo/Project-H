using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public HashSet<uint> units = new();

    public void Awake()
    {
        units.Add(0);
        units.Add(1);
        units.Add(2);
        units.Add(3);
        units.Add(4);
        units.Add(5);
    }
}
