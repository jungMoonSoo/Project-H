using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour, IUnitPos, IUnitState, IUnitStatus, IUnitSkills
{
    public bool isAlly;
    public bool notMove;

    private Vector3 existingPos;

    private int stateNum;
    private UnitState state;
    private UnitStateBase stateBase;
    private Animator animator;

    [SerializeField] public UnitStatus status; // private으로 변경 예정
    [SerializeField] private LerpSprite hpBar;

    private readonly List<UnitSkillBase> skills = new();

    public Vector3 ExistingPos => existingPos;

    public int StateNum => stateNum;
    public UnitState State => state;
    public UnitStateBase StateBase => stateBase;
    public Animator Animator => animator;

    public UnitStatus Status => status;
    public LerpSprite HpBar => hpBar;

    public List<UnitSkillBase> Skills => skills;

    public EllipseCollider EllipseCollider { get; private set; }

    private readonly UnitBind unitBind = new();

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
        animator = GetComponent<Animator>();

        EllipseCollider = GetComponent<EllipseCollider>();
        EllipseCollider.SetArea(UnitManager.Instance.mapPos, UnitManager.Instance.mapSize);

        state = UnitState.Idle;
        stateBase = new UnitState_Idle(this, stateBase);

        unitBind.Init(this);
    }

    private void FixedUpdate()
    {
        stateBase.OnUpdate();
    }

    public void StateChange(UnitState _state, int _stateNum = 0)
    {
        if (state == _state) return;

        stateBase.OnExit();

        state = _state;
        stateNum = _stateNum;

        switch (state)
        {
            case UnitState.Idle:
                stateBase = new UnitState_Idle(this, stateBase);
                break;

            case UnitState.Move:
                stateBase = new UnitState_Move(this, stateBase);
                break;

            case UnitState.Attack:
                stateBase = new UnitState_Attack(this, stateBase);
                break;

            case UnitState.Skill:
                stateBase = new UnitState_Skill(this, stateBase);
                break;

            case UnitState.Die:
                stateBase = new UnitState_Die(this, stateBase);
                break;
        }

        stateBase.OnEnter();
    }

    public void SetPos(Vector2 _pos)
    {
        existingPos = _pos;

        ReturnToPos();
    }

    public void ReturnToPos()
    {
        transform.position = existingPos;

        if (isAlly) transform.rotation = Quaternion.Euler(0, 180, 0);
        else transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public bool OnDamage(bool _active, int _acc, int _atk, int _skp, int _cri, int _crp, int _fd)
    {
        if (_atk < 0) return false;

        int _acci = 0; // 추가 명중률
        int _atki = 0; // 추가 공격력
        int _crii = 0; // 추가 치명타 확률

        int _dodi = 0; // 추가 회피율
        int _cai = 0; // 추가 치명타 저항률
        int _crpi = 0; // 추가 치명타 확률
        int _defi = 0; // 추가 방어력

        if (Random.Range(0, 101) > (_acc + _acci) - (Status.dod + _dodi)) return false; // 명중 여부 확인

        float _dmg = (_atk + (_atk * _atki) + _atki) * (_skp * 0.01f);

        if (!_active && Random.Range(0, 101) < (_cri + _crii) - (Status.ca + _cai)) _dmg *= (_crp + _crpi) * 0.01f; // 치명타 여부 확인

        float _def = Status.def + (_defi * 0.01f) + _defi;

        _dmg *= 1 - _def / (_def + UnitManager.Instance.DM);
        _dmg += _fd; // 고정피해

        Status.hp[0].Data -= (int)_dmg;
        Status.mp[0].Data += (int)(Status.mpRegen * 0.5f);

        return true;
    }

    public bool OnDamage(bool _active, UnitStatus _status, int _fd) => OnDamage(_active, _status.acc, _status.atk, _status.skp, _status.cri, _status.crp, _fd);

    [System.Obsolete("계산식 추가로 더 이상 사용하지 않습니다.")]
    public bool OnDamage(int _dmg) => false;

    public bool OnHeal(int _value)
    {
        if (_value < 0) return false;

        Status.hp[0].Data += _value;

        return true;
    }

    public bool CheckSkill()
    {
        if (Skills.Count < StateNum + 1) return false;
        if (Status.mp[0].Data != Status.mp[1].Data) return false;

        return true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (stateBase == null) return;

        Gizmos.color = Color.gray;

        Gizmos.DrawLine(transform.position, transform.position + stateBase.GetMovePos() * 5);
    }
#endif
}
