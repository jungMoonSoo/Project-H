using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadAttack : MonoBehaviour
{
    private Unidad parent;
    private Unidad target;

    public void Init(Unidad parent, Unidad target)
    {
        this.parent = parent;
        this.target = target;
    }

    private IEnumerator Action()
    {
        while (true)
        {
            yield return null;
        }
    }
}
