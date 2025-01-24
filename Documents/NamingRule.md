# C# 네이밍 룰
## 네이밍 코딩 스타일
1. 변수
    - PascalCase로 해야하는 경우
        - NonSerialize인 public 변수
        - static 유형
    - camelCase로 해야하는 경우
        - SerializeField public 변수
        - protected 변수
        - private 변수
    - SCREAMING_SNAKE_CASE로 해야하는 경우
        - const 변수
2. 프로퍼티
    - PascalCase로
3. 메소드
    - PascalCase로
4. 클래스
    - PascalCase로
5. 열거형
    - PascalCase로

## 호칭 네이밍
1. Boolean
    - 접두사에 Is, Has, Can, Should
    - ex) isAlive, HasItem
2. 비동기 메소드 Async
    - 접미사에 Async
    - ex) FrameMoveAsync
3. 비동기 메소드 코루틴
    - 접미사에 Coroutine, Co 등
    - ex) CountingTimerCoroutine, CountingTimerCo
4. 추상 클래스
    - 접미사에 Base
    - ex) SkillBase
5. 인터페이스
    - 접두사에 I
    - ex) IUnidadState
6. 이벤트
    1. event, Action, delegate 등
        - ~Event
        - ex) AttackEvent
    2. event, Action, delegate 등에서 사용되는 Method
        - On~
        - ex) OnDamage
7. On 접두사
    - 수동적인 상황에서 사용되는 명칭
    - ~상태일 때 사용되는 Method
    - 콜백 및 이벤트 명칭
    - ex) OnDamage, OnStateChanged
8. 메소드 명칭 기본 구조
    - 기본적으로 **동사 + 명사**의 구조
    - ex) RideVenhicle, TakeDamage, ChangeState
9. 상태패턴
    - interface < State 구조
    - State 클래스의 명칭은 interface의 이름을 붙일 것
    - ex) IUnidadState < AttackUniState, MoveUniState

# Specific 네이밍 룰
1. Animation Parameter
    - camelCase로
2. Tag
    - PascalCase로
3. Layer
    - PascalCase로