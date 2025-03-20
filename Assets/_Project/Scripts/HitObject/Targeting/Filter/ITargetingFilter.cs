using System.Collections.Generic;
using UnityEngine;

public interface ITargetingFilter
{
    public Unidad[] GetFilteredTargets(List<Unidad> unidads, Vector3 castedPosition);
}