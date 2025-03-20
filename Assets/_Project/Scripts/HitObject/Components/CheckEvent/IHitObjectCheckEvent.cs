public interface IHitObjectCheckEvent
{
    /// <summary>
    /// Object 생성시에만 실행되는 함수
    /// </summary>
    /// <param name="caster">Object 생성 주체</param>
    public void Init(Unidad caster);

    /// <summary>
    /// HitObject에서 OnFinish가 호출되기 이전까지 Update에서 호출되는 함수
    /// </summary>
    /// <param name="hitObject">메인 HitObject 정보</param>
    public void OnEvent(HitObject hitObject);
}
