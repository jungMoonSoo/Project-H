public interface ITrackingSystem
{
    /// <summary>
    /// 특정 조건에 부합하는 Target 반환
    /// </summary>
    /// <param name="targets">반환 받을 Target 배열</param>
    /// <param name="type">유닛 타입</param>
    /// <param name="collider">몸통 Collider</param>
    /// <returns>조건에 부합하는 Target이 있는지 여부</returns>
    public bool TryGetTargets(out Unidad[] targets, UnitType type, EllipseCollider collider);
}