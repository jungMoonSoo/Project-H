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
        HitObject hitObject = Instantiate(unidad.Status.skillInfo?.effectPrefab);

        hitObject.SetTargetPos(targetPos);

        if (targetPos.x - unidad.transform.position.x < 0)
        {
            Vector3 scale = hitObject.transform.localScale;

            scale.x = -scale.x;

            hitObject.transform.localScale = scale;
        }

        if (effectHandle != null)
        {
            SpineBoneData bone = effectHandle.GetBoneInfo(effectIndex);

            hitObject.Init(unidad, bone.effectManager, bone.bone.GetWorldPosition(transform));
        }
        else hitObject.Init(unidad, null, unidad.transform.position);
    }
}
