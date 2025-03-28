using Spine.Unity;
using UnityEngine;

public class TrackingManager : MonoBehaviour
{
    [SerializeField] private SpineEffectHandle effectHandle;
    [SerializeField] private int effectIndex;

    private HitObjectManager hitObjectManager;

    private Unidad[] targets;
    public Unidad[] Targets => targets;

    private TrackingType trackingType = TrackingType.Near;

    private void Start() => TryGetComponent(out hitObjectManager);

    public void SetType(TrackingType type) => trackingType = type;

    /// <summary>
    /// 유닛이 현재 가져야 하는 상태 반환
    /// </summary>
    /// <param name="unidad">상태를 추론할 대상</param>
    /// <returns>가져야 하는 상태</returns>
    public UnitState CheckState(Unidad unidad)
    {
        ITrackingSystem trackingSystem = TrackingTypeHub.GetSystem(trackingType);

        if (trackingSystem.TryGetTargets(out targets, unidad.Owner, unidad.attackCollider))
        {
            Unidad target = targets[0];

            FlipX(unidad.transform, target.transform.position.x - unidad.transform.position.x > 0);

            if (unidad.attackCollider.OnEnter(target.unitCollider)) return UnitState.Attack;
            else return UnitState.Move;
        }

        return UnitState.Idle;
    }

    /// <summary>
    /// 공격 시 HitObject 생성 함수
    /// </summary>
    /// <param name="unidad">시전자</param>
    public void CreateHitObject(Unidad unidad)
    {
        HitObject hitObject = hitObjectManager.HitObjectPool.Get();

        hitObject.SetTarget(targets[0].Body);

        if (effectHandle != null)
        {
            SpineBoneData bone = effectHandle.GetBoneInfo(effectIndex);

            hitObject.SetEffect(bone.effectManager);
            hitObject.Init(unidad, bone.bone.GetWorldPosition(unidad.View));
        }
        else hitObject.Init(unidad, unidad.transform.position);
    }

    private void FlipX(Transform trans, bool right)
    {
        if (trans.localScale.x > 0 && right) return;
        if (trans.localScale.x < 0 && !right) return;

        trans.localScale = new Vector3(-trans.localScale.x, 1, 1);
    }
}
