public interface IHitObjectTriggerEvent
{
    /// <summary>
    /// Object 생성시에만 실행되는 함수
    /// </summary>
    /// <param name="caster">Object 생성 주체</param>
    public void Init(Unidad caster);

    /// <summary>
    /// CheckEvent에서 특정 조건에 부합하는 경우 호출되는 함수
    /// </summary>
    /// <param name="hitObject">메인 HitObject 정보</param>
    public void OnEvent(HitObject hitObject);
}