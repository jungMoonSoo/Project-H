using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Unit unit;

    public bool IsAlly { get; private set; }

    public void Init(bool _isAlly)
    {
        IsAlly = _isAlly;
    }

    public void SetActive(bool _active)
    {
        gameObject.SetActive(_active);
    }
}
