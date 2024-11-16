using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour, IUnitPos, IUnitState, IUnitStatus, IUnitSkills
{
    public bool isAlly;
    public bool notMove;

    private UnitPosManager posManager;
    private UnitStateManager stateManager;
    private UnitStatusManager statusManager;
    private UnitSkillManager skillManager;

    [SerializeField] public UnitStatus status;
    [SerializeField] private LerpSprite hpBar;

    private readonly List<UnitSkillBase> skills = new();

    public Vector3 ExistingPos => posManager.ExistingPos;

    public int StateNum => stateManager.StateNum;
    public UnitState State => stateManager.State;
    public UnitStateBase StateBase => stateManager.StateBase;
    public Animator Animator => stateManager.Animator;

    public UnitStatus Status => statusManager.Status;
    public LerpSprite HpBar => statusManager.HpBar;

    public List<UnitSkillBase> Skills => skillManager.Skills;

    public EllipseCollider EllipseCollider { get; private set; }

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
        stateManager = new UnitStateManager(this, GetComponent<Animator>());
        posManager = new UnitPosManager(transform, isAlly);
        statusManager = new UnitStatusManager(this, status, hpBar);
        skillManager = new UnitSkillManager(this, skills);

        EllipseCollider = GetComponent<EllipseCollider>();
        EllipseCollider.SetArea(UnitManager.Instance.mapPos, UnitManager.Instance.mapSize);

        stateManager.StateChange(UnitState.Idle); // 초기 상태 설정
    }

    private void FixedUpdate()
    {
        stateManager.StateBase.OnUpdate();
    }

    public void SetPos(Vector2 pos) => posManager.SetPos(pos);

    public void ReturnToPos() => posManager.ReturnToPos();

    public void StateChange(UnitState _newState, int _stateNum = 0) => stateManager.StateChange(_newState, _stateNum);

    public bool OnDamage(bool _isActive, int _acc, int _atk, int _skp, int _cri, int _crp, int _fd) => statusManager.OnDamage(_isActive, _acc, _atk, _skp, _cri, _crp, _fd);

    public bool OnDamage(bool _isActive, UnitStatus _targetStatus, int _fd) => statusManager.OnDamage(_isActive, _targetStatus, _fd);

    [System.Obsolete("계산식 추가로 더 이상 사용하지 않습니다.")]
    public bool OnDamage(int _damage) => true;

    public bool OnHeal(int _healAmount) => statusManager.OnHeal(_healAmount);

    public bool CheckSkill() => skillManager.CheckSkill();

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!EditorApplication.isPlaying) return;
        
        Gizmos.color = Color.gray;
        
        Gizmos.DrawLine(transform.position, transform.position + StateBase.GetMovePos().normalized * 3);
    }
#endif
}
