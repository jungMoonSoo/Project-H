using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour, IUnitPos, IUnitMove, IUnitState, IUnitStatus, IUnitSkills
{
    public UnitType unitType;
    public bool notMove;

    private UnitStateBase stateBase;

    private UnitPosManager posManager;
    private UnitStatusManager statusManager;
    private UnitSkillManager skillManager;

    [SerializeField] public UnitStatus status;
    [SerializeField] private LerpSprite hpBar;

    private readonly List<UnitSkillBase> skills = new();
    private readonly Dictionary<UnitState, UnitStateBase> unitStates = new();

    public Vector3 ExistingPos => posManager.ExistingPos;

    public int StateNum { get; private set; }
    public UnitState State { get; private set; }
    public Animator Animator { get; private set; }

    public Unit Target { get; set; }
    public Vector3 MovePos { get; set; }
    public EllipseCollider EllipseCollider { get; private set; }

    public UnitStatus Status => statusManager.Status;
    public LerpSprite HpBar => statusManager.HpBar;

    public List<UnitSkillBase> Skills => skillManager.Skills;

    private void OnEnable()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying) UnitManager.Instance.units.Add(this);
#else
        UnitManager.Instance.units.Add(this);
#endif
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        if (gameObject.scene.isLoaded) UnitManager.Instance.units.Remove(this);
#else
        UnitManager.Instance.units.Remove(this);
#endif
    }

    private void Start()
    {
        posManager = new UnitPosManager(transform, unitType);
        statusManager = new UnitStatusManager(this, status, hpBar);
        skillManager = new UnitSkillManager(this, skills);

        EllipseCollider = GetComponent<EllipseCollider>();
        EllipseCollider.SetArea(UnitManager.Instance.mapPos, UnitManager.Instance.mapSize);

        Animator = GetComponent<Animator>();

        unitStates.Add(UnitState.Idle, new UnitState_Idle(this));
        unitStates.Add(UnitState.Move, new UnitState_Move(this));
        unitStates.Add(UnitState.Attack, new UnitState_Attack(this));
        unitStates.Add(UnitState.Skill, new UnitState_Skill(this));
        unitStates.Add(UnitState.Die, new UnitState_Die(this));

        stateBase = unitStates[UnitState.Idle]; // 초기 상태 설정
    }

    private void Update()
    {
        stateBase.OnUpdate();
    }

    public void StateChange(UnitState _newState, int _stateNum = 0)
    {
        if (State == _newState) return;

        stateBase.OnExit();

        State = _newState;
        StateNum = _stateNum;
        stateBase = unitStates[_newState];

        stateBase.OnEnter();
    }

    public void SetPos(Vector2 _pos) => posManager.SetPos(_pos);

    public void ReturnToPos() => posManager.ReturnToPos();

    public bool OnDamage(bool _isActive, DamageStatus _targetStatus, int _fd) => statusManager.OnDamage(_isActive, _targetStatus, _fd);

    [System.Obsolete("계산식 추가로 더 이상 사용하지 않습니다.")]
    public bool OnDamage(int _damage) => true;

    public bool OnHeal(int _healAmount) => statusManager.OnHeal(_healAmount);

    public bool CheckSkill() => skillManager.CheckSkill();

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!EditorApplication.isPlaying) return;
        
        Gizmos.color = Color.gray;
        
        Gizmos.DrawLine(transform.position, transform.position + MovePos.normalized * 3);
    }
#endif
}
