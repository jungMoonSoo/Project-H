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

    public void CreateHitObject(Unidad unidad)
    {
        HitObject hitObject = hitObjectManager.GetHitObject(transform);

        hitObject.Target = targets[0];

        if (effectHandle != null)
        {
            SpineBoneData bone = effectHandle.GetBoneInfo(effectIndex);

            hitObject.Init(unidad, bone.effectManager, bone.bone.GetWorldPosition(transform));
        }
        else hitObject.Init(unidad, null, unidad.transform.position);
    }

    private void FlipX(Transform trans, bool right)
    {
        if (trans.localScale.x > 0 && right) return;
        if (trans.localScale.x < 0 && !right) return;

        trans.localScale = new Vector3(-trans.localScale.x, 1, 1);
    }
}
