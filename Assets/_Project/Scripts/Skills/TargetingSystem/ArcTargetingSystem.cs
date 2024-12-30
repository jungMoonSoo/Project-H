using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArcTargetingSystem: ITargetingSystem
{
    /// <summary>
    /// 원뿔 형태의 범위에 속하는 타겟 목록 반환
    /// </summary>
    /// <param name="targetOwner">탐색 할 대상</param>
    /// <param name="casterPosition">시작 위치</param>
    /// <param name="castedPosition">각도 x~y</param>
    /// <param name="rangeSize">탐색 범위</param>
    /// <returns></returns>
    public Unidad[] GetTargets(UnitType targetOwner, Vector2 casterPosition, Vector2 castedPosition, Vector2 rangeSize)
    {
        List<Unidad> targets = new(UnidadManager.Instance.unidades.Where(x => x.Owner == targetOwner));

        for (int i = targets.Count - 1; i >= 0; i--)
        {
            Vector2 targetPos = (Vector2)targets[i].transform.position + targets[i].unitCollider.center;

            if (!CheckTargetInArea(casterPosition, rangeSize, targetPos, targets[i].unitCollider.Radius, castedPosition)) targets.Remove(targets[i]);
        }

        return targets.ToArray();
    }

    private bool CheckTargetInArea(Vector2 centerPos, Vector2 rangeSize, Vector2 targetPos, Vector2 targetSize, Vector2 angle)
    {
        if (VectorCalc.CalcEllipse(centerPos, targetPos, rangeSize, targetSize) > 1f) return false;

        foreach (Vector2 point in GetContactPoints(targetPos, targetSize))
        {
            if (CheckDirectionInAngle(point - centerPos, angle)) return true;
        }

        if (CheckDirectionInAngle(targetPos - centerPos, angle)) return true;

        return false;
    }

    private bool CheckDirectionInAngle(Vector2 direction, Vector2 angle)
    {
        float centerAngle = Mathf.Repeat(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, 360);
        float normalizedStartCenter = Mathf.Repeat(angle.x, 360);
        float normalizedEndCenter = Mathf.Repeat(angle.y, 360);

        if (normalizedStartCenter <= normalizedEndCenter)
        {
            if (centerAngle >= normalizedStartCenter && centerAngle <= normalizedEndCenter) return true;
        }
        else
        {
            if (centerAngle >= normalizedStartCenter || centerAngle <= normalizedEndCenter) return true;
        }

        return false;
    }

    private Vector2[] GetContactPoints(Vector2 centerB, Vector2 radiiB)
    {
        return new Vector2[] { centerB + new Vector2(-radiiB.x, 0), centerB + new Vector2(radiiB.x, 0), centerB + new Vector2(0, -radiiB.y), centerB + new Vector2(0, radiiB.y) };
    }
}