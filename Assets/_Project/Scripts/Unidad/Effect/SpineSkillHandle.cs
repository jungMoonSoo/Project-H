using Spine.Unity;
using UnityEngine;

public class SpineSkillHandle : MonoBehaviour
{
    [SerializeField] private SpineEffectHandle effectHandle;
    [SerializeField] private int effectIndex;

    private Vector3 targetPos;

    public void SetTargetPos(Vector3 targetPos) => this.targetPos = targetPos;

    public void Spawn(Unidad unidad)
    {
        HitObject hitObject = Instantiate(unidad.Status.skillInfo?.effectPrefab, transform);

        hitObject.SetTargetPos(targetPos);

        if (effectHandle != null)
        {
            SpineBoneData bone = effectHandle.GetBoneInfo(effectIndex);

            hitObject.Init(unidad, bone.effectManager, bone.bone.GetWorldPosition(unidad.View));
        }
        else hitObject.Init(unidad, null, unidad.transform.position);
    }
}
