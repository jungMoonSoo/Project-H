public interface IHitObjectFinishEvent
{
    /// <summary>
    /// CheckEvent에서 CheckEvent의 호출을 끝내려는 경우 호출되는 함수
    /// </summary>
    /// <param name="hitObject">메인 HitObject 정보</param>
    public void OnEvent(HitObject hitObject);
}