using System.Collections.Generic;
using UnityEngine;

public interface ITargetingFilter
{
    /// <summary>
    /// 전달 받은 Target 중 특정 조건에 부합하는 Target 반환
    /// </summary>
    /// <param name="unidads">Target 목록</param>
    /// <param name="castedPosition">시전자 위치</param>
    /// <returns>특정 조건에 부합하는 Target</returns>
    public Unidad[] GetFilteredTargets(List<Unidad> unidads, Vector3 castedPosition);
}