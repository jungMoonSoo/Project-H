using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isAlly;
    public bool notMove;

    public UnitStatus status;
    public UnitStateBase stateBase;
    public UnitSkillBase[] skills;

    public LerpSprite hpBar = new();

    public int StateNum { get; private set; }
    public Animator Animator { get; private set; }
    public EllipseCollider EllipseCollider { get; private set; }

    private UnitState state;
    private Vector3 existingPos;

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
        Animator = GetComponent<Animator>();
        EllipseCollider = GetComponent<EllipseCollider>();

        EllipseCollider.SetArea(UnitManager.Instance.mapPos, UnitManager.Instance.mapSize);

        state = UnitState.Idle;
        stateBase = new UnitState_Idle(this, stateBase);

        unitBind.Init(this);
    }

    private void FixedUpdate()
    {
        stateBase.SetTarget();
        stateBase.OnUpdate();
    }

    public void StateChange(UnitState _state, int _stateNum = 0)
    {
        if (state == _state) return;

        stateBase.OnExit();

        state = _state;
        StateNum = _stateNum;

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

        ReturnPos();
    }

    public void ReturnPos()
    {
        transform.position = existingPos;

        if (isAlly) transform.rotation = Quaternion.Euler(0, 180, 0);
        else transform.rotation = Quaternion.Euler(0, 0, 0);
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
