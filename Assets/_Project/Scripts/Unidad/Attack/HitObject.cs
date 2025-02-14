using UnityEngine;

public class HitObject : MonoBehaviour
{
    [SerializeField] private float coefficient = 100f;

    public Unidad Unidad { get; private set; }
    public Vector3 TargetPos { get; private set; }

    private ICheckHitable hitableCheck;
    private EffectManager effectManager;

    private ObjectPool<HitObject> hitObjects;

    public void Init(ObjectPool<HitObject> hitObjects) => this.hitObjects = hitObjects;

    public void Init(Unidad unidad, EffectManager effectManager)
    {
        Unidad = unidad;
        this.effectManager = effectManager;

        TryGetComponent(out hitableCheck);

        hitableCheck.HitObject = this;
    }

    private void Update() => hitableCheck.Hit();

    public void SetTargetPos(Vector3 pos) => TargetPos = pos;

    public void Remove() => hitObjects.Enqueue(this);

    public void Attack(Unidad target)
    {
        CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(Unidad.NowAttackStatus, target.NowDefenceStatus, coefficient, 0, ElementType.None);

        target.OnDamage((int)callback.value, callback.type, effectManager?.GetEffect(target.transform));

        Unidad.IncreaseMp(callback.type == DamageType.Miss ? 0.5f : 1);
    }
}
